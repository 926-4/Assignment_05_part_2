using UBB_SE_2024_Team_42.Domain.Notification;
using UBB_SE_2024_Team_42.Domain.Post.Interfaces;
using UBB_SE_2024_Team_42.Domain.Posts;
using UBB_SE_2024_Team_42.Domain.Reactions;

namespace UBB_SE_2024_Team_42.Service.EntityCreationServices
{
    public class AnswerBuilder : AbstractEntityBuilder<IAnswer, Answer>
    {
        public override AnswerBuilder Begin()
        {
            return (AnswerBuilder)base.Begin();
        }
        public AnswerBuilder SetId(long id)
        {
            instance.ID = id;
            return this;
        }
        public AnswerBuilder SetUserId(long userId)
        {
            instance.UserID = userId;
            return this;
        }
        public AnswerBuilder SetContent(string content)
        {
            instance.Content = content;
            return this;
        }
        public AnswerBuilder SetDatePosted(DateTime datePosted)
        {
            instance.DatePosted = datePosted;
            return this;
        }
        public AnswerBuilder SetDateOfLastEdit(DateTime dateOfLastEdit)
        {
            instance.DateOfLastEdit = dateOfLastEdit;
            return this;
        }
        public AnswerBuilder SetReactions(List<IReaction> reactions)
        {
            instance.Reactions = reactions;
            return this;
        }
    }
}
