using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using UBB_SE_2024_Team_42.Domain;
using UBB_SE_2024_Team_42.Domain.badge;
using UBB_SE_2024_Team_42.Domain.category;
using UBB_SE_2024_Team_42.Domain.notification;
using UBB_SE_2024_Team_42.Domain.Posts;
using UBB_SE_2024_Team_42.Domain.Reactions;
using UBB_SE_2024_Team_42.Domain.tag;
using UBB_SE_2024_Team_42.Domain.user;
using static UBB_SE_2024_Team_42.Domain.Posts.PostFactory;

namespace UBB_SE_2024_Team_42.Repository
{
    public class Repository
    {
        //Data Source = CAMFRIGLACLUJ; Initial Catalog = Team42DB;Integrated Security = True
        private readonly string sqlConnectionString = (@"Data Source = CAMFRIGLACLUJ; Initial Catalog = Team42DB;Integrated Security = True");

        //private static readonly ImageConverter imageConverter = new ImageConverter;

        // no other fields required
        // when you need something, just create public functions which insert/update/retrieve data directly
        // from the database by calling functions/procedures DEFINED IN THE DB


        public List<Notification> GetNotificationsOfUser(long userId)
        {
            SqlConnection connection = new(sqlConnectionString);
            connection.Open();
            SqlCommand command = new("select * from dbo.getNotificationsOfUser(" + userId + ")", connection);
            SqlDataAdapter dataAdapter = new(command);
            DataTable dataTable = new();
            dataAdapter.Fill(dataTable);

            List<Notification> notificationList = [];
            foreach (DataRow row in dataTable.Rows)
            {
                notificationList.Add(
                    new Notification(
                        Convert.ToInt64(row["id"]),
                        Convert.ToInt64(row["postId"]),
                        Convert.ToInt64(row["badgeId"])
                    ));
            }
            //for (int i = 0; i < dataTable.Rows.Count; i++)
            //{
            //    notificationList.Add(new Notification(
            //        Convert.ToInt64(dataTable.Rows[i]["id"]),
            //        Convert.ToInt64(dataTable.Rows[i]["postId"]),
            //        Convert.ToInt64(dataTable.Rows[i]["badgeId"])
            //        ));
            //}
            connection.Close();

            return notificationList;
        }

        public List<Category> GetCategoriesModeratedByUser(long userId)
        {
            SqlConnection connection = new(sqlConnectionString);
            connection.Open();
            SqlCommand command = new("select * from dbo.getCategoriesModeratedByUser(" + userId + ")", connection);
            SqlDataAdapter dataAdapter = new(command);
            DataTable dataTable = new();
            dataAdapter.Fill(dataTable);

            List<Category> categoryList = [];
            foreach (DataRow row in dataTable.Rows)
            {
                categoryList.Add(
                    new Category(
                        Convert.ToInt64(row["id"]),
                        row["name"]?.ToString() ?? ""
                    ));
            }
            //for (int i = 0; i < dataTable.Rows.Count; i++)
            //{
            //    categoryList.Add(new Category(
            //        Convert.ToInt64(dataTable.Rows[i]["id"]),
            //        dataTable.Rows[i]["name"].ToString()
            //        ));
            //}
            connection.Close();
            return categoryList;
        }

