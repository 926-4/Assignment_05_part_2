using UBB_SE_2024_Team_42.Domain.Category;
using UBB_SE_2024_Team_42.Domain.Post.Interfaces;
using UBB_SE_2024_Team_42.Domain.Posts;
using UBB_SE_2024_Team_42.Domain.Reactions;
using UBB_SE_2024_Team_42.Domain.Tag;

namespace UBB_SE_2024_Team_42.Service.EntityCreationServices
{
    internal class QuestionFactory
    {
        private IQuestion instance = new Question();
        public QuestionFactory NewQuestion()
        {
            instance = new Question();
            return this;
        }
        public QuestionFactory SetId(long id)
        {
            instance.ID = id;
            return this;
        }
        public QuestionFactory SetTitle(string title)
        {
            instance.Title = title;
            return this;
        }
        public QuestionFactory SetCategory(ICategory category)
        {
            instance.Category = category;
            return this;
        }
        public QuestionFactory SetTags(List<ITag> tags)
        {
            instance.Tags = tags;
            return this;
        }
        public QuestionFactory SetUserId(long userId)
        {
            instance.UserID = userId;
            return this;
        }
        public QuestionFactory SetContent(string content)
        {
            instance.Content = content;
            return this;
        }
        public QuestionFactory SetPostTime(DateTime postTime)
        {
            instance.DatePosted = postTime;
            return this;
        }
        public QuestionFactory SetEditTime(DateTime editTime)
        {
            instance.DateOfLastEdit = editTime;
            return this;
        }
        public QuestionFactory SetVoteList(List<IReaction> reactions)
        {
            instance.Reactions = reactions;
            return this;
        }
        public IQuestion GetQuestion()
        {
            IQuestion returnValue = instance;
            instance = new Question();
            return returnValue;
        }
    }
}
