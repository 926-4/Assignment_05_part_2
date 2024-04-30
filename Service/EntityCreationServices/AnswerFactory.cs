using UBB_SE_2024_Team_42.Domain.Notification;
using UBB_SE_2024_Team_42.Domain.Post.Interfaces;
using UBB_SE_2024_Team_42.Domain.Posts;
using UBB_SE_2024_Team_42.Domain.Reactions;

namespace UBB_SE_2024_Team_42.Service.EntityCreationServices
{
    internal class AnswerFactory : AbstractEntityFactory<IAnswer, Answer>
    {
        public override AnswerFactory Begin()
        {
            return (AnswerFactory)base.Begin();
        }
        public AnswerFactory SetId(long id)
        {
            instance.ID = id;
            return this;
        }
        public AnswerFactory SetUserId(long userId)
        {
            instance.UserID = userId;
            return this;
        }
        public AnswerFactory SetContent(string content)
        {
            instance.Content = content;
            return this;
        }
        public AnswerFactory SetDatePosted(DateTime datePosted)
        {
            instance.DatePosted = datePosted;
            return this;
        }
        public AnswerFactory SetDateOfLastEdit(DateTime dateOfLastEdit)
        {
            instance.DateOfLastEdit = dateOfLastEdit;
            return this;
        }
        public AnswerFactory SetReactions(List<IReaction> reactions)
        {
            instance.Reactions = reactions;
            return this;
        }
    }
}
