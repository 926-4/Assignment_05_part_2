using UBB_SE_2024_Team_42.Domain;
using UBB_SE_2024_Team_42.Domain.badge;
using UBB_SE_2024_Team_42.Domain.category;
using UBB_SE_2024_Team_42.Domain.Posts;
using UBB_SE_2024_Team_42.Domain.Reactions;
using UBB_SE_2024_Team_42.Domain.tag;
using UBB_SE_2024_Team_42.Domain.user;

namespace UBB_SE_2024_Team_42.Service
{
    public class Service
    {
        private readonly Repository.Repository repository;

        List<Question> currentQuestions;


        public Service(Repository.Repository repository)
        {
            this.repository = repository;
            currentQuestions = GetAllQuestions();
        }

        public User GetUser(long userId)
        {
            return repository.GetUser(userId);
        }

        public List<Question> GetAllQuestions()
        {
            return repository.GetAllQuestions();
        }

        public List<IPost> GetRepliesOfPost(long postId)
        {
            return repository.GetRepliesOfPost(postId);
        }

        public List<Question> getQuestionsOfCategory(Category category)
        {
            List<Question> questions = repository.GetAllQuestions();
            List<Question> filteredQuestions = [];

            foreach (Question question in questions)
            {
                if (question.Category.CategoryName == category.CategoryName)
                    filteredQuestions.Add(question);
            }
            currentQuestions = filteredQuestions;
            return filteredQuestions;
        }

        public List<Question> getQuestionsWithAtLeastOneAnswer()
        {
            List<Question> questions = repository.GetAllQuestions();
            List<Question> filteredQuestions = new List<Question>();

            foreach (Question question in questions)
            {
                foreach (Post post in repository.GetRepliesOfPost(question.PostID))
                {
                    if (post is Answer)
                    {
                        filteredQuestions.Add(question);
                        break;
                    }
                }
            }
            //currentQuestions = filteredQuestions;
            return filteredQuestions;
        }

        public List<Question> searchQuestion(string textToBeSearchedBy)
        {
            List<Question> questions = repository.GetAllQuestions();
            List<Question> filteredQuestions = new List<Question>();

            foreach (Question question in questions)
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
                    string[] keywords = question.Title.Split(' ');
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
            currentQuestions = filteredQuestions;
            return filteredQuestions;
        }

        public List<Question> sortQuestionsByScoreAscending()
        {
            Dictionary<Question, int> hash = new Dictionary<Question, int>();
            List<Question> listOfQuestions = currentQuestions;
            List<Question> sortedListOfQuestions;
            foreach (Question question in listOfQuestions)
            {
                long questionId = question.PostID;
                List<Reaction> votesForQuestion = this.repository.GetVotesOfPost(questionId);
                int voteCount = getVoteScore(votesForQuestion);
                hash[question] = voteCount;
            }

            //sortedListOfQuestions = hash.OrderBy(x => x.ReactionValue);
            hash.OrderBy(x => x.Value);
            sortedListOfQuestions = hash.Keys.ToList();

            return sortedListOfQuestions;
        }

        public List<Question> sortQuestionsByScoreDescending()
        {
            List<Question> questions = sortQuestionsByScoreAscending();
            questions.Reverse();
            return questions;
        }

        private int getVoteScore(List<Reaction> voteList)
        {
            int score = 0;
            for (int i = 0; i < voteList.Count; i++)
            {
                score += voteList[i].ReactionValue;
            }
            return score;
        }

        public List<Question> sortQuestionsByNumberOfAnswersAscending()
        {
            Dictionary<Question, int> hash = new Dictionary<Question, int>();
            List<Question> listOfQuestions = currentQuestions;
            List<Question> sortedListOfQuestions;
            foreach (Question question in listOfQuestions)
            {
                int numberOfAnswers = 0;
                long questionId = question.PostID;
                List<Post> repliesFromPost = this.repository.GetRepliesOfPost(questionId);
                foreach (Post post in repliesFromPost)
                {
                    if (post.PostType == Post.ANSWER_TYPE)
                    {
                        numberOfAnswers += 1;
                    }
                }
                hash[question] = numberOfAnswers;
            }

            hash.OrderBy(x => x.Value);
            sortedListOfQuestions = hash.Keys.ToList();
            return sortedListOfQuestions;
        }

        public List<Question> sortQuestionsByNumberOfAnswersDescending()
        {
            List<Question> questions = sortQuestionsByNumberOfAnswersAscending();
            questions.Reverse();
            return questions;
        }

        public List<Question> sortQuestionsByDateAscending()
        {
            Dictionary<Question, DateTime> hash = [];
            List<Question> listOfQuestions = currentQuestions;
            List<Question> sortedListOfQuestions;
            foreach (Question question in listOfQuestions)
            {
                //long questionId = question.PostID;
                hash[question] = question.DatePosted;
            }

            hash.OrderBy(x => x.Value);
            sortedListOfQuestions = hash.Keys.ToList();
            return sortedListOfQuestions;
        }

        public List<Question> sortQuestionsByDateDescending()
        {
            List<Question> questions = sortQuestionsByDateAscending();
            questions.Reverse();
            return questions;
        }

        public List<Category> getAllCategories()
        {
            return repository.GetAllCategories();
        }

        public List<Question> getCurrentQuestions()
        {
            return currentQuestions;
        }

        public List<Post> getAnswersOfUser(long userId)
        {
            return this.repository.getAnswersOfUser(userId);
        }

        public List<Question> getQuestionsOfUser(long userId)
        {
            return this.repository.getQuestionsOfUser(userId);
        }

        public List<Post> getCommentsOfUser(long userId)
        {
            return this.repository.getCommentsOfUser(userId);
        }

        public List<Tag> getTagsOfQuestion(long questionId)
        {
            return this.repository.GetTagsOfQuestion(questionId);
        }



        public void addQuestion(string title, string content, Category category)
        {
            Question question = new Question(0, 1, title, category, content, new DateTime(), new DateTime(), "question", null, null);
            repository.AddQuestion(question);

        }




        public List<Badge> getBadgesOfUser(long userId)
        {
            return repository.GetBadgesOfUser(userId);
        }
    }
}