        public List<Badge> GetBadgesOfUser(long userId)
        {
            SqlConnection connection = new(sqlConnectionString);
            connection.Open();
            SqlCommand command = new("select * from dbo.getBadgesOfUser(" + userId + ")", connection);
            SqlDataAdapter dataAdapter = new(command);
            DataTable dataTable = new();
            dataAdapter.Fill(dataTable);

            List<Badge> badgeList = [];
            foreach (DataRow row in dataTable.Rows)
            {
                Image badgeImage;
                byte[] imageBytes = (byte[])row["image"];

                using (Stream stream = new MemoryStream(imageBytes))
                {
                    badgeImage = Image.FromStream(stream);
                }
                badgeList.Add(
                    new Badge(
                        Convert.ToInt64(row["id"]),
                        row["name"]?.ToString() ?? "",
                        row["description"]?.ToString() ?? "",
                        badgeImage
                    ));
            }
            //for (int i = 0; i < dataTable.Rows.Count; i++)
            //{
            //    Image badgeImage;
            //    byte[] imageBytes = (byte[])dataTable.Rows[i]["image"];

            //    using (Stream stream = new MemoryStream(imageBytes))
            //    {
            //        badgeImage = Image.FromStream(stream);
            //    }
            //    badgeList.Add(new Badge(
            //        Convert.ToInt64(dataTable.Rows[i]["id"]),
            //        dataTable.Rows[i]["name"].ToString(),
            //        dataTable.Rows[i]["description"].ToString(),
            //        badgeImage
            //        ));
            //}
            connection.Close();

            return badgeList;
        }


        public User GetUser(long userId)
        {
            SqlConnection connection = new(sqlConnectionString);
            connection.Open();
            SqlCommand command = new("select * from dbo.getUser(" + userId + ")", connection);
            SqlDataAdapter dataAdapter = new(command);
            DataTable dataTable = new();
            dataAdapter.Fill(dataTable);

            User user = new(
                            userId,
                            dataTable.Rows[0]["name"]?.ToString() ?? "",
                            GetNotificationsOfUser(userId),
                            GetCategoriesModeratedByUser(userId),
                            GetBadgesOfUser(userId)
                        );

            connection.Close();
            return user;
        }

        public List<User> GetAllUsers()
        {
            SqlConnection connection = new(sqlConnectionString);
            connection.Open();
            SqlCommand command = new("select * from dbo.getAllUsers()", connection);
            SqlDataAdapter dataAdapter = new(command);
            DataTable dataTable = new();
            dataAdapter.Fill(dataTable);

            List<User> userList = [];
            foreach (DataRow row in dataTable.Rows)
            {
                long userId = Convert.ToInt64(row["id"]);
                userList.Add(GetUser(userId));
            }
            //for (int i = 0; i < dataTable.Rows.Count; i++)
            //{
            //    long userId = Convert.ToInt64(dataTable.Rows[i]["id"]);
            //    userList.Add(GetUser(userId));
            //    /*userList.Add(new User(
            //        userId,
            //        dataTable.Rows[i]["name"].ToString(),
            //        getNotificationsOfUser(userId),
            //        getCategoriesModeratedByUser(userId),
            //        getBadgesOfUser(userId)
            //        ));*/
            //}
            connection.Close();

            return userList;
        }


        public List<IReaction> GetVotesOfPost(long postId)
        {
            SqlConnection connection = new(sqlConnectionString);
            connection.Open();
            SqlCommand command = new("select * from dbo.getVotesOfPost(" + postId + ")", connection);
            SqlDataAdapter dataAdapter = new(command);
            DataTable dataTable = new();
            dataAdapter.Fill(dataTable);

            List<IReaction> voteList = [];
            foreach (DataRow row in dataTable.Rows)
            {
                voteList.Add(
                    new Reaction(
                        Convert.ToInt32(row["value"]),
                        Convert.ToInt64(row["userId"])
                    ));
            }
            //for (int i = 0; i < dataTable.Rows.Count; i++)
            //{
            //    voteList.Add(new Reaction(
            //        Convert.ToInt32(dataTable.Rows[i]["value"]),
            //        Convert.ToInt64(dataTable.Rows[i]["userId"])
            //        ));
            //}
            connection.Close();

            return voteList;
        }

        public List<ICategory> GetAllCategories()
        {
            SqlConnection connection = new(sqlConnectionString);
            connection.Open();
            SqlCommand command = new("select * from dbo.getAllCategories()", connection);
            SqlDataAdapter dataAdapter = new(command);
            DataTable dataTable = new();
            dataAdapter.Fill(dataTable);

            List<ICategory> categoryList = [];
            foreach (DataRow row in dataTable.Rows)
            {
                categoryList.Add(new Category(
                    Convert.ToInt64(row["id"]),
                    row["name"]?.ToString() ?? ""
                    ));
            }
            //for (int i = 0; i < dataTable.Rows.Count; i++)
            //{
            //    categoryList.Add(new Category(
            //        Convert.ToInt64(dataTable.Rows[i]["id"]),
            //        dataTable.Rows[i]["name"].ToString()
            //        ));
            //}
            connection.Close();

            return categoryList;
        }

