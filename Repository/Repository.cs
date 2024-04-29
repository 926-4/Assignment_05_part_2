using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using UBB_SE_2024_Team_42.Domain;
using UBB_SE_2024_Team_42.Domain.Badge;
using UBB_SE_2024_Team_42.Domain.Category;
using UBB_SE_2024_Team_42.Domain.Notification;
using UBB_SE_2024_Team_42.Domain.Posts;
using UBB_SE_2024_Team_42.Domain.Reactions;
using UBB_SE_2024_Team_42.Domain.Tag;
using UBB_SE_2024_Team_42.Domain.User;
using static UBB_SE_2024_Team_42.Domain.Posts.PostFactory;

namespace UBB_SE_2024_Team_42.Repository
{
    public class Repository
    {
        private readonly string sqlConnectionString = @"Data Source = CAMFRIGLACLUJ; Initial Catalog = Team42DB;Integrated Security = True";

        public List<INotification> GetNotificationsOfUser(long userId)
        {
            SqlConnection connection = new (sqlConnectionString);
            connection.Open();
            SqlCommand command = new ("select * from dbo.getNotificationsOfUser(" + userId + ")", connection);
            SqlDataAdapter dataAdapter = new (command);
            DataTable dataTable = new ();
            dataAdapter.Fill(dataTable);

            List<INotification> notificationList = new ();
            foreach (DataRow row in dataTable.Rows)
            {
                notificationList.Add(
                    new Notification(
                        Convert.ToInt64(row["id"]),
                        Convert.ToInt64(row["postId"]),
                        Convert.ToInt64(row["badgeId"])));
            }

            connection.Close();

            return notificationList;
        }

        public List<ICategory> GetCategoriesModeratedByUser(long userId)
        {
            SqlConnection connection = new (sqlConnectionString);
            connection.Open();
            SqlCommand command = new ("select * from dbo.getCategoriesModeratedByUser(" + userId + ")", connection);
            SqlDataAdapter dataAdapter = new (command);
            DataTable dataTable = new ();
            dataAdapter.Fill(dataTable);

            List<ICategory> categoryList = new ();
            foreach (DataRow row in dataTable.Rows)
            {
                categoryList.Add(
                    new Category(
                        Convert.ToInt64(row["id"]),
                        row["name"]?.ToString() ?? string.Empty));
            }

            connection.Close();
            return categoryList;
        }

        public List<IBadge> GetBadgesOfUser(long userId)
        {
            SqlConnection connection = new (sqlConnectionString);
            connection.Open();
            SqlCommand command = new ("select * from dbo.getBadgesOfUser(" + userId + ")", connection);
            SqlDataAdapter dataAdapter = new (command);
            DataTable dataTable = new ();
            dataAdapter.Fill(dataTable);

            List<IBadge> badgeList = new ();
            foreach (DataRow row in dataTable.Rows)
            {
                System.Drawing.Image badgeImage;
                byte[] imageBytes = (byte[])row["image"];

                using (Stream stream = new MemoryStream(imageBytes))
                {
                    badgeImage = Image.FromStream(stream);
                }
                badgeList.Add(
                    new Badge(
                        Convert.ToInt64(row["id"]),
                        row["name"]?.ToString() ?? string.Empty,
                        row["description"]?.ToString() ?? string.Empty,
                        badgeImage));
            }

            connection.Close();

            return badgeList;
        }

        public IUser GetUser(long userId)
        {
            SqlConnection connection = new (sqlConnectionString);
            connection.Open();
            SqlCommand command = new ("select * from dbo.getUser(" + userId + ")", connection);
            SqlDataAdapter dataAdapter = new (command);
            DataTable dataTable = new ();
            dataAdapter.Fill(dataTable);
            var firstRow = dataTable.Rows[0];
            IUser user = new UserFactory().NewUser()
                .SetName(firstRow["name"]?.ToString() ?? string.Empty)
                .SetNotificationList(GetNotificationsOfUser(userId))
                .SetCategoriesModeratedList(GetCategoriesModeratedByUser(userId))
                .SetBadgeList(GetBadgesOfUser(userId))
                .Get();

            connection.Close();
            return user;
        }

        public List<IUser> GetAllUsers()
        {
            SqlConnection connection = new (sqlConnectionString);
            connection.Open();
            SqlCommand command = new ("select * from dbo.getAllUsers()", connection);
            SqlDataAdapter dataAdapter = new (command);
            DataTable dataTable = new ();
            dataAdapter.Fill(dataTable);

            List<IUser> userList = new ();
            foreach (DataRow row in dataTable.Rows)
            {
                long userId = Convert.ToInt64(row["id"]);
                userList.Add(GetUser(userId));
            }

            connection.Close();

            return userList;
        }

