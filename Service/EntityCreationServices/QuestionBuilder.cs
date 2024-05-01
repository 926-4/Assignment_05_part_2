using UBB_SE_2024_Team_42.Domain.Category;
using UBB_SE_2024_Team_42.Domain.Post.Interfaces;
using UBB_SE_2024_Team_42.Domain.Posts;
using UBB_SE_2024_Team_42.Domain.Reactions;
using UBB_SE_2024_Team_42.Domain.Tag;

namespace UBB_SE_2024_Team_42.Service.EntityCreationServices
{
    public class QuestionBuilder : AbstractEntityBuilder<IQuestion, Question>
    {
        public override QuestionBuilder Begin()
            => (QuestionBuilder)base.Begin();
        public QuestionBuilder SetId(long id)
        {
            instance.ID = id;
            return this;
        }
        public QuestionBuilder SetTitle(string title)
        {
            instance.Title = title;
            return this;
        }
        public QuestionBuilder SetCategory(ICategory category)
        {
            instance.Category = category;
            return this;
        }
        public QuestionBuilder SetTags(List<ITag> tags)
        {
            instance.Tags = tags;
            return this;
        }
        public QuestionBuilder SetUserId(long userId)
        {
            instance.UserID = userId;
            return this;
        }
        public QuestionBuilder SetContent(string content)
        {
            instance.Content = content;
            return this;
        }
        public QuestionBuilder SetPostTime(DateTime postTime)
        {
            instance.DatePosted = postTime;
            return this;
        }
        public QuestionBuilder SetEditTime(DateTime editTime)
        {
            instance.DateOfLastEdit = editTime;
            return this;
        }
        public QuestionBuilder SetReactions(List<IReaction> reactions)
        {
            instance.Reactions = reactions;
            return this;
        }
    }
}