        public List<ITag> GetTagsOfQuestion(long questionId)
        {
            SqlConnection connection = new(sqlConnectionString);
            connection.Open();
            string commandString = "select * from dbo.getTagById(" + questionId + ")";
            SqlCommand command = new(commandString, connection);
            SqlDataAdapter dataAdapter = new(command);
            DataTable dataTable = new();
            dataAdapter.Fill(dataTable);

            List<ITag> tagList = [];
            foreach (DataRow row in dataTable.Rows)
            {
                tagList.Add(new Tag(Convert.ToInt64(row["id"]), row["name"]?.ToString() ?? ""));
            }
            //for (int i = 0; i < dataTable.Rows.Count; i++)
            //{
            //    tagList.Add(new Tag(Convert.ToInt64(dataTable.Rows[i]["id"]), dataTable.Rows[i]["name"].ToString()));
            //}
            connection.Close();

            return tagList;
        }

        // Questions DB Functions
        public Question GetQuestion(long questionId)
        {
            SqlConnection connection = new(sqlConnectionString);
            connection.Open();
            SqlCommand command = new("select * from dbo.getQuestionByID(" + questionId + ")", connection);
            SqlDataAdapter dataAdapter = new(command);

            DataTable dataTable = new();

            dataAdapter.Fill(dataTable);
            DataRow firstRow = dataTable.Rows[0];

            List<ITag> tagList = GetTagsOfQuestion(questionId);
            List<IReaction> voteList = GetVotesOfPost(questionId);
            ICategory category = GetCategory(Convert.ToInt64(firstRow["categoryId"]));

            connection.Close();
            return new Question(
                Convert.ToInt64(firstRow["id"]),
                                firstRow["title"]?.ToString() ?? "",
                category,
                tagList,
                Convert.ToInt64(firstRow["userId"]),
                                firstRow["content"]?.ToString() ?? "",
             Convert.ToDateTime(firstRow["datePosted"]),
                                firstRow["dateOfLastEdit"] == DBNull.Value
                                    ? Convert.ToDateTime(firstRow["datePosted"])
                                    : Convert.ToDateTime(firstRow["dateOfLastEdit"]),
                                voteList);
        }

        public List<Question> GetAllQuestions()
        {
            SqlConnection connection = new(sqlConnectionString);
            connection.Open();
            SqlCommand command = new("select * from dbo.getAllQuestions()", connection);
            SqlDataAdapter dataAdapter = new(command);
            DataTable dataTable = new();
            dataAdapter.Fill(dataTable);

            List<Question> questionList = [];
            foreach (DataRow row in dataTable.Rows)
            {
                questionList.Add(GetQuestion(Convert.ToInt64(row["id"])));
            }
            //for (int i = 0; i < dataTable.Rows.Count; i++)
            //{
            //    questionList.Add(GetQuestion(Convert.ToInt64(dataTable.Rows[i]["id"])));
            //}
            connection.Close();

            return questionList;
        }

        public ICategory GetCategory(long categoryId)
        {
            SqlConnection connection = new(sqlConnectionString);
            connection.Open();
            SqlCommand command = new("select * from dbo.getCategoryByID(" + categoryId + ")", connection);
            SqlDataAdapter dataAdapter = new(command);

            DataTable dataTable = new();

            dataAdapter.Fill(dataTable);
            DataRow firstRow = dataTable.Rows[0];
            connection.Close();

            return (new Category(Convert.ToInt64(firstRow["id"]), firstRow["name"]?.ToString() ?? ""));
        }

