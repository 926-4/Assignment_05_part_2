using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using UBB_SE_2024_Team_42.Domain;
using System.Drawing;
using System.IO;

namespace UBB_SE_2024_Team_42.Repository
{
    public class Repository
    { 
        //Data Source = CAMFRIGLACLUJ; Initial Catalog = Team42DB;Integrated Security = True
        private string sqlConnectionString = (@"Data Source = CAMFRIGLACLUJ; Initial Catalog = Team42DB;Integrated Security = True");

        //private static readonly ImageConverter imageConverter = new ImageConverter;

        // no other fields required
        // when you need something, just create public functions which insert/update/retrieve data directly
        // from the database by calling functions/procedures DEFINED IN THE DB

        public Repository(string sqlConnectionString)
        {
            //this.sqlConnectionString = sqlConnectionString;
        }

        public List<Notification> getNotificationsOfUser(long userId)
        {
            SqlConnection connection = new SqlConnection(sqlConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand("select * from dbo.getNotificationsOfUser(" + userId + ")", connection);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);

            List<Notification> notificationList = new List<Notification>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                notificationList.Add(new Notification(
                    Convert.ToInt64(dataTable.Rows[i]["id"]),
                    Convert.ToInt64(dataTable.Rows[i]["postId"]),
                    Convert.ToInt64(dataTable.Rows[i]["badgeId"])
                    ));
            }
            connection.Close();

            return notificationList;
        }

        public List<Category> getCategoriesModeratedByUser(long userId)
        {
            SqlConnection connection = new SqlConnection(sqlConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand("select * from dbo.getCategoriesModeratedByUser(" + userId + ")", connection);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);

            List<Category> categoryList = new List<Category>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                categoryList.Add(new Category(
                    Convert.ToInt64(dataTable.Rows[i]["id"]),
                    dataTable.Rows[i]["name"].ToString()
                    ));
            }
            connection.Close();

