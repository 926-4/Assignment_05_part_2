using UBB_SE_2024_Team_42.Domain.Reactions;

namespace UBB_SE_2024_Team_42.Domain.Posts
{
    internal class CommentFactory
    {
        private Comment instance = new ();

        public CommentFactory NewAnswer()
        {
            instance = new ();
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
        public Comment Get()
        {
            Comment comment = instance;
            instance = new ();
            return comment;
        }
    }
}