        public List<IReaction> GetVotesOfPostByPostID(long postId)
        {
            SqlConnection connection = new (sqlConnectionString);
            connection.Open();
            SqlCommand command = new ("select * from dbo.getVotesOfPost(" + postId + ")", connection);
            SqlDataAdapter dataAdapter = new (command);
            DataTable dataTable = new ();
            dataAdapter.Fill(dataTable);

            List<IReaction> voteList = new ();
            foreach (DataRow row in dataTable.Rows)
            {
                voteList.Add(
                    new Reaction(
                        Convert.ToInt32(row["value"]),
                        Convert.ToInt64(row["userId"])));
            }
            connection.Close();

            return voteList;
        }

        public List<ICategory> GetAllCategories()
        {
            SqlConnection connection = new (sqlConnectionString);
            connection.Open();
            SqlCommand command = new ("select * from dbo.getAllCategories()", connection);
            SqlDataAdapter dataAdapter = new (command);
            DataTable dataTable = new ();
            dataAdapter.Fill(dataTable);

            List<ICategory> categoryList = new ();
            foreach (DataRow row in dataTable.Rows)
            {
                categoryList.Add(new Category(
                    Convert.ToInt64(row["id"]),
                    row["name"]?.ToString() ?? string.Empty));
            }
            connection.Close();

            return categoryList;
        }

        public List<ITag> GetTagsOfQuestion(long questionId)
        {
            SqlConnection connection = new (sqlConnectionString);
            connection.Open();
            string commandString = "select * from dbo.getTagById(" + questionId + ")";
            SqlCommand command = new (commandString, connection);
            SqlDataAdapter dataAdapter = new (command);
            DataTable dataTable = new ();
            dataAdapter.Fill(dataTable);

            List<ITag> tagList = new ();
            foreach (DataRow row in dataTable.Rows)
            {
                tagList.Add(new Tag(Convert.ToInt64(row["id"]), row["name"]?.ToString() ?? string.Empty));
            }
            connection.Close();

            return tagList;
        }
        public IQuestion GetQuestion(long questionId)
        {
            SqlConnection connection = new (sqlConnectionString);
            connection.Open();
            SqlCommand command = new ("select * from dbo.getQuestionByID(" + questionId + ")", connection);
            SqlDataAdapter dataAdapter = new (command);

            DataTable dataTable = new ();

            dataAdapter.Fill(dataTable);
            DataRow firstRow = dataTable.Rows[0];

            List<ITag> tagList = GetTagsOfQuestion(questionId);
            List<IReaction> voteList = GetVotesOfPostByPostID(questionId);
            ICategory category = GetCategory(Convert.ToInt64(firstRow["categoryId"]));

            connection.Close();
            return new Question(
                Convert.ToInt64(firstRow["id"]),
                                firstRow["title"]?.ToString() ?? string.Empty,
                category,
                tagList,
                Convert.ToInt64(firstRow["userId"]),
                                firstRow["content"]?.ToString() ?? string.Empty,
             Convert.ToDateTime(firstRow["datePosted"]),
                                firstRow["dateOfLastEdit"] == DBNull.Value
                                    ? Convert.ToDateTime(firstRow["datePosted"])
                                    : Convert.ToDateTime(firstRow["dateOfLastEdit"]),
                                voteList);
        }

        public List<IQuestion> GetAllQuestions()
        {
            SqlConnection connection = new (sqlConnectionString);
            connection.Open();
            SqlCommand command = new ("select * from dbo.getAllQuestions()", connection);
            SqlDataAdapter dataAdapter = new (command);
            DataTable dataTable = new ();
            dataAdapter.Fill(dataTable);

            List<IQuestion> questionList = new ();
            foreach (DataRow row in dataTable.Rows)
            {
                questionList.Add(GetQuestion(Convert.ToInt64(row["id"])));
            }
            connection.Close();

            return questionList;
        }

        public ICategory GetCategory(long categoryId)
        {
            SqlConnection connection = new (sqlConnectionString);
            connection.Open();
            SqlCommand command = new ("select * from dbo.getCategoryByID(" + categoryId + ")", connection);
            SqlDataAdapter dataAdapter = new (command);

            DataTable dataTable = new ();

            dataAdapter.Fill(dataTable);
            DataRow firstRow = dataTable.Rows[0];
            connection.Close();

            return new Category(Convert.ToInt64(firstRow["id"]), firstRow["name"]?.ToString() ?? string.Empty);
        }

