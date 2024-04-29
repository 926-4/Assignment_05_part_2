using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using UBB_SE_2024_Team_42.Domain.Badge;
using UBB_SE_2024_Team_42.Domain.Category;
using UBB_SE_2024_Team_42.Domain.Notification;
using UBB_SE_2024_Team_42.Domain.Post;
using UBB_SE_2024_Team_42.Domain.Post.Interfaces;
using UBB_SE_2024_Team_42.Domain.Posts;
using UBB_SE_2024_Team_42.Domain.Reactions;
using UBB_SE_2024_Team_42.Domain.Tag;
using UBB_SE_2024_Team_42.Domain.User;
using UBB_SE_2024_Team_42.Service.EntityCreationServices;
using UBB_SE_2024_Team_42.Utils.Functionals;
using static UBB_SE_2024_Team_42.Domain.Posts.PostFactory;

namespace UBB_SE_2024_Team_42.Repository
{
    public class Repository : IRepository
    {
        private readonly string sqlConnectionString = @"Data Source = CAMFRIGLACLUJ; Initial Catalog = Team42DB;Integrated Security = True";
        private readonly NotificationFactory notificationFactory = new();
        private readonly CategoryFactory categoryFactory = new();
        private readonly BadgeFactory badgeFactory = new();
        private readonly UserFactory userFactory = new();
        private readonly ReactionFactory reactionFactory = new();
        private readonly TagFactory tagFactory = new();
        private readonly AnswerFactory answerFactory = new();
        private readonly CommentFactory commentFactory = new();
        private static Image? CellInDBToBadgeImage(object dataRowCell) => Image.FromStream(new MemoryStream((byte[])dataRowCell));
        private DataTable QueryDB(string sqlStatement)
        {
            SqlConnection connection = new(sqlConnectionString);
            connection.Open();
            SqlCommand command = new(sqlStatement, connection);
            SqlDataAdapter dataAdapter = new(command);
            DataTable dataTable = new();
            dataAdapter.Fill(dataTable);
            connection.Close();
            return dataTable;
        }
        private INotification RowInDBToNotification(DataRow row)
            => notificationFactory.NewNotification()
                .SetID(Convert.ToInt64(row["id"]))
                .SetPostID(Convert.ToInt64(row["postId"]))
                .SetBadgeId(Convert.ToInt64(row["badgeId"]))
                .Get();
        private IBadge RowInDBToBadge(DataRow row)
            => badgeFactory.NewBadge()
                .SetID(Convert.ToInt64(row["id"]))
                .SetName(row["name"].ToString() ?? string.Empty)
                .SetDescription(row["description"]?.ToString() ?? string.Empty)
                .SetImage(CellInDBToBadgeImage(row["image"]))
                .Get();
        private ICategory RowInDBToCategory(DataRow row)
            => categoryFactory.NewCategory()
                .SetID(Convert.ToInt64(row["id"]))
                .SetName(row["name"].ToString() ?? string.Empty)
                .Get();
        private IUser RowInDBToUser(DataRow row)
        {
            long userId = Convert.ToInt64(row["id"]);
            return userFactory.NewUser()
                              .SetName(row["name"].ToString() ?? string.Empty)
                              .SetNotificationList(GetNotificationsOfUser(userId))
                              .SetCategoriesModeratedList(GetCategoriesModeratedByUser(userId))
                              .SetBadgeList(GetBadgesOfUser(userId))
                              .Get();
        }
        private IReaction RowInDBToIReaction(DataRow row)
            => reactionFactory.NewReaction()
                .SetReacterUserId(Convert.ToInt64(row["userId"]))
                .SetReactionValue(Convert.ToInt32(row["value"]))
                .Get();
        private ITag RowInDBToTag(DataRow row)
            => tagFactory.NewTag()
                .SetID(Convert.ToInt64(row["id"]))
                .SetName(row["name"].ToString() ?? string.Empty)
                .Get();
        private IAnswer RowInDBToAnswer(DataRow row)
            => answerFactory.NewAnswer()
               .SetId(Convert.ToInt64(row["id"]))
               .SetUserId(Convert.ToInt64(row["userId"]))
               .SetContent(Convert.ToString(row["content"]) ?? string.Empty)
               .SetDatePosted(Convert.ToDateTime(row["datePosted"]))
               .SetDateOfLastEdit(Convert.ToDateTime(row["dateOfLastEdit"]))
               .SetReactions(GetReactionsOfPostByPostID(Convert.ToInt64(row["id"])))
               .Get();
        private IComment RowInDBToComment(DataRow row)
            => commentFactory.NewComment()
                .SetId(Convert.ToInt64(row["id"]))
                .SetUserId(Convert.ToInt64(row["userId"]))
                .SetContent(Convert.ToString(row["content"]) ?? string.Empty)
                .SetDatePosted(Convert.ToDateTime(row["datePosted"]))
                .SetDateOfLastEdit(DateTime.TryParse(row["dateOfLastEdit"].ToString(), out DateTime parsingResult)
                                                                                ? parsingResult
                                                                                : Convert.ToDateTime(row["datePosted"]))
                .Get();
        private IQuestion RowInDBToQuestion(DataRow row)
        {
            long questionId = Convert.ToInt64(row["id"]);
            long userId = Convert.ToInt64(row["userId"]);
            List<ITag> tagList = GetTagsOfQuestion(questionId);
            List<IReaction> voteList = GetReactionsOfPostByPostID(questionId);
            ICategory category = GetCategoryByID(Convert.ToInt64(row["categoryId"]));
            DateTime postDate = Convert.ToDateTime(row["datePosted"]);
            DateTime lastEditDate = DateTime.TryParse(row["dateOfLastEdit"].ToString(), out DateTime editDate)
                ? editDate
                : postDate;
            string title = row["title"]?.ToString() ?? string.Empty;
            string content = row["content"]?.ToString() ?? string.Empty;
            return new QuestionFactory().NewQuestion()
                                        .SetId(questionId)
                                        .SetTitle(title)
                                        .SetCategory(category)
                                        .SetTags(tagList)
                                        .SetUserId(userId)
                                        .SetContent(content)
                                        .SetPostTime(postDate)
                                        .SetEditTime(lastEditDate)
                                        .SetVoteList(voteList)
                                        .GetQuestion();
        }

