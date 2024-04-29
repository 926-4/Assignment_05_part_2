using UBB_SE_2024_Team_42.Domain.Category;
using UBB_SE_2024_Team_42.Domain.Reactions;
using UBB_SE_2024_Team_42.Domain.Tag;

namespace UBB_SE_2024_Team_42.Domain.Posts
{
    internal class QuestionFactory
    {
        public Question? Instance;
        public QuestionFactory NewQuestion()
        {
            Instance = new ();
            return this;
        }
        public QuestionFactory SetId(long id)
        {
            Instance.ID = id;
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
        public QuestionFactory SetUserId(long userId)
        {
            Instance.UserID = userId;
            return this;
        }
        public QuestionFactory SetContent(string content)
        {
            Instance.Content = content;
            return this;
        }
        public QuestionFactory SetPostTime(DateTime postTime)
        {
            Instance.DatePosted = postTime;
            return this;
        }
        public QuestionFactory SetEditTime(DateTime editTime)
        {
            Instance.DateOfLastEdit = editTime;
            return this;
        }
        public QuestionFactory SetVoteList(List<Reactions.IReaction> reactions)
        {
            Instance.Reactions = reactions;
            return this;
        }
        public Question GetQuestion()
        {
            Question returnValue = Instance;
            Instance = null;
            return returnValue;
        }
    }
}
