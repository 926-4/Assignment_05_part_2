using UBB_SE_2024_Team_42.Domain.Post.Interfaces;
using UBB_SE_2024_Team_42.Domain.Posts;
using UBB_SE_2024_Team_42.Domain.Reactions;

namespace UBB_SE_2024_Team_42.Service.EntityCreationServices
{
    public class TextPostFactory : AbstractEntityFactory<IPost, TextPost>
    {
        public override TextPostFactory Begin()
            => (TextPostFactory)base.Begin();
        public TextPostFactory SetID(long id)
        {
            instance.ID = id;
            return this;
        }
        public TextPostFactory SetUserId(long userId)
        {
            instance.UserID = userId;
            return this;
        }
        public TextPostFactory SetContent(string content)
        {
            instance.Content = content;
            return this;
        }
        public TextPostFactory SetDatePosted(DateTime datePosted)
        {
            instance.DatePosted = datePosted;
            return this;
        }
        public TextPostFactory SetDateOfLastEdit(DateTime dateOfLastEdit)
        {
            instance.DateOfLastEdit = dateOfLastEdit;
            return this;
        }
        public TextPostFactory SetReactions(List<IReaction> reactions)
        {
            instance.Reactions = reactions;
            return this;
        }
    }
}
