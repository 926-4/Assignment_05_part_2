using UBB_SE_2024_Team_42.Domain.Badge;
using UBB_SE_2024_Team_42.Domain.Category;
using UBB_SE_2024_Team_42.Domain.Notification;
using UBB_SE_2024_Team_42.Domain.Post;
using UBB_SE_2024_Team_42.Domain.Post.Interfaces;
using UBB_SE_2024_Team_42.Domain.Reactions;
using UBB_SE_2024_Team_42.Domain.Tag;
using UBB_SE_2024_Team_42.Domain.User;

namespace UBB_SE_2024_Team_42.Repository
{
    public interface IRepository
    {
        void AddQuestion(IQuestion question);
        IEnumerable<ICategory> GetAllCategories();
        IEnumerable<IQuestion> GetAllQuestions();
        IEnumerable<IUser> GetAllUsers();
        IEnumerable<IAnswer> GetAnswersOfUser(long userId);
        IEnumerable<IBadge> GetBadgesOfUser(long userId);
        IEnumerable<ICategory> GetCategoriesModeratedByUser(long userId);
        ICategory GetCategoryByID(long categoryId);
        IEnumerable<IComment> GetCommentsOfUser(long userId);
        IEnumerable<INotification> GetNotificationsOfUser(long userId);
        IQuestion GetQuestion(long questionId);
        IEnumerable<IQuestion> GetQuestionsOfUser(long userId);
        IEnumerable<IReaction> GetReactionsOfPostByPostID(long postId);
        public void AddPostReply(IPost reply, long postId);
        IEnumerable<IPost> GetRepliesOfPost(long postId);
        IEnumerable<ITag> GetTagsOfQuestion(long questionId);
        IUser GetUser(long userId);
        public void AddPost(IPost post);
        IPost GetPost(long postId);
        void UpdatePost(IPost oldPost, IPost newPost);
    }
}