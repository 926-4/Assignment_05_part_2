﻿using UBB_SE_2024_Team_42.Domain.Badge;
using UBB_SE_2024_Team_42.Domain.Category;
using UBB_SE_2024_Team_42.Domain.Post;
using UBB_SE_2024_Team_42.Domain.Post.Interfaces;
using UBB_SE_2024_Team_42.Domain.Posts;
using UBB_SE_2024_Team_42.Domain.Reactions;
using UBB_SE_2024_Team_42.Domain.Tag;
using UBB_SE_2024_Team_42.Domain.User;
using UBB_SE_2024_Team_42.Utils;
using UBB_SE_2024_Team_42.Utils.Functionals;

namespace UBB_SE_2024_Team_42.Service
{
    public class Service
    {
        private readonly Repository.Repository repository;

        private readonly List<IQuestion> currentQuestions;
        public Service(Repository.Repository repository)
        {
            this.repository = repository;
            currentQuestions = GetAllQuestions();
        }

        public IUser GetUser(long userId)
        {
            return repository.GetUser(userId);
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

        public List<IPost> GetRepliesOfPost(long postId)
        {
            return repository.GetRepliesOfPost(postId).ToList();
        }

        public List<IQuestion> GetQuestionsOfCategory(ICategory? category)
        {
            if (category == null)
            {
                return new List<IQuestion>();
            }
            List<IQuestion> questions = repository.GetAllQuestions().ToList();
            List<IQuestion> filteredQuestions = new ();

            foreach (IQuestion question in questions)
            {
                if ((question.Category?.Name ?? string.Empty) == category.Name)
                {
                    filteredQuestions.Add(question);
                }
            }
            return filteredQuestions;
        }

        public List<IQuestion> GetQuestionsWithAtLeastOneAnswer()
        {
            static bool IsAnswer(IPost ipost) => ipost is Answer;
            bool QuestionHasAtLeastOneAnswer(IQuestion question) => repository.GetRepliesOfPost(question.ID).Any(IsAnswer);
            List<IQuestion> filteredQuestions = repository.GetAllQuestions().Where(QuestionHasAtLeastOneAnswer).ToList();
            return filteredQuestions;
        }

        public List<IQuestion> FindQuestionsByPartialStringInAnyField(string textToBeSearchedBy)
        {
            List<IQuestion> questions = repository.GetAllQuestions().ToList();
            List<IQuestion> filteredQuestions = new ();

            foreach (IQuestion question in questions)
            {
                bool addedQuestionToList = false;

                foreach (ITag tag in question.Tags)
                {
                    if (textToBeSearchedBy.Contains(tag.Name))
                    {
                        filteredQuestions.Add(question);
                        addedQuestionToList = true;
                        break;
                    }
                }

                if (!addedQuestionToList)
                {
                    string[] keywords = question.Title?.Split(' ') ?? Array.Empty<string>();
                    foreach (string keyword in keywords)
                    {
                        if (textToBeSearchedBy.Contains(keyword))
                        {
                            filteredQuestions.Add(question);
                            break;
                        }
                    }
                }
            }
            return filteredQuestions;
        }

        public List<IQuestion> GetQuestionsSortedByScoreAscending()
        {
            static int GetReactionValue(IReaction ireaction) => ireaction.Value;

            Dictionary<IQuestion, int> questionToReactionValueMap = new ();

            List<IQuestion> listOfQuestions = currentQuestions;
            CollectionSummer<IReaction> reactionValueSummer = new (GetReactionValue);
            void AddMappingForQuestion(IQuestion question) =>
                questionToReactionValueMap[question] = GetReactionScore(repository.GetReactionsOfPostByPostID(question.ID).ToList());

            listOfQuestions.ForEach(AddMappingForQuestion);

            Dictionary<IQuestion, int> sortedQuestionToReactionValueMap =
                questionToReactionValueMap.OrderBy(questionValuePair => questionValuePair.Value).ToDictionary();

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
            Dictionary<IQuestion, int> hash = new ();
            List<IQuestion> listOfQuestions = currentQuestions;
            List<IQuestion> sortedListOfQuestions;
            foreach (IQuestion question in listOfQuestions)
            {
                int numberOfAnswers = 0;
                long questionId = question.ID;
                List<IPost> repliesFromPost = repository.GetRepliesOfPost(questionId).ToList();
                foreach (IPost ipost in repliesFromPost)
                {
                    if (ipost is Answer)
                    {
                        numberOfAnswers += 1;
                    }
                }
                hash[question] = numberOfAnswers;
            }

            var sortedMap = hash.OrderBy(x => x.Value).ToDictionary();
            sortedListOfQuestions = sortedMap.Keys.ToList();
            return sortedListOfQuestions;
        }

        public List<IQuestion> SortQuestionsByNumberOfAnswersDescending()
        {
            List<IQuestion> questions = SortQuestionsByNumberOfAnswersAscending();
            questions.Reverse();
            return questions;
        }

        public List<IQuestion> SortQuestionsByDateAscending()
        {
            Dictionary<IQuestion, DateTime> hash = new ();
            List<IQuestion> listOfQuestions = currentQuestions;
            List<IQuestion> sortedListOfQuestions;
            foreach (IQuestion question in listOfQuestions)
            {
                hash[question] = question.DatePosted;
            }

            Dictionary<IQuestion, DateTime> sortedMap = hash.OrderBy(x => x.Value).ToDictionary();
            sortedListOfQuestions = sortedMap.Keys.ToList();
            return sortedListOfQuestions;
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
            Question question = new (userID, content, category, title);
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

            List<IQuestion> questionsAnsweredThisMonth = GetAllQuestions()
                .Where(question => question.DatePosted >= firstDayOfMonth && question.DatePosted <= lastDayOfMonth)
                .ToList();

            return questionsAnsweredThisMonth.Count;
        }

        public int FilterQuestionsAnsweredLastYear()
        {
            DateTime currentDate = DateTime.Now;
            DateTime firstDayOfLastYear = new (currentDate.Year - 1, 1, 1);
            DateTime lastDayOfLastYear = new (currentDate.Year - 1, 12, 31);

            List<IQuestion> questionsAnsweredLastYear = GetAllQuestions()
                .Where(question => question.DatePosted >= firstDayOfLastYear && question.DatePosted <= lastDayOfLastYear)
                .ToList();

            return questionsAnsweredLastYear.Count;
        }
    }
}
