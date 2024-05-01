using UBB_SE_2024_Team_42.Domain.Post;
using UBB_SE_2024_Team_42.Domain.Reactions;

namespace UBB_SE_2024_Team_42.Service.EntityCreationServices
{
    public class CommentBuilder : AbstractEntityBuilder<IComment, Comment>
    {
        public override CommentBuilder Begin()
            => (CommentBuilder)base.Begin();
        public CommentBuilder SetId(long id)
        {
            instance.ID = id;
            return this;
        }
        public CommentBuilder SetUserId(long userId)
        {
            instance.UserID = userId;
            return this;
        }
        public CommentBuilder SetContent(string content)
        {
            instance.Content = content;
            return this;
        }
        public CommentBuilder SetDatePosted(DateTime datePosted)
        {
            instance.DatePosted = datePosted;
            return this;
        }
        public CommentBuilder SetDateOfLastEdit(DateTime dateOfLastEdit)
        {
            instance.DateOfLastEdit = dateOfLastEdit;
            return this;
        }
        public CommentBuilder SetReactions(List<IReaction> reactions)
        {
            instance.Reactions = reactions;
            return this;
        }
    }
}