        public List<INotification> GetNotificationsOfUser(long userId)
            => QueryDB("select * from dbo.getNotificationsOfUser(" + userId + ")")
                .AsEnumerable()
                .Select(RowInDBToNotification)
                .ToList();

        public List<ICategory> GetCategoriesModeratedByUser(long userId)
            => QueryDB("select * from dbo.getCategoriesModeratedByUser(" + userId + ")")
                .AsEnumerable()
                .Select(RowInDBToCategory)
                .ToList();

        public List<IBadge> GetBadgesOfUser(long userId)
            => QueryDB("select * from dbo.getBadgesOfUser(" + userId + ")")
                .AsEnumerable()
                .Select(RowInDBToBadge)
                .ToList();

        public IUser GetUser(long userId) => RowInDBToUser(QueryDB("select * from dbo.getUser(" + userId + ")").Rows[0]);

        public List<IUser> GetAllUsers()
            => QueryDB("select * from dbo.getAllUsers()")
                .AsEnumerable()
                .Select(RowInDBToUser)
                .ToList();

        public List<IReaction> GetReactionsOfPostByPostID(long postId)
            => QueryDB("select * from dbo.getVotesOfPost(" + postId + ")")
                .AsEnumerable()
                .Select(RowInDBToIReaction)
                .ToList();

        public List<ICategory> GetAllCategories()
            => QueryDB("select * from dbo.getAllCategories()")
                .AsEnumerable()
                .Select(RowInDBToCategory)
                .ToList();

