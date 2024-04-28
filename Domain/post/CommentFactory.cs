using UBB_SE_2024_Team_42.Domain.Reactions;

namespace UBB_SE_2024_Team_42.Domain.Posts
{
    internal class CommentFactory
    {
        public Comment Instance = new ();

        public CommentFactory NewAnswer()
        {
            Instance = new ();
            return this;
        }
        public CommentFactory SetUserId(long userId)
        {
            Instance.UserID = userId;
            return this;
        }
        public CommentFactory SetContent(string content)
        {
            Instance.Content = content;
            return this;
        }
        public CommentFactory SetDatePosted(DateTime datePosted)
        {
            Instance.DatePosted = datePosted;
            return this;
        }
        public CommentFactory SetDateOfLastEdit(DateTime dateOfLastEdit)
        {
            Instance.DateOfLastEdit = dateOfLastEdit;
            return this;
        }
        public CommentFactory SetReactions(List<IReaction> reactions)
        {
            Instance.Reactions = reactions;
            return this;
        }
        public Comment Get()
        {
            Comment comment = Instance;
            Instance = new ();
            return comment;
        }
    }
}