        public List<IPost> GetRepliesOfPost(long postId)
        {
            SqlConnection connection = new (sqlConnectionString);
            connection.Open();
            SqlCommand command = new ("select * from dbo.GetAllRepliesOfPost(" + postId + ")", connection);
            SqlDataAdapter dataAdapter = new (command);
            DataTable dataTable = new ();
            dataAdapter.Fill(dataTable);

            List<IPost> postList = new ();
            foreach (DataRow row in dataTable.Rows)
            {
                IPost newPost;
                long id = Convert.ToInt64(row["id"]);
                long userId = Convert.ToInt64(row["userId"]);
                DateTime datePosted = Convert.ToDateTime(row["datePosted"]);
                DateTime dateOfLastEdit = row["dateOfLastEdit"] == DBNull.Value
                    ? datePosted
                    : Convert.ToDateTime(row["dateOfLastEdit"]);
                string type = row["type"]?.ToString() ?? string.Empty;
                string content = row["content"]?.ToString() ?? string.Empty;
                List<IReaction> votes = GetVotesOfPostByPostID(postId);
                PostType postType = type switch
                {
                    "post" => PostType.TEXT_POST,
                    "comment" => PostType.COMMENT,
                    "question" => PostType.QUESTION,
                    "answer" => PostType.ANSWER,
                    _ => throw new NotImplementedException($"This branch is not covered by the TextPost Factory -- {type}")
                };
                if (postType == PostType.QUESTION)
                {
                    string title = row["title"]?.ToString() ?? string.Empty;
                    ICategory category = GetCategory(Convert.ToInt64(row["categoryId"]));
                    List<ITag> tags = GetTagsOfQuestion(postId);
                    newPost = new Question(postId, title, category, tags, userId, content, datePosted, dateOfLastEdit, votes);
                }
                else
                {
                    newPost = ConstructExistingPost(postType, postId, userId, content, datePosted, dateOfLastEdit, votes);
                }

                postList.Add(newPost);
            }
            connection.Close();
            return postList;
        }

