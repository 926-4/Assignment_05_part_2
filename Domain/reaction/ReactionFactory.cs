namespace UBB_SE_2024_Team_42.Domain.Reactions
{
    internal class ReactionFactory
    {
        private Reaction instance = new ();

        public ReactionFactory NewReaction()
        {
            instance = new ();
            return this;
        }

        public ReactionFactory SetReactionValue(int value)
        {
            instance.Value = value;
            return this;
        }

        public ReactionFactory SetReacterUserId(long userId)
        {
            instance.UserID = userId;
            return this;
        }

        public Reaction Get()
        {
            Reaction returnValue = instance;
            instance = new ();
            return returnValue;
        }
    }
}