            return categoryList;
        }

        public List<Badge> getBadgesOfUser(long userId)
        {
            SqlConnection connection = new SqlConnection(sqlConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand("select * from dbo.getBadgesOfUser(" + userId + ")", connection);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);

            List<Badge> badgeList = new List<Badge>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                Image badgeImage;
                byte[] imageBytes = (byte[])dataTable.Rows[i]["image"];
        
                using (Stream stream = new MemoryStream(imageBytes))
                {
                    badgeImage = Image.FromStream(stream);
                }
                badgeList.Add(new Badge(
                    Convert.ToInt64(dataTable.Rows[i]["id"]),
                    dataTable.Rows[i]["name"].ToString(),
                    dataTable.Rows[i]["description"].ToString(),
                    badgeImage
                    ));
            }
            connection.Close();

            return badgeList;
        }


        public User getUser(long userId)
        {
            SqlConnection connection = new SqlConnection(sqlConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand("select * from dbo.getUser(" + userId + ")", connection);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);

            User user = new User(
                    userId,
                    dataTable.Rows[0]["name"].ToString(),
                    getNotificationsOfUser(userId),
                    getCategoriesModeratedByUser(userId),
                    getBadgesOfUser(userId)
                    );
        
             connection.Close();
             return user;
        }

        public List<User> getAllUsers()
        {
        SqlConnection connection = new SqlConnection(sqlConnectionString);
        connection.Open();
        SqlCommand command = new SqlCommand("select * from dbo.getAllUsers()", connection);
        SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
        DataTable dataTable = new DataTable();
        dataAdapter.Fill(dataTable);

        List<User> userList = new List<User>();
        for (int i = 0; i < dataTable.Rows.Count; i++)
        {
            long userId = Convert.ToInt64(dataTable.Rows[i]["id"]);
            userList.Add(getUser(userId));
            /*userList.Add(new User(
                userId,
                dataTable.Rows[i]["name"].ToString(),
                getNotificationsOfUser(userId),
                getCategoriesModeratedByUser(userId),
                getBadgesOfUser(userId)
                ));*/
        }
        connection.Close();

        return userList;
        }


        public List<Vote> getVotesOfPost(long postId)
        {
        SqlConnection connection = new SqlConnection(sqlConnectionString);
        connection.Open();
        SqlCommand command = new SqlCommand("select * from dbo.getVotesOfPost(" + postId + ")", connection);
        SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
        DataTable dataTable = new DataTable();
        dataAdapter.Fill(dataTable);

        List<Vote> voteList = new List<Vote>();
        for (int i = 0; i < dataTable.Rows.Count; i++)
        {
            voteList.Add(new Vote(
                Convert.ToInt32(dataTable.Rows[i]["value"]),
                Convert.ToInt64(dataTable.Rows[i]["userId"])
                ));
        }
        connection.Close();

        return voteList;
        }

        public List<Category> getAllCategories()
        {
            SqlConnection connection = new SqlConnection(sqlConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand("select * from dbo.getAllCategories()", connection);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);

            List<Category> categoryList = new List<Category>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                categoryList.Add(new Category(
                    Convert.ToInt64(dataTable.Rows[i]["id"]),
                    dataTable.Rows[i]["name"].ToString()
                    ));
            }
            connection.Close();

            return categoryList;
        }

        public List<Tag> getTagsOfQuestion(long questionId)
        {
            SqlConnection connection = new SqlConnection(sqlConnectionString);
            connection.Open();
            string commandString = "select * from dbo.getTagById(" + questionId + ")";
            SqlCommand command = new SqlCommand(commandString, connection);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);

            List<Tag> tagList = new List<Tag>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                tagList.Add(new Tag(Convert.ToInt64(dataTable.Rows[i]["id"]), dataTable.Rows[i]["name"].ToString()));
            }
            connection.Close();

            return tagList;
        }

        // Questions DB Functions
        public Question getQuestion(long questionId)
        {
            SqlConnection connection = new SqlConnection(sqlConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand("select * from dbo.getQuestionByID(" + questionId + ")", connection);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);

            DataTable dataTable = new DataTable();

            dataAdapter.Fill(dataTable);

            List<Tag> tagList = getTagsOfQuestion(questionId);
            List<Vote> voteList = getVotesOfPost(questionId);
            Category category = getCategory(Convert.ToInt64(dataTable.Rows[0]["categoryId"]));

            connection.Close();

            return new Question(Convert.ToInt64(dataTable.Rows[0]["id"]), Convert.ToInt64(dataTable.Rows[0]["userId"]),
                                dataTable.Rows[0]["title"].ToString(), category,
                                dataTable.Rows[0]["content"].ToString(), 
                                Convert.ToDateTime(dataTable.Rows[0]["datePosted"]),
                                dataTable.Rows[0]["dateOfLastEdit"]==DBNull.Value ? Convert.ToDateTime(dataTable.Rows[0]["datePosted"]) : Convert.ToDateTime(dataTable.Rows[0]["dateOfLastEdit"]), dataTable.Rows[0]["type"].ToString(),
                                voteList, tagList);
        }

        public List<Question> getAllQuestions()
        {
            SqlConnection connection = new SqlConnection(sqlConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand("select * from dbo.getAllQuestions()", connection);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);

            List<Question> questionList = new List<Question>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                questionList.Add(getQuestion(Convert.ToInt64(dataTable.Rows[i]["id"])));
            }
            connection.Close();

            return questionList;
        }

        public Category getCategory(long categoryId)
        {
            SqlConnection connection = new SqlConnection(sqlConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand("select * from dbo.getCategoryByID(" + categoryId + ")", connection);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);

            DataTable dataTable = new DataTable();

            dataAdapter.Fill(dataTable);

            connection.Close();

            return (new Category(Convert.ToInt64(dataTable.Rows[0]["id"]), dataTable.Rows[0]["name"].ToString()));
        }

        public List<Post> getRepliesOfPost(long postId)
        {
            SqlConnection connection = new SqlConnection(sqlConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand("select * from dbo.GetAllRepliesOfPost(" + postId + ")", connection);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);

            List<Post> postList = new List<Post>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                Post newPost;
                long id = Convert.ToInt64(dataTable.Rows[i]["id"]);
                long userId = Convert.ToInt64(dataTable.Rows[i]["userId"]);
                DateTime datePosted = Convert.ToDateTime(dataTable.Rows[i]["datePosted"]);
                DateTime dateOfLastEdit = dataTable.Rows[i]["dateOfLastEdit"] == DBNull.Value ? datePosted : Convert.ToDateTime(dataTable.Rows[i]["dateOfLastEdit"]);
                string type = dataTable.Rows[i]["type"].ToString();
                string content = dataTable.Rows[i]["content"].ToString();
                List<Vote> votes = getVotesOfPost(postId);
                if (type == Post.QUESTION_TYPE)
                {
                    string title = dataTable.Rows[i]["title"].ToString();
                    Category category = getCategory(Convert.ToInt64(dataTable.Rows[i]["categoryId"]));
                    List<Tag> tags = getTagsOfQuestion(postId);
                    newPost = new Question(postId, userId, title, category, content, datePosted, dateOfLastEdit, type, votes, tags);
                }
                else
                {
                    newPost = new Post(postId, userId, content, type, votes, datePosted, dateOfLastEdit);
                }

                postList.Add(newPost);
            }

            connection.Close();
            return postList;
        }

        public void addQuestion(Question question)
        {
            SqlConnection sqlConnection = new SqlConnection(sqlConnectionString);
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("addQuestion", sqlConnection);
            command.Parameters.AddWithValue("@userID", question.UserID);
            command.Parameters.AddWithValue("@content", question.Content);
            command.Parameters.AddWithValue("@title", question.Title);
            command.Parameters.AddWithValue("@categoryId", question.Category.CategoryID);
            command.CommandType = CommandType.StoredProcedure;
            command.ExecuteNonQuery();
            sqlConnection.Close();

        }

        public void updateQuestion(Question oldQuestion, Question newQuestion)
        {
            SqlConnection sqlConnection = new SqlConnection(sqlConnectionString);
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("updateQuestion", sqlConnection);
            command.Parameters.AddWithValue("@questionId", oldQuestion.PostID);
            command.Parameters.AddWithValue("@content", newQuestion.Content);
            command.Parameters.AddWithValue("@title", newQuestion.Title);
            command.Parameters.AddWithValue("@categoryId", newQuestion.Category);
            command.CommandType = CommandType.StoredProcedure;
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public void addPostAndReply(Post post, Post postRepliedOn)
        {
            SqlConnection connection = new SqlConnection(sqlConnectionString);
            connection.Open();
            SqlCommand command = null;
            SqlCommand reply_command = null;


            if (post.PostType == Post.ANSWER_TYPE)
            {
                command = new SqlCommand("addAnswer", connection);
                command.Parameters.AddWithValue("@userId", post.UserID);
                command.Parameters.AddWithValue("@content", post.Content);
                command.Parameters.AddWithValue("@postId", post.PostID);
            }
            else if (post.PostType == Post.COMMENT_TYPE)
            {
                command = new SqlCommand("addComment", connection);
                command.Parameters.AddWithValue("@userId", post.UserID);
                command.Parameters.AddWithValue("@content", post.Content);
                command.Parameters.AddWithValue("@postId", post.PostID);
            }

            reply_command = new SqlCommand("addReply", connection);
            reply_command.Parameters.AddWithValue("@idOfPostRepliedOn", postRepliedOn.PostID);
            reply_command.Parameters.AddWithValue("@idOfReply", post.PostID);
            if (command != null)
            {
                command.CommandType = CommandType.StoredProcedure;
                reply_command.CommandType = CommandType.StoredProcedure;
                command.ExecuteNonQuery();
                reply_command.ExecuteNonQuery();
            }

            connection.Close();

        }

        public void updatePost(Post oldPost, Post newPost)
        {
            SqlConnection connection = new SqlConnection(sqlConnectionString);
            connection.Open();
            SqlCommand command = null;

            if (oldPost.PostType == Post.ANSWER_TYPE)
            {
                command = new SqlCommand("updateAnswer", connection);
                command.Parameters.AddWithValue("@answerId", oldPost.PostID);
                command.Parameters.AddWithValue("@content", newPost.Content);



            }
            else if (oldPost.PostType == Post.COMMENT_TYPE)
            {
                command = new SqlCommand("updateComment", connection);
                command.Parameters.AddWithValue("@commentId", oldPost.PostID);
                command.Parameters.AddWithValue("@content", newPost.Content);

            }

            if (command != null)
            {
                command.CommandType = CommandType.StoredProcedure;
                command.ExecuteNonQuery();
            }
            connection.Close();

        }
        public List<Post> getAnswersOfUser(long userId)
        {
            SqlConnection connection = new SqlConnection(sqlConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand("select * from dbo.getPostsByUserId(" + userId + ")", connection);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);

            List<Post> answerList = new List<Post>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                string type = dataTable.Rows[i]["type"].ToString();
                List<Vote> voteList = getVotesOfPost(Convert.ToInt64(dataTable.Rows[i]["id"]));
                if (type == Post.ANSWER_TYPE)

                    answerList.Add(new Post(Convert.ToInt64(dataTable.Rows[i]["id"]), Convert.ToInt64(dataTable.Rows[i]["userID"]),
                                          dataTable.Rows[i]["content"].ToString(), type, voteList,
                                          Convert.ToDateTime(dataTable.Rows[i]["datePosted"]), Convert.ToDateTime(dataTable.Rows[i]["dateOfLastEdit"])));
            }

            connection.Close();

            return answerList;
        
        }
        public List<Post> getCommentsOfUser(long userId)
        {
            SqlConnection connection = new SqlConnection(sqlConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand("select * from dbo.getPostsByUserId(" + userId + ")", connection);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);

            List<Post> commentList = new List<Post>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                string type = dataTable.Rows[i]["type"].ToString();
                List<Vote> voteList = getVotesOfPost(Convert.ToInt64(dataTable.Rows[i]["id"]));
                if (type == Post.COMMENT_TYPE)
                {
                    DateTime datePosted = Convert.ToDateTime(dataTable.Rows[i]["datePosted"]);
                    DateTime dateOfLastEdit;
                    try
                    {
                        dateOfLastEdit = Convert.ToDateTime(dataTable.Rows[i]["dateOfLastEdit"]);
                        
                    }
                    catch (Exception e)
                    {
                        dateOfLastEdit = DateTime.Today;
                    }
                    commentList.Add(new Post(Convert.ToInt64(dataTable.Rows[i]["id"]), Convert.ToInt64(dataTable.Rows[i]["userID"]),
                                          dataTable.Rows[i]["content"].ToString(), type, voteList,
                                          Convert.ToDateTime(dataTable.Rows[i]["datePosted"]), dataTable.Rows[i]["dateOfLastEdit"] == DBNull.Value ? Convert.ToDateTime(dataTable.Rows[i]["datePosted"]) : Convert.ToDateTime(dataTable.Rows[i]["dateOfLastEdit"])));
                }
            }
            connection.Close();

            return commentList;
        }

        public List<Question> getQuestionsOfUser(long userId)
        {
            SqlConnection connection = new SqlConnection(sqlConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand("select * from dbo.getPostsByUserId(" + userId + ")", connection);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);

            List<Question> questionList = new List<Question>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                string type = dataTable.Rows[i]["type"].ToString();
                List<Vote> voteList = getVotesOfPost(Convert.ToInt64(dataTable.Rows[i]["id"]));
                if (type == Post.QUESTION_TYPE)
                {
                    List<Tag> tagList = getTagsOfQuestion(Convert.ToInt64(dataTable.Rows[i]["id"]));
                    Category category = getCategory(Convert.ToInt64(dataTable.Rows[i]["categoryId"]));

                    questionList.Add(new Question(Convert.ToInt64(dataTable.Rows[i]["id"]), Convert.ToInt64(dataTable.Rows[i]["userId"]),
                                dataTable.Rows[i]["title"].ToString(), category,
                                dataTable.Rows[i]["content"].ToString(),
                                Convert.ToDateTime(dataTable.Rows[i]["datePosted"]),
                                dataTable.Rows[i]["dateOfLastEdit"] == DBNull.Value ? Convert.ToDateTime(dataTable.Rows[i]["datePosted"]) : Convert.ToDateTime(dataTable.Rows[i]["dateOfLastEdit"]), dataTable.Rows[i]["type"].ToString(),
                                voteList, tagList));
                    
                }
            }
            //questionList.Add(new Question(8, 3, "question", new Category(8, "category"), "content", new DateTime(), new DateTime(), "type", new List<Vote>(), new List<Tag>() ));
            connection.Close();
            return questionList;
        }
    }
}
