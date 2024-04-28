using UBB_SE_2024_Team_42.Domain.Reactions;

namespace UBB_SE_2024_Team_42.Domain.Posts
{
    internal class TextPostFactory
    {
        public TextPost Instance = new ();

        public TextPostFactory NewAnswer()
        {
            Instance = new ();
            return this;
        }
        public TextPostFactory SetUserId(long userId)
        {
            Instance.UserID = userId;
            return this;
        }
        public TextPostFactory SetContent(string content)
        {
            Instance.Content = content;
            return this;
        }
        public TextPostFactory SetDatePosted(DateTime datePosted)
        {
            Instance.DatePosted = datePosted;
            return this;
        }
        public TextPostFactory SetDateOfLastEdit(DateTime dateOfLastEdit)
        {
            Instance.DateOfLastEdit = dateOfLastEdit;
            return this;
        }
        public TextPostFactory SetReactions(List<IReaction> reactions)
        {
            Instance.Reactions = reactions;
            return this;
        }
        public TextPost Get()
        {
            TextPost textPost = Instance;
            Instance = new ();
            return textPost;
        }
    }
}
