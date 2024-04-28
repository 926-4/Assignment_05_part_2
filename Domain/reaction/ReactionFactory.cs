namespace UBB_SE_2024_Team_42.Domain.Reactions
{
    internal class ReactionFactory
    {
        public Reaction Instance = new ();

        public ReactionFactory NewReaction()
        {
            Instance = new ();
            return this;
        }

        public ReactionFactory SetReactionValue(int value)
        {
            Instance.ReactionValue = value;
            return this;
        }

        public ReactionFactory SetReacterUserId(long userId)
        {
            Instance.ReacterUserID = userId;
            return this;
        }

        public Reaction Get()
        {
            Reaction returnValue = Instance;
            Instance = new ();
            return returnValue;
        }
    }
}
