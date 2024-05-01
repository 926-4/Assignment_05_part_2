using System.Linq;
using UBB_SE_2024_Team_42.Domain.Badge;
using UBB_SE_2024_Team_42.Domain.Category;
using UBB_SE_2024_Team_42.Domain.Notification;
using UBB_SE_2024_Team_42.Domain.Post;
using UBB_SE_2024_Team_42.Domain.Post.Interfaces;
using UBB_SE_2024_Team_42.Domain.Reactions;
using UBB_SE_2024_Team_42.Domain.Tag;
using UBB_SE_2024_Team_42.Domain.User;
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
            badges = new Dictionary<long, IBadge>();
            categories = new Dictionary<long, ICategory>();
            notifications = new Dictionary<long, INotification>();
            posts = new Dictionary<long, IPost>();
            questions = new Dictionary<long, IQuestion>();
            users = new Dictionary<long, IUser>();
            badgeIdToUserIdAssociation = new Dictionary<long, long>();
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

        public IEnumerable<IPost> GetRepliesOfPost(long postId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ITag> GetTagsOfQuestion(long questionId)
        {
            throw new NotImplementedException();
        }

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
