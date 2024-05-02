using UBB_SE_2024_Team_42.Domain.Badge;
using UBB_SE_2024_Team_42.Domain.Category;
using UBB_SE_2024_Team_42.Domain.Post;
using UBB_SE_2024_Team_42.Domain.Post.Interfaces;
using UBB_SE_2024_Team_42.Domain.Posts;
using UBB_SE_2024_Team_42.Domain.Reactions;
using UBB_SE_2024_Team_42.Domain.Tag;
using UBB_SE_2024_Team_42.Domain.User;
using UBB_SE_2024_Team_42.Repository;
using UBB_SE_2024_Team_42.Service.EntityCreationServices;
using UBB_SE_2024_Team_42.Utils;
using UBB_SE_2024_Team_42.Utils.Functionals;

namespace UBB_SE_2024_Team_42.Service
{
    public class Service : IService
    {
        private readonly IRepository repository;

        private readonly List<IQuestion> currentQuestions;
        public Service(IRepository repository)
        {
            this.repository = repository;
            currentQuestions = GetAllQuestions();
        }

        public IUser GetUser(long userId)
        {
            return repository.GetUser(userId);
        }

        public IPost GetPost(long postId)
        {
            return repository.GetPost(postId);
        }

        public void UpdatePost(IPost oldPost, IPost newPost)
        {
            repository.UpdatePost(oldPost, newPost);
        }

        public IQuestion GetQuestion(long userId)
        {
            return repository.GetQuestion(userId);
        }

        public List<IQuestion> GetAllQuestions()
        {
            return repository.GetAllQuestions().ToList();
        }

        public void AddPostReply(IPost reply, long postId)
        {
            repository.AddPostReply(reply, postId);
        }

        public List<IPost> GetRepliesOfPost(long postId)
        {
            return repository.GetRepliesOfPost(postId).ToList();
        }

        public List<IQuestion> GetQuestionsOfCategory(ICategory? category)
        {
            if (category == null)
            {
                return new ();
            }
            bool QuestionIsOfCategory(IQuestion iquestion) => iquestion.Category?.Name == category.Name;
            return StreamProcessor<IQuestion, IQuestion>.FilterCollection(repository.GetAllQuestions(), QuestionIsOfCategory).ToList();
        }

        public List<IQuestion> GetQuestionsWithAtLeastOneAnswer()
        {
            static bool IsIAnswer(IPost ipost) => ipost is IAnswer;
            bool QuestionHasAtLeastOneAnswer(IQuestion question) => repository.GetRepliesOfPost(question.ID).Any(IsIAnswer);
            List<IQuestion> filteredQuestions = repository.GetAllQuestions().Where(QuestionHasAtLeastOneAnswer).ToList();
            return filteredQuestions;
        }

        public List<IQuestion> FindQuestionsByPartialStringInAnyField(string textToBeSearchedBy)
        {
            bool PartialStringMatches(string str) => str.Contains(textToBeSearchedBy, StringComparison.CurrentCultureIgnoreCase);
            bool TagNameMatches(ITag itag) => PartialStringMatches(itag.Name);
            bool AnyTagPartialMatches(IQuestion question) => question.Tags.Where(TagNameMatches).Any();
            bool AnyKeywordInTitleMatched(IQuestion question) => question.Title?.Split(" ").Where(PartialStringMatches).Any() ?? false;
            bool MasterFilterCondition(IQuestion question) => AnyTagPartialMatches(question) || AnyKeywordInTitleMatched(question);
            return StreamProcessor<IQuestion, IQuestion>.FilterCollection(repository.GetAllQuestions(), MasterFilterCondition).ToList();
        }

        public List<IQuestion> GetQuestionsSortedByScoreAscending()
        {
            static int GetReactionValue(IReaction ireaction) => ireaction.Value;

            Dictionary<IQuestion, int> questionToReactionValueMapping = new ();

            List<IQuestion> listOfQuestions = currentQuestions;
            CollectionSummer<IReaction> reactionValueSummer = new (GetReactionValue);
            void AddMappingForQuestion(IQuestion question) =>
                questionToReactionValueMapping[question] = GetReactionScore(repository.GetReactionsOfPostByPostID(question.ID).ToList());

            listOfQuestions.ForEach(AddMappingForQuestion);

            Dictionary<IQuestion, int> sortedQuestionToReactionValueMap =
                questionToReactionValueMapping.OrderBy(questionValuePair => questionValuePair.Value).ToDictionary();

            return sortedQuestionToReactionValueMap.Keys.ToList();
        }

        public List<IQuestion> GetQuestionsSortedByScoreDescending()
        {
            List<IQuestion> questions = GetQuestionsSortedByScoreAscending();
            questions.Reverse();
            return questions;
        }

