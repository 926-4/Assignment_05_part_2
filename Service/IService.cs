using UBB_SE_2024_Team_42.Domain.Badge;
using UBB_SE_2024_Team_42.Domain.Category;
using UBB_SE_2024_Team_42.Domain.Post;
using UBB_SE_2024_Team_42.Domain.Post.Interfaces;
using UBB_SE_2024_Team_42.Domain.Tag;
using UBB_SE_2024_Team_42.Domain.User;

namespace UBB_SE_2024_Team_42.Service
{
    public interface IService
    {
        void AddQuestion(string title, string content, Category category);
        int FilterQuestionsAnsweredLastYear();
        int FilterQuestionsAnsweredThisMonth();
        int FilterQuestionsByLast7Days();
        List<IQuestion> FindQuestionsByPartialStringInAnyField(string textToBeSearchedBy);
        List<ICategory> GetAllCategories();
        List<IQuestion> GetAllQuestions();
        List<IAnswer> GetAnswersOfUser(long userId);
        List<IBadge> GetBadgesOfUser(long userId);
        List<IComment> GetCommentsOfUser(long userId);
        List<IQuestion> GetCurrentQuestions();
        IQuestion GetQuestion(long userId);
        List<IQuestion> GetQuestionsOfCategory(ICategory? category);
        List<IQuestion> GetQuestionsOfUser(long userId);
        List<IQuestion> GetQuestionsSortedByScoreAscending();
        List<IQuestion> GetQuestionsSortedByScoreDescending();
        List<IQuestion> GetQuestionsWithAtLeastOneAnswer();
        List<IPost> GetRepliesOfPost(long postId);
        List<ITag> GetTagsOfQuestion(long questionId);
        IUser GetUser(long userId);
        List<IQuestion> SortQuestionsByDateAscending();
        List<IQuestion> SortQuestionsByDateDescending();
        List<IQuestion> SortQuestionsByNumberOfAnswersAscending();
        List<IQuestion> SortQuestionsByNumberOfAnswersDescending();
        void UpdatePost(IPost oldPost, IPost newPost);
    }
}