        public List<IPost> GetRepliesOfPost(long postId)
        {
            SqlConnection connection = new(sqlConnectionString);
            connection.Open();
            SqlCommand command = new("select * from dbo.GetAllRepliesOfPost(" + postId + ")", connection);
            SqlDataAdapter dataAdapter = new(command);
            DataTable dataTable = new();
            dataAdapter.Fill(dataTable);

            List<IPost> postList = [];
            foreach (DataRow row in dataTable.Rows)
            {
                IPost newPost;
                long id = Convert.ToInt64(row["id"]);
                long userId = Convert.ToInt64(row["userId"]);
                DateTime datePosted = Convert.ToDateTime(row["datePosted"]);
                DateTime dateOfLastEdit = row["dateOfLastEdit"] == DBNull.Value
                    ? datePosted
                    : Convert.ToDateTime(row["dateOfLastEdit"]);
                string type = row["type"]?.ToString() ?? "";
                string content = row["content"]?.ToString() ?? "";
                List<IReaction> votes = GetVotesOfPost(postId);
                PostFactory.PostType postType = type switch
                {
                    "post" => PostFactory.PostType.POST,
                    "comment" => PostFactory.PostType.COMMENT,
                    "question" => PostFactory.PostType.QUESTION,
                    "answer" => PostFactory.PostType.ANSWER,
                    _ => throw new NotImplementedException($"This branch is not covered by the Post Factory -- {type}")
                };
                if (postType == PostFactory.PostType.QUESTION)
                {
                    string title = row["title"]?.ToString() ?? "";
                    ICategory category = GetCategory(Convert.ToInt64(row["categoryId"]));
                    List<ITag> tags = GetTagsOfQuestion(postId);
                    newPost = new Question(postId, title, category, tags, userId, content, datePosted, dateOfLastEdit, votes);
                        
                }
                else
                {
                    newPost = PostFactory.ConstructExistingPost(postType, postId, userId, content, datePosted, dateOfLastEdit, votes);
                }

                postList.Add(newPost);
            }
            //for (int i = 0; i < dataTable.Rows.Count; i++)
            //{
            //    Post newPost;
            //    long id = Convert.ToInt64(dataTable.Rows[i]["id"]);
            //    long userId = Convert.ToInt64(dataTable.Rows[i]["userId"]);
            //    DateTime datePosted = Convert.ToDateTime(dataTable.Rows[i]["datePosted"]);
            //    DateTime dateOfLastEdit = dataTable.Rows[i]["dateOfLastEdit"] == DBNull.Value ? datePosted : Convert.ToDateTime(dataTable.Rows[i]["dateOfLastEdit"]);
            //    string type = dataTable.Rows[i]["type"].ToString();
            //    string content = dataTable.Rows[i]["content"].ToString();
            //    List<Reaction> votes = GetVotesOfPost(postId);
            //    if (type == Post.QUESTION_TYPE)
            //    {
            //        string title = dataTable.Rows[i]["title"].ToString();
            //        Category category = GetCategory(Convert.ToInt64(dataTable.Rows[i]["categoryId"]));
            //        List<Tag> tags = GetTagsOfQuestion(postId);
            //        newPost = new Question(postId, userId, title, category, content, datePosted, dateOfLastEdit, type, votes, tags);
            //    }
            //    else
            //    {
            //        newPost = new Post(postId, userId, content, type, votes, datePosted, dateOfLastEdit);
            //    }

            //    postList.Add(newPost);
            //}
            connection.Close();
            return postList;
        }

        public void AddQuestion(Question question)
        {
            SqlConnection sqlConnection = new(sqlConnectionString);
            sqlConnection.Open();
            SqlCommand command = new("addQuestion", sqlConnection);
            command.Parameters.AddWithValue("@userID", question.UserID);
            command.Parameters.AddWithValue("@content", question.Content);
            command.Parameters.AddWithValue("@title", question.Title);
            command.Parameters.AddWithValue("@categoryId", question.Category?.CategoryID);
            command.CommandType = CommandType.StoredProcedure;
            command.ExecuteNonQuery();
            sqlConnection.Close();

        }

