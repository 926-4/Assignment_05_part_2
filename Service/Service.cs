using UBB_SE_2024_Team_42.Domain;
using UBB_SE_2024_Team_42.Domain.badge;
using UBB_SE_2024_Team_42.Domain.category;
using UBB_SE_2024_Team_42.Domain.Posts;
using UBB_SE_2024_Team_42.Domain.Reactions;
using UBB_SE_2024_Team_42.Domain.tag;
using UBB_SE_2024_Team_42.Domain.user;
using UBB_SE_2024_Team_42.Utils;
using UBB_SE_2024_Team_42.Utils.functionbros;

namespace UBB_SE_2024_Team_42.Service
{
    public class Service
    {
        private readonly Repository.Repository repository;

        List<IQuestion> currentQuestions;


        public Service(Repository.Repository repository)
        {
            this.repository = repository;
            currentQuestions = GetAllQuestions();
        }

        public IUser GetUser(long userId)
        {
            return repository.GetUser(userId);
        }

        public List<IQuestion> GetAllQuestions()
        {
            return repository.GetAllQuestions();
        }

        public List<IPost> GetRepliesOfPost(long postId)
        {
            return repository.GetRepliesOfPost(postId);
        }

        public List<IQuestion> GetQuestionsOfCategory(ICategory category)
        {
            List<IQuestion> questions = repository.GetAllQuestions();
            List<IQuestion> filteredQuestions = [];

            foreach (IQuestion question in questions)
            {
                if ((question.Category?.CategoryName ?? "") == category.CategoryName)
                    filteredQuestions.Add(question);
            }
            return filteredQuestions;
        }

        public List<IQuestion> GetQuestionsWithAtLeastOneAnswer()
        {
            static bool isAnswer(IPost ipost) => (ipost is Answer);
            bool questionHasAtLeastOneAnswer(IQuestion question) => repository.GetRepliesOfPost(question.PostID).Any(isAnswer);
            List<IQuestion> filteredQuestions = repository.GetAllQuestions().Where(questionHasAtLeastOneAnswer).ToList();
            return filteredQuestions;
        }

        public List<IQuestion> FindQuestionsByPartialStringInAnyField(string textToBeSearchedBy)
        {
            List<IQuestion> questions = repository.GetAllQuestions();
            List<IQuestion> filteredQuestions = [];

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
                    string[] keywords = question.Title?.Split(' ') ?? [];
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
            static int getReactionValue(IReaction ireaction) => ireaction.ReactionValue;
            Dictionary<IQuestion, int> questionToReactionValueMap = [];
            List<IQuestion> listOfQuestions = currentQuestions;
            CollectionSummer<IReaction> reactionValueSummer = new(getReactionValue);


            void addMappingForQuestion(IQuestion question) =>
                questionToReactionValueMap[question] = GetReactionScore(repository.GetVotesOfPostByPostID(question.PostID));

            listOfQuestions.ForEach(addMappingForQuestion);

            Dictionary<IQuestion, int> sortedQuestionToReactionValueMap =
                questionToReactionValueMap.OrderBy(questionValuePair => questionValuePair.Value).ToDictionary();

            return [.. sortedQuestionToReactionValueMap.Keys];
        }

        public List<IQuestion> GetQuestionsSortedByScoreDescending()
        {
            List<IQuestion> questions = GetQuestionsSortedByScoreAscending();
            questions.Reverse();
            return questions;
        }

        private static int GetReactionScore(List<IReaction> voteList)
        {
            static int getReactionValue(IReaction ireaction) => ireaction.ReactionValue;
            CollectionReducer<IReaction, int> summer = new(
                mapper: getReactionValue,
                folder: (x, y) => x + y,
                defaultResult: 0);
            return summer.MapThenFold(voteList);
        }

        public List<IQuestion> SortQuestionsByNumberOfAnswersAscending()
        {
            Dictionary<IQuestion, int> hash = [];
            List<IQuestion> listOfQuestions = currentQuestions;
            List<IQuestion> sortedListOfQuestions;
            foreach (IQuestion question in listOfQuestions)
            {
                int numberOfAnswers = 0;
                long questionId = question.PostID;
                List<IPost> repliesFromPost = repository.GetRepliesOfPost(questionId);
                foreach (IPost ipost in repliesFromPost)
                {
                    if (ipost is Answer)
                    {
                        numberOfAnswers += 1;
                    }
                }
                hash[question] = numberOfAnswers;
            }

            hash.OrderBy(x => x.Value);
            sortedListOfQuestions = [.. hash.Keys];
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
            Dictionary<IQuestion, DateTime> hash = [];
            List<IQuestion> listOfQuestions = currentQuestions;
            List<IQuestion> sortedListOfQuestions;
            foreach (IQuestion question in listOfQuestions)
            {
                hash[question] = question.DatePosted;
            }

            hash.OrderBy(x => x.Value);
            sortedListOfQuestions = [.. hash.Keys];
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
            return repository.GetAllCategories();
        }

        public List<IQuestion> getCurrentQuestions()
        {
            return currentQuestions;
        }

        public List<IPost> GetAnswersOfUser(long userId)
        {
            return repository.GetAnswersOfUser(userId);
        }

        public List<IQuestion> GetQuestionsOfUser(long userId)
        {
            return repository.GetQuestionsOfUser(userId);
        }

        public List<IPost> GetCommentsOfUser(long userId)
        {
            return repository.GetCommentsOfUser(userId);
        }

        public List<ITag> GetTagsOfQuestion(long questionId)
        {
            return repository.GetTagsOfQuestion(questionId);
        }



        public void AddQuestion(string title, string content, Category category)
        {
            long userID = IDGenerator.RandomLong();
            Question question = new(userID, content, category, title); 
            repository.AddQuestion(question);

        }




        public List<IBadge> getBadgesOfUser(long userId)
        {
            return repository.GetBadgesOfUser(userId);
        }
    }
}
