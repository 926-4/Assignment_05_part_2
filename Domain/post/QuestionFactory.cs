using UBB_SE_2024_Team_42.Domain.Category;
using UBB_SE_2024_Team_42.Domain.Reactions;
using UBB_SE_2024_Team_42.Domain.Tag;

namespace UBB_SE_2024_Team_42.Domain.Posts
{
    internal class QuestionFactory
    {
        public Question Instance = new ();
        public QuestionFactory NewQuestion()
        {
            Instance = new ();
            return this;
        }
        public QuestionFactory SetUserId(long userId)
        {
            Instance.Post.UserID = userId;
            return this;
        }
        public QuestionFactory SetContent(string content)
        {
            Instance.Post.Content = content;
            return this;
        }
        public QuestionFactory SetDatePosted(DateTime datePosted)
        {
            Instance.Post.DatePosted = datePosted;
            return this;
        }
        public QuestionFactory SetDateOfLastEdit(DateTime dateOfLastEdit)
        {
            Instance.Post.DateOfLastEdit = dateOfLastEdit;
            return this;
        }
        public QuestionFactory SetReactions(List<IReaction> reactions)
        {
            Instance.Post.Reactions = reactions;
            return this;
        }
        public QuestionFactory SetTitle(string title)
        {
            Instance.Title = title;
            return this;
        }
        public QuestionFactory SetCategory(ICategory category)
        {
            Instance.Category = category;
            return this;
        }
        public QuestionFactory SetTags(List<ITag> tags)
        {
            Instance.Tags = tags;
            return this;
        }
        public Question Get()
        {
            Question question = Instance;
            Instance = new ();
            return question;
        }
    }
}