        private static int GetReactionScore(List<IReaction> voteList)
        {
            static int GetReactionValue(IReaction ireaction) => ireaction.Value;
            CollectionReducer<IReaction, int> summer = new (
                mapper: GetReactionValue,
                folder: (x, y) => x + y,
                defaultResult: 0);
            return summer.MapThenFold(voteList);
        }

        public List<IQuestion> SortQuestionsByNumberOfAnswersAscending()
        {
            Dictionary<IQuestion, int> iQuestionToIAnswerCountMapping = new ();
            void ProcessQuestion(IQuestion question)
                => iQuestionToIAnswerCountMapping[question]
                = StreamProcessor<IPost, IPost>.FilterCollection(repository.GetRepliesOfPost(question.ID), Filters.IPostIsIAnswer).Count();
            currentQuestions.ForEach(ProcessQuestion);
            Dictionary<IQuestion, int> sortedMapping = iQuestionToIAnswerCountMapping.OrderBy(x => x.Value).ToDictionary();
            return sortedMapping.Keys.ToList();
        }

        public List<IQuestion> SortQuestionsByNumberOfAnswersDescending()
        {
            List<IQuestion> questions = SortQuestionsByNumberOfAnswersAscending();
            questions.Reverse();
            return questions;
        }

        public List<IQuestion> SortQuestionsByDateAscending()
        {
            Dictionary<IQuestion, DateTime> iQuestionToDatePostedMapping = new ();
            void ProcessQuestion(IQuestion question) => iQuestionToDatePostedMapping[question] = question.DatePosted;
            currentQuestions.ForEach(ProcessQuestion);
            Dictionary<IQuestion, DateTime> sortedMap = iQuestionToDatePostedMapping.OrderBy(keyValuePair => keyValuePair.Value).ToDictionary();
            return sortedMap.Keys.ToList();
        }

        public List<IQuestion> SortQuestionsByDateDescending()
        {
            List<IQuestion> questions = SortQuestionsByDateAscending();
            questions.Reverse();
            return questions;
        }

        public List<ICategory> GetAllCategories()
        {
            return repository.GetAllCategories().ToList();
        }

        public List<IQuestion> GetCurrentQuestions()
        {
            return currentQuestions;
        }

        public List<IAnswer> GetAnswersOfUser(long userId)
        {
            return repository.GetAnswersOfUser(userId).ToList();
        }

        public List<IQuestion> GetQuestionsOfUser(long userId)
        {
            return repository.GetQuestionsOfUser(userId).ToList();
        }

        public List<IComment> GetCommentsOfUser(long userId)
        {
            return repository.GetCommentsOfUser(userId).ToList();
        }

        public List<ITag> GetTagsOfQuestion(long questionId)
        {
            return repository.GetTagsOfQuestion(questionId).ToList();
        }

        public void AddQuestion(string title, string content, Category category)
        {
            long userID = IDGenerator.RandomLong();
            long questionId = IDGenerator.RandomLong();
            IQuestion question = new QuestionBuilder().Begin().SetId(questionId).SetUserId(userID).SetContent(content).SetCategory(category).SetTitle(title).End();
            repository.AddQuestion(question);
        }

        public void AddQuestionByObject(IQuestion question)
        {
            repository.AddQuestion(question);
        }

        public List<IBadge> GetBadgesOfUser(long userId)
        {
            return repository.GetBadgesOfUser(userId).ToList();
        }
        public int FilterQuestionsByLast7Days()
        {
            List<IQuestion> questionsWithinLast7Days = GetAllQuestions()
                .Where(question => question.DatePosted >= DateTime.Now.AddDays(-7) && question.DatePosted <= DateTime.Now)
                .ToList();

            return questionsWithinLast7Days.Count;
        }

        public int FilterQuestionsAnsweredThisMonth()
        {
            DateTime currentDate = DateTime.Now;
            DateTime firstDayOfMonth = new (currentDate.Year, currentDate.Month, 1);
            DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            bool QuestionIsPostedWithinLastCalendarMonth(IQuestion question) => question.DatePosted >= firstDayOfMonth && question.DatePosted <= lastDayOfMonth;
            return GetAllQuestions()
                .Where(QuestionIsPostedWithinLastCalendarMonth)
                .Count();
        }

        public int FilterQuestionsAnsweredLastYear()
        {
            DateTime currentDate = DateTime.Now;
            DateTime firstDayOfLastYear = new (currentDate.Year - 1, 1, 1);
            DateTime lastDayOfLastYear = new (currentDate.Year - 1, 12, 31);
            bool QuestionIsPostedWithinPreviousCalendarYear(IQuestion question) => question.DatePosted >= firstDayOfLastYear && question.DatePosted <= lastDayOfLastYear;
            return GetAllQuestions()
                .Where(QuestionIsPostedWithinPreviousCalendarYear)
                .Count();
        }
    }
}
