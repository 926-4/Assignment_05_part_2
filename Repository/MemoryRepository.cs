using UBB_SE_2024_Team_42.Domain.Badge;
using UBB_SE_2024_Team_42.Domain.Category;
using UBB_SE_2024_Team_42.Domain.Notification;
using UBB_SE_2024_Team_42.Domain.Post;
using UBB_SE_2024_Team_42.Domain.Post.Interfaces;
using UBB_SE_2024_Team_42.Domain.Posts;
using UBB_SE_2024_Team_42.Domain.Reactions;
using UBB_SE_2024_Team_42.Domain.Tag;
using UBB_SE_2024_Team_42.Domain.User;
using UBB_SE_2024_Team_42.Utils;
using UBB_SE_2024_Team_42.Utils.Functionals;

namespace UBB_SE_2024_Team_42.Repository
{
    public class MemoryRepository : IRepository
    {
        private readonly Dictionary<long, IBadge> badges;
        private readonly Dictionary<long, ICategory> categories;
        private readonly Dictionary<long, INotification> notifications;
        private readonly Dictionary<long, IPost> posts;
        private readonly Dictionary<long, IPost> replies;
        private readonly Dictionary<long, IQuestion> questions;
        private readonly Dictionary<long, IUser> users;
        private readonly Dictionary<long, long> badgeIdToUserIdAssociation;
        public MemoryRepository()
        {
            Badge badgeA = new () { Name = "answer" };
            Badge badgeB = new () { Name = "b", ID = IDGenerator.RandomLong() };
            badges = new Dictionary<long, IBadge>()
            {
                { badgeA.ID, badgeA },
                { badgeB.ID, badgeB }
            };

            Category categoryA = new () { Name = "answer" };
            Category categoryB = new () { Name = "b", ID = IDGenerator.RandomLong() };
            categories = new Dictionary<long, ICategory>()
            {
                { categoryA.ID, categoryA },
                { categoryB.ID, categoryB }
            };

            User userA = new () { Name = "answer", CategoriesModeratedList = new () { categoryA } };
            User userB = new () { ID = IDGenerator.RandomLong(), CategoriesModeratedList = new () { categoryB } };
            users = new Dictionary<long, IUser>()
            {
                { userA.ID, userA },
                { userB.ID, userB }
            };

            badgeIdToUserIdAssociation = new Dictionary<long, long>()
            {
                { badgeA.ID, userA.ID },
                { badgeB.ID, userB.ID }
            };

            Notification notificationA = new () { Text = "answer", UserID = userA.ID };
            Notification notificationB = new () { Text = "b", ID = IDGenerator.RandomLong(), UserID = userB.ID };
            notifications = new Dictionary<long, INotification>()
            {
                { notificationA.ID, notificationA },
                { notificationB.ID, notificationB }
            };

            Question questionA = new () { Content = "answer", UserID = userA.ID };
            Question questionB = new () { Content = "b", ID = IDGenerator.RandomLong(), UserID = userA.ID };
            questions = new Dictionary<long, IQuestion>()
            {
                { questionA.ID, questionA },
                { questionB.ID, questionB }
            };

            Comment comment = new () { ID = 2, Content = "answer", UserID = userA.ID };
            Answer answer = new () { ID = 3, Content = "answer", UserID = userA.ID };
            TextPost postA = new () { Content = "answer", UserID = userA.ID };
            TextPost postB = new () { Content = "b", ID = IDGenerator.RandomLong(), UserID = userA.ID };
            posts = new Dictionary<long, IPost>()
            {
                { postA.ID, postA },
                { postB.ID, postB },
                { comment.ID, comment },
                { answer.ID, answer }
            };

            IPost replyA = new TextPost() { Content = "reply", UserID = userA.ID };
            IPost replyB = new TextPost() { Content = "reply", UserID = userB.ID, ID = IDGenerator.RandomLong() };

            replies = new Dictionary<long, IPost>()
            {
                { replyA.ID, replyA },
                { replyB.ID, replyB }
            };
        }
        private IAnswer MapIPostToIAnswer(IPost ipost) => (IAnswer)ipost;
        private IComment MapIPostToIComment(IPost ipost) => (IComment)ipost;
        public void AddCategory(ICategory category) => categories.Add(category.ID, category);
        public void AddQuestion(IQuestion question)
        {
            questions.Add(question.ID, question);
            posts.Add(question.ID, question);
        }
        public void AddBadge(IBadge badge, long userId)
        {
            badges.Add(badge.ID, badge);
            badgeIdToUserIdAssociation.Add(badge.ID, userId);
        }
        public IEnumerable<ICategory> GetAllCategories() => categories.Values;

        public IEnumerable<IQuestion> GetAllQuestions() => questions.Values;

        public void AddUser(IUser user) => users.Add(user.ID, user);

        public void AddPost(IPost post) => posts.Add(post.ID, post);

        public IEnumerable<IUser> GetAllUsers() => users.Values;

        public IEnumerable<IAnswer> GetAnswersOfUser(long userId) => posts.Values.Where(Filters.IPostIsIAnswer).Select(MapIPostToIAnswer).Where(ianswer => ianswer.UserID == userId);

        public IEnumerable<IBadge> GetBadgesOfUser(long userId) => badges.Values.Where(badge => badgeIdToUserIdAssociation[badge.ID] == userId);

        public IEnumerable<ICategory> GetCategoriesModeratedByUser(long userId) => users[userId].CategoriesModeratedList;
        public ICategory GetCategoryByID(long categoryId) => categories[categoryId];

        public IEnumerable<IComment> GetCommentsOfUser(long userId) => posts.Values.Where(Filters.IPostIsIComment).Select(MapIPostToIComment).Where(post => post.UserID == userId);

        // Some sort of closure could be created here -- not exactly sure how
        public IEnumerable<INotification> GetNotificationsOfUser(long userId) => notifications.Values.Where(notification => notification.UserID == userId);

        public IQuestion GetQuestion(long questionId) => questions[questionId];

        public IEnumerable<IQuestion> GetQuestionsOfUser(long userId) => questions.Values.Where(question => question.UserID == userId);

        public IEnumerable<IReaction> GetReactionsOfPostByPostID(long postId) => posts[postId].Reactions;
        public void AddPostReply(IPost reply, long postId) => replies.Add(postId, reply);
        public IEnumerable<IPost> GetRepliesOfPost(long postId)
        {
            List<IPost> targetedReplies = new ();

            foreach (var reply in replies)
            {
                if (reply.Key == postId)
                {
                    targetedReplies.Add(reply.Value);
                }
            }

            return targetedReplies;
        }

        public IEnumerable<ITag> GetTagsOfQuestion(long questionId) => questions[questionId].Tags;

        public IUser GetUser(long userId) => users[userId];

        public IPost GetPost(long postId) => posts[postId];

        public void UpdatePost(IPost oldPost, IPost newPost)
        {
            posts[oldPost.ID].Content = newPost.Content;
            posts[oldPost.ID].DateOfLastEdit = DateTime.Now;
            posts[oldPost.ID].Reactions = newPost.Reactions;
        }
    }
}
