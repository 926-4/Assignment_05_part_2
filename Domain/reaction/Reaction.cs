namespace UBB_SE_2024_Team_42.Domain.Reactions
{
    public class Reaction : IReaction
    {
        public int ReactionValue { get; set; }
        public long ReacterUserID { get; }
        public Reaction(long voterId)
        {
            ReacterUserID = voterId;
            ReactionValue = 0;
        }
        internal Reaction(int voteValue, long voterID)
        {
            ReactionValue = voteValue;
            ReacterUserID = voterID;
        }
        public override string ToString()
        {
            return $"ReactionValue: {ReactionValue}, ReacterUserID: {ReacterUserID}) \n";
        }
    }
}
