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
        List<ICategory> GetAllCategories();
        List<IQuestion> GetAllQuestions();
        List<IUser> GetAllUsers();
        List<IAnswer> GetAnswersOfUser(long userId);
        List<IBadge> GetBadgesOfUser(long userId);
        List<ICategory> GetCategoriesModeratedByUser(long userId);
        ICategory GetCategoryByID(long categoryId);
        List<IComment> GetCommentsOfUser(long userId);
        List<INotification> GetNotificationsOfUser(long userId);
        IQuestion GetQuestion(long questionId);
        List<IQuestion> GetQuestionsOfUser(long userId);
        List<IReaction> GetReactionsOfPostByPostID(long postId);
        List<IPost> GetRepliesOfPost(long postId);
        List<ITag> GetTagsOfQuestion(long questionId);
        IUser GetUser(long userId);
        void UpdatePost(IPost oldPost, IPost newPost);
    }
}