        public void AddQuestion(IQuestion question)
        {
            SqlConnection sqlConnection = new (sqlConnectionString);
            sqlConnection.Open();
            SqlCommand command = new ("addQuestion", sqlConnection);
            command.Parameters.AddWithValue("@userID", question.UserID);
            command.Parameters.AddWithValue("@content", question.Content);
            command.Parameters.AddWithValue("@title", question.Title);
            command.Parameters.AddWithValue("@categoryId", question.Category?.ID);
            command.CommandType = CommandType.StoredProcedure;
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public void UpdateQuestion(IQuestion oldQuestion, IQuestion newQuestion)
        {
            SqlConnection sqlConnection = new (sqlConnectionString);
            sqlConnection.Open();
            SqlCommand command = new ("updateQuestion", sqlConnection);
            command.Parameters.AddWithValue("@questionId", oldQuestion.ID);
            command.Parameters.AddWithValue("@content", newQuestion.Content);
            command.Parameters.AddWithValue("@title", newQuestion.Title);
            command.Parameters.AddWithValue("@categoryId", newQuestion.Category);
            command.CommandType = CommandType.StoredProcedure;
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public void AddPostAndReply(IPost post, IPost postRepliedOn)
        {
            SqlConnection connection = new (sqlConnectionString);
            connection.Open();
            SqlCommand? command = null;
            switch (post.GetType())
            {
                case Type t when t == typeof(Answer):
                    command = new SqlCommand("AddAnswer", connection);
                    command.Parameters.AddWithValue("@userId", post.UserID);
                    command.Parameters.AddWithValue("@content", post.Content);
                    command.Parameters.AddWithValue("@postId", post.ID);
                    break;
                case Type t when t == typeof(Comment):
                    command = new SqlCommand("AddComment", connection);
                    command.Parameters.AddWithValue("@userId", post.UserID);
                    command.Parameters.AddWithValue("@content", post.Content);
                    command.Parameters.AddWithValue("@postId", post.ID);
                    break;
            }

            SqlCommand reply_command = new ("AddReply", connection);
            reply_command.Parameters.AddWithValue("@idOfPostRepliedOn", postRepliedOn.ID);
            reply_command.Parameters.AddWithValue("@idOfReply", post.ID);
            if (command != null)
            {
                command.CommandType = CommandType.StoredProcedure;
                reply_command.CommandType = CommandType.StoredProcedure;
                command.ExecuteNonQuery();
                reply_command.ExecuteNonQuery();
            }
            connection.Close();
        }
        public void UpdatePost(IPost oldPost, IPost newPost)
        {
            SqlConnection connection = new (sqlConnectionString);
            connection.Open();
            SqlCommand command;
            switch (oldPost.GetType())
            {
                case Type t when t == typeof(Answer):
                    command = new SqlCommand("UpdateAnswer", connection);
                    command.Parameters.AddWithValue("@answerId", newPost.ID);
                    command.Parameters.AddWithValue("@content", newPost.Content);
                    break;
                case Type t when t == typeof(Comment):
                    command = new SqlCommand("UpdateComment", connection);
                    command.Parameters.AddWithValue("@commentId", newPost.ID);
                    command.Parameters.AddWithValue("@content", newPost.Content);
                    break;
                default:
                    goto cleanupLabel;
            }

            command.CommandType = CommandType.StoredProcedure;
            command.ExecuteNonQuery();
        cleanupLabel:
            connection.Close();
        }

        public List<IPost> GetAnswersOfUser(long userId)
        {
            SqlConnection connection = new (sqlConnectionString);
            connection.Open();
            SqlCommand command = new ("select * from dbo.getPostsByUserId(" + userId + ")", connection);
            SqlDataAdapter dataAdapter = new (command);
            DataTable dataTable = new ();
            dataAdapter.Fill(dataTable);

            List<IPost> answerList = new ();
            foreach (DataRow row in dataTable.Rows)
            {
                string type = row["type"]?.ToString() ?? string.Empty;
                if ((PostType)Enum.Parse(typeof(PostType), type) == PostType.ANSWER)
                {
                    answerList.Add(
                        new Answer(
                            Convert.ToInt64(row["id"]),
                            Convert.ToInt64(row["userId"]),
                            Convert.ToString(row["content"]) ?? string.Empty,
                            Convert.ToDateTime(row["datePosted"]),
                            Convert.ToDateTime(row["dateOfLastEdit"]),
             GetVotesOfPostByPostID(Convert.ToInt64(row["id"]))));
                }
            }
            connection.Close();

            return answerList;
        }

        public List<IPost> GetCommentsOfUser(long userId)
        {
            SqlConnection connection = new (sqlConnectionString);
            connection.Open();
            SqlCommand command = new ("select * from dbo.getPostsByUserId(" + userId + ")", connection);
            SqlDataAdapter dataAdapter = new (command);
            DataTable dataTable = new ();
            dataAdapter.Fill(dataTable);

            List<IPost> commentList = new ();

            foreach (DataRow row in dataTable.Rows)
            {
                string type = row["type"]?.ToString() ?? string.Empty;
                if ((PostType)Enum.Parse(typeof(PostType), type) == PostType.COMMENT)
                {
                    DateTime dateOfLastEdit = DateTime.TryParse(row["dateOfLastEdit"]?.ToString() ?? string.Empty, out DateTime parsingResult)
                        ? parsingResult
                        : DateTime.Today;
                    commentList.Add(
                        new Comment(
                            Convert.ToInt64(row["id"]),
                            Convert.ToInt64(row["userId"]),
                            Convert.ToString(row["content"]) ?? string.Empty,
                            Convert.ToDateTime(row["datePosted"]),
                            dateOfLastEdit,
                            GetVotesOfPostByPostID(Convert.ToInt64(row["id"]))));
                }
            }
            // cam asta s-a intamplat cand codul asta a primit validare la pull request https://www.youtube.com/watch?v=rR4n-0KYeKQ
            connection.Close();

            return commentList;
        }

        public List<IQuestion> GetQuestionsOfUser(long userId)
        {
            SqlConnection connection = new (sqlConnectionString);
            connection.Open();
            SqlCommand command = new ("select * from dbo.getPostsByUserId(" + userId + ")", connection);
            SqlDataAdapter dataAdapter = new (command);
            DataTable dataTable = new ();
            dataAdapter.Fill(dataTable);

            List<IQuestion> questionList = new ();
            foreach (DataRow row in dataTable.Rows)
            {
                string type = row["type"].ToString() ?? string.Empty;
                if ((PostType)Enum.Parse(typeof(PostType), type) == PostType.QUESTION)
                {
                    questionList.Add(
                        new Question(
                            Convert.ToInt64(row["id"]),
                            Convert.ToString(row["title"]) ?? string.Empty,
                            GetCategory(Convert.ToInt64(row["categoryId"])),
                            GetTagsOfQuestion(Convert.ToInt64(row["id"])),
                            Convert.ToInt64(row["userId"]),
                            Convert.ToString(row["content"]) ?? string.Empty,
                            Convert.ToDateTime(row["datePosted"]),
                            Convert.ToDateTime(row["dateOfLastEdit"]),
                            GetVotesOfPostByPostID(Convert.ToInt64(row["id"]))));
                }
            }
            connection.Close();
            return questionList;
        }
    }
}
