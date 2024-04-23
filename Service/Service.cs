using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBB_SE_2024_Team_42.Domain;
using UBB_SE_2024_Team_42.GUI;
using UBB_SE_2024_Team_42.Repository;

namespace UBB_SE_2024_Team_42.Service
{
    public class Service
    {
        private Repository.Repository repository;

        List<Question> currentQuestions;

        // no other fields required for now

        public Service(Repository.Repository repository)
        {
            this.repository = repository;
            currentQuestions = getAllQuestions();
        }

        public User getUser(long userId)
        {
            return repository.getUser(userId);
        }

        public List<Question> getAllQuestions()
        {
            return repository.getAllQuestions();
        }

        public List<Post> getRepliesOfPost(long postId)
        {
            return repository.getRepliesOfPost(postId);
        }

        public List<Question> getQuestionsOfCategory(Category category)
        {
            List<Question> questions = repository.getAllQuestions();
            List<Question> filteredQuestions = new List<Question>();

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
            List<Question> questions = repository.getAllQuestions();
            List<Question> filteredQuestions = new List<Question>();

            foreach (Question question in questions)
            {
                foreach (Post post in repository.getRepliesOfPost(question.PostID))
                {
                    if (post.PostType == Post.ANSWER_TYPE)
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
            List<Question> questions = repository.getAllQuestions();
            List<Question> filteredQuestions = new List<Question>();

            foreach (Question question in questions)
            {
                bool addedQuestionToList = false;

                foreach (Tag tag in question.Tags)
                {
                    if (textToBeSearchedBy.Contains(tag.TagName))
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
                List<Vote> votesForQuestion = this.repository.getVotesOfPost(questionId);
                int voteCount = getVoteScore(votesForQuestion);
                hash[question] = voteCount;
            }

            //sortedListOfQuestions = hash.OrderBy(x => x.Value);
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

        private int getVoteScore(List<Vote> voteList)
        {
            int score = 0;
            for (int i = 0; i < voteList.Count; i++)
            {
                score += voteList[i].VoteValue;
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
                List<Post> repliesFromPost = this.repository.getRepliesOfPost(questionId);
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
            Dictionary<Question, DateTime> hash = new Dictionary<Question, DateTime>();
            List<Question> listOfQuestions = currentQuestions;
            List<Question> sortedListOfQuestions;
            foreach (Question question in listOfQuestions)
            {
                //long questionId = question.PostID;
                hash[question] = question.datePosted;
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
            return repository.getAllCategories();
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
            return this.repository.getTagsOfQuestion(questionId);
        }



        public void addQuestion(string title, string content, Category category)
        {
            Question question = new Question(0, 1, title, category, content, new DateTime(), new DateTime(), "question", null, null);
            repository.addQuestion(question);

        }




        public List<Badge> getBadgesOfUser(long userId)
        {
            return this.repository.getBadgesOfUser(userId);
        }
    }
}