        public void UpdateQuestion(Question oldQuestion, Question newQuestion)
        {
            SqlConnection sqlConnection = new(sqlConnectionString);
            sqlConnection.Open();
            SqlCommand command = new("updateQuestion", sqlConnection);
            command.Parameters.AddWithValue("@questionId", oldQuestion.PostID);
            command.Parameters.AddWithValue("@content", newQuestion.Content);
            command.Parameters.AddWithValue("@title", newQuestion.Title);
            command.Parameters.AddWithValue("@categoryId", newQuestion.Category);
            command.CommandType = CommandType.StoredProcedure;
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public void AddPostAndReply(Post post, Post postRepliedOn)
        {
            SqlConnection connection = new(sqlConnectionString);
            connection.Open();
            SqlCommand? command = null;
            switch (post.GetType())
            {
                case Type t when t == typeof(Answer):
                    command = new SqlCommand("addAnswer", connection);
                    command.Parameters.AddWithValue("@userId", post.UserID);
                    command.Parameters.AddWithValue("@content", post.Content);
                    command.Parameters.AddWithValue("@postId", post.PostID);
                    break;
                case Type t when t == typeof(Comment):
                    command = new SqlCommand("addComment", connection);
                    command.Parameters.AddWithValue("@userId", post.UserID);
                    command.Parameters.AddWithValue("@content", post.Content);
                    command.Parameters.AddWithValue("@postId", post.PostID);
                    break;
            }

            SqlCommand reply_command = new SqlCommand("addReply", connection);
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
        // -------------------------------------------------------------------------------------------------------------------------------------------------------------
        //Boti lucreaza aici
        public void updatePost(Post oldPost, Post newPost)
        {
            SqlConnection connection = new SqlConnection(sqlConnectionString);
            connection.Open();
            SqlCommand? command = null;

            switch (oldPost.GetType())
            {
                case Type t when t == typeof(Answer):
                    command = new SqlCommand("UpdateAnswer", connection);
                    command.Parameters.AddWithValue("@answerId", newPost.PostID);
                    command.Parameters.AddWithValue("@content", newPost.Content);
                    break;
                case Type t when t == typeof(Comment):
                    command = new SqlCommand("UpdateComment",connection);
                    command.Parameters.AddWithValue("@commentId",newPost.PostID);
                    command.Parameters.AddWithValue("@content", newPost.Content);
                    break;
            }

            if (command != null)
            {
                command.CommandType = CommandType.StoredProcedure;
                command.ExecuteNonQuery();
            }
            connection.Close();

        }
        public List<Answer> getAnswersOfUser(long userId)
        {
            SqlConnection connection = new SqlConnection(sqlConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand("select * from dbo.getPostsByUserId(" + userId + ")", connection);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);

            List<Answer> answerList = [];
            foreach (DataRow row in dataTable.Rows)
            {
                string type = row["type"]?.ToString() ?? "";
                if ((PostType)Enum.Parse(typeof(PostType), type) == PostType.ANSWER)
                {
                    answerList.Add(new Answer(Convert.ToInt64(row["id"]), Convert.ToInt64(row["userId"]), Convert.ToString(row["content"]), Convert.ToDateTime(row["datePosted"]), Convert.ToDateTime(row["dateOfLastEdit"]), GetVotesOfPost(Convert.ToInt64(row["id"]))));
                }
            }
            //for (int i = 0; i < dataTable.Rows.Count; i++)
            //{
            //    string type = dataTable.Rows[i]["type"].ToString();
            //    List<Reaction> voteList = GetVotesOfPost(Convert.ToInt64(dataTable.Rows[i]["id"]));
            //    if (type == Post.ANSWER_TYPE)

            //        answerList.Add(new Post(Convert.ToInt64(dataTable.Rows[i]["id"]), Convert.ToInt64(dataTable.Rows[i]["userID"]),
            //                              dataTable.Rows[i]["content"].ToString(), type, voteList,
            //                              Convert.ToDateTime(dataTable.Rows[i]["datePosted"]), Convert.ToDateTime(dataTable.Rows[i]["dateOfLastEdit"])));
            //}

            connection.Close();

            return answerList;

        }
        public List<Comment> getCommentsOfUser(long userId)
        {
            SqlConnection connection = new SqlConnection(sqlConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand("select * from dbo.getPostsByUserId(" + userId + ")", connection);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);

            List<Comment> commentList = [];

            foreach(DataRow row in  dataTable.Rows)
            {
                string type = row["type"]?.ToString() ?? "";
                if((PostType)Enum.Parse(typeof(PostType), type) == PostType.COMMENT)
                {
                    //big sex aici
                    DateTime dateOfLastEdit;
                    try
                    {
                        dateOfLastEdit = Convert.ToDateTime(row["dateOfLastEdit"]);
                    }
                    catch (Exception e)
                    {
                        dateOfLastEdit = DateTime.Today;
                    }
                    commentList.Add(new Comment(Convert.ToInt64(row["id"]), Convert.ToInt64(row["userId"]), Convert.ToString(row["content"]), Convert.ToDateTime(row["datePosted"]), dateOfLastEdit, GetVotesOfPost(Convert.ToInt64(row["id"]))));
                }
            }
            // cam asta s-a intamplat cand codul asta a primit validare la pull request https://www.youtube.com/watch?v=rR4n-0KYeKQ
            //for (int i = 0; i < dataTable.Rows.Count; i++)
            //{
            //    string type = dataTable.Rows[i]["type"].ToString();
            //    List<Reaction> voteList = GetVotesOfPost(Convert.ToInt64(dataTable.Rows[i]["id"]));
            //    if (type == Post.COMMENT_TYPE)
            //    {
            //        DateTime datePosted = Convert.ToDateTime(dataTable.Rows[i]["datePosted"]);
            //        DateTime dateOfLastEdit;
            //        try
            //        {
            //            dateOfLastEdit = Convert.ToDateTime(dataTable.Rows[i]["dateOfLastEdit"]);

            //        }
            //        catch (Exception e)
            //        {
            //            dateOfLastEdit = DateTime.Today;
            //        }
            //        commentList.Add(new Post(Convert.ToInt64(dataTable.Rows[i]["id"]), Convert.ToInt64(dataTable.Rows[i]["userID"]),
            //                              dataTable.Rows[i]["content"].ToString(), type, voteList,
            //                              Convert.ToDateTime(dataTable.Rows[i]["datePosted"]), dataTable.Rows[i]["dateOfLastEdit"] == DBNull.Value ? Convert.ToDateTime(dataTable.Rows[i]["datePosted"]) : Convert.ToDateTime(dataTable.Rows[i]["dateOfLastEdit"])));
            //    }
            //}
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
                List<Reaction> voteList = GetVotesOfPost(Convert.ToInt64(dataTable.Rows[i]["id"]));
                if (type == Post.QUESTION_TYPE)
                {
                    List<Tag> tagList = GetTagsOfQuestion(Convert.ToInt64(dataTable.Rows[i]["id"]));
                    Category category = GetCategory(Convert.ToInt64(dataTable.Rows[i]["categoryId"]));

                    questionList.Add(new Question(Convert.ToInt64(dataTable.Rows[i]["id"]), Convert.ToInt64(dataTable.Rows[i]["userId"]),
                                dataTable.Rows[i]["title"].ToString(), category,
                                dataTable.Rows[i]["content"].ToString(),
                                Convert.ToDateTime(dataTable.Rows[i]["datePosted"]),
                                dataTable.Rows[i]["dateOfLastEdit"] == DBNull.Value ? Convert.ToDateTime(dataTable.Rows[i]["datePosted"]) : Convert.ToDateTime(dataTable.Rows[i]["dateOfLastEdit"]), dataTable.Rows[i]["type"].ToString(),
                                voteList, tagList));

                }
            }
            //questionList.Add(new Question(8, 3, "question", new Category(8, "category"), "content", new DateTime(), new DateTime(), "type", new List<Reaction>(), new List<Tag>() ));
            connection.Close();
            return questionList;
        }
    }
}
