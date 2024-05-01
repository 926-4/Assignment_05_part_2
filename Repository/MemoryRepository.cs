using System.Linq;
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
        private readonly Dictionary<long, IQuestion> questions;
        private readonly Dictionary<long, IUser> users;
        private readonly Dictionary<long, long> badgeIdToUserIdAssociation;
        public MemoryRepository()
        {
            // TODO pune aici valori
            Badge b1 = new() { Name = "a" };
            Badge b2 = new() { Name = "b", ID = IDGenerator.RandomLong() };
            Category c1 = new() { Name = "a" };
            Category c2 = new() { Name = "b", ID = IDGenerator.RandomLong() };
            User u1 = new() { Name = "a", CategoriesModeratedList = [c1] };
            User u2 = new() { ID = IDGenerator.RandomLong(), CategoriesModeratedList = [c2] };
            Notification n1 = new() { Text = "a", UserID = u1.ID };
            Notification n2 = new() { Text = "b", ID = IDGenerator.RandomLong(), UserID = u2.ID };
            TextPost p1 = new() { Content = "a", UserID = u1.ID };
            TextPost p2 = new() { Content = "b", ID = IDGenerator.RandomLong(), UserID = u1.ID };
            Question q1 = new() { Content = "a", UserID = u1.ID };
            Question q2 = new() { Content = "b", ID = IDGenerator.RandomLong(), UserID = u1.ID };
            Comment c = new() { ID = 2, Content = "a", UserID = u1.ID };
            Answer a = new() { ID = 3, Content = "a", UserID = u1.ID };
            badges = new Dictionary<long, IBadge>()
            {
                {b1.ID, b1 },
                {b2.ID, b2 }
            };
            categories = new Dictionary<long, ICategory>()
            {
                {c1.ID,c1 },
                {c2.ID,c2 }
            };
            notifications = new Dictionary<long, INotification>()
            {
                {n1.ID, n1},
                {n2.ID, n2 }
            };
            posts = new Dictionary<long, IPost>()
            {
                {p1.ID, p1},
                {p2.ID,p2 },
                {c.ID, c },
                {a.ID, a }
            };
            questions = new Dictionary<long, IQuestion>()
            {
                {q1.ID, q1 },
                {q2.ID, q2 }
            };
            users = new Dictionary<long, IUser>()
            {
                {u1.ID, u1 },
                {u2.ID, u2 }
            };
            badgeIdToUserIdAssociation = new Dictionary<long, long>()
            {
                {b1.ID, u1.ID },
                {b2.ID, u2.ID }
            };
        }
        private IAnswer MapIPostToIAnswer(IPost ipost) => (IAnswer)ipost;
        public void AddQuestion(IQuestion question) => questions[question.ID] = question;

        public IEnumerable<ICategory> GetAllCategories() => categories.Values;

        public IEnumerable<IQuestion> GetAllQuestions() => questions.Values;

        public void AddUser(IUser user)
        {
            users.Add(user.ID, user);
        }

        public void AddPost(IPost post)
        {
            posts.Add(post.ID, post);
        }

        public IEnumerable<IUser> GetAllUsers() => users.Values;

        public IEnumerable<IAnswer> GetAnswersOfUser(long userId) => posts.Values.Where(Filters.IPostIsIAnswer).Select(MapIPostToIAnswer).Where(ianswer => ianswer.UserID == userId);

        public IEnumerable<IBadge> GetBadgesOfUser(long userId) => badges.Values.Where(badge => badgeIdToUserIdAssociation[badge.ID] == userId);

        public IEnumerable<ICategory> GetCategoriesModeratedByUser(long userId) => users[userId].CategoriesModeratedList;
        public ICategory GetCategoryByID(long categoryId) => categories[categoryId];

        public IEnumerable<IComment> GetCommentsOfUser(long userId) => posts.Values.Where(ipost => ipost is IComment).Select(ipost => (IComment)ipost);

        public IEnumerable<INotification> GetNotificationsOfUser(long userId) => notifications.Values.Where(notification => notification.UserID == userId);

        public IQuestion GetQuestion(long questionId) => questions[questionId];

        public IEnumerable<IQuestion> GetQuestionsOfUser(long userId) => questions.Values.Where(question => question.UserID == userId);

        public IEnumerable<IReaction> GetReactionsOfPostByPostID(long postId) => posts[postId].Reactions;

        public IEnumerable<IPost> GetRepliesOfPost(long postId) => posts.Values;

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