        public List<ITag> GetTagsOfQuestion(long questionId)
            => QueryDB("select * from dbo.getTagById(" + questionId + ")")
                .AsEnumerable()
                .Select(RowInDBToTag)
                .ToList();

        public IQuestion GetQuestion(long questionId)
            => RowInDBToQuestion(QueryDB("select * from dbo.getQuestionByID(" + questionId + ")").Rows[0]);

        public List<IQuestion> GetAllQuestions()
            => QueryDB("select * from dbo.getAllQuestions()")
            .AsEnumerable()
            .Select(RowInDBToQuestion)
            .ToList();

        public ICategory GetCategoryByID(long categoryId) =>
            RowInDBToCategory(QueryDB("select * from dbo.getCategoryByID(" + categoryId + ")").Rows[0]);

        public List<IPost> GetRepliesOfPost(long postId)
        {
            var dataTable = QueryDB("select * from dbo.GetAllRepliesOfPost(" + postId + ")");
            List<IPost> postList = new();
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
                List<IReaction> votes = GetReactionsOfPostByPostID(postId);
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
                    ICategory category = GetCategoryByID(Convert.ToInt64(row["categoryId"]));
                    List<ITag> tags = GetTagsOfQuestion(postId);
                    newPost = new Question(postId, title, category, tags, userId, content, datePosted, dateOfLastEdit, votes);
                }
                else
                {
                    newPost = ConstructExistingPost(postType, postId, userId, content, datePosted, dateOfLastEdit, votes);
                }

                postList.Add(newPost);
            }
            return postList;
        }

        public void AddQuestion(IQuestion question)
        {
            SqlConnection sqlConnection = new(sqlConnectionString);
            sqlConnection.Open();
            SqlCommand command = new("addQuestion", sqlConnection);
            command.Parameters.AddWithValue("@userID", question.UserID);
            command.Parameters.AddWithValue("@content", question.Content);
            command.Parameters.AddWithValue("@title", question.Title);
            command.Parameters.AddWithValue("@categoryId", question.Category?.ID);
            command.CommandType = CommandType.StoredProcedure;
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }
        public void UpdatePost(IPost oldPost, IPost newPost)
        {
            if (oldPost is not Answer && oldPost is not Comment)
            {
                return;
            }
            SqlConnection connection = new(sqlConnectionString);
            connection.Open();
            SqlCommand command;
            switch (oldPost.GetType())
            {
                case Type t when t == typeof(Answer):
                    command = new SqlCommand("UpdateAnswer", connection);
                    command.Parameters.AddWithValue("@answerId", newPost.ID);
                    break;
                case Type t when t == typeof(Comment):
                    command = new SqlCommand("UpdateComment", connection);
                    command.Parameters.AddWithValue("@commentId", newPost.ID);
                    break;
                default:
                    return;
            }
            command.Parameters.AddWithValue("@content", newPost.Content);
            command.CommandType = CommandType.StoredProcedure;
            command.ExecuteNonQuery();
            connection.Close();
        }

        public List<IAnswer> GetAnswersOfUser(long userId)
            => QueryDB("select * from dbo.getPostsByUserId(" + userId + ")")
                .AsEnumerable()
                .Where(Filters.DataRowRepresentsAnswer)
                .Select(RowInDBToAnswer)
                .ToList();

        public List<IComment> GetCommentsOfUser(long userId)
            => QueryDB("select * from dbo.getPostsByUserId(" + userId + ")")
                .AsEnumerable()
                .Where(Filters.DataRowRepresentsComment)
                .Select(RowInDBToComment)
                .ToList();

        public List<IQuestion> GetQuestionsOfUser(long userId)
        => QueryDB("select * from dbo.getPostsByUserId(" + userId + ")")
            .AsEnumerable()
            .Where(Filters.DataRowRepresentsQuestion)
            .Select(RowInDBToQuestion)
            .ToList();
    }
}