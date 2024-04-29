using UBB_SE_2024_Team_42.Domain.Post;
using UBB_SE_2024_Team_42.Domain.Reactions;

namespace UBB_SE_2024_Team_42.Service.EntityCreationServices
{
    internal class CommentFactory
    {
        private IComment instance = new Comment();

        public CommentFactory NewComment()
        {
            instance = new Comment();
            return this;
        }
        public CommentFactory SetId(long id)
        {
            instance.ID = id;
            return this;
        }
        public CommentFactory SetUserId(long userId)
        {
            instance.UserID = userId;
            return this;
        }
        public CommentFactory SetContent(string content)
        {
            instance.Content = content;
            return this;
        }
        public CommentFactory SetDatePosted(DateTime datePosted)
        {
            instance.DatePosted = datePosted;
            return this;
        }
        public CommentFactory SetDateOfLastEdit(DateTime dateOfLastEdit)
        {
            instance.DateOfLastEdit = dateOfLastEdit;
            return this;
        }
        public CommentFactory SetReactions(List<IReaction> reactions)
        {
            instance.Reactions = reactions;
            return this;
        }
        public IComment Get()
        {
            IComment comment = instance;
            instance = new Comment();
            return comment;
        }
    }
}
