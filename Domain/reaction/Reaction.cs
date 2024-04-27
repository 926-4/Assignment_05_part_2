namespace UBB_SE_2024_Team_42.Domain.Reactions
{
    public class Reaction(int voteValue, long voterID) : IReaction
    {
        public int ReactionValue { get; set; } = voteValue;
        public long ReacterUserID { get; } = voterID;

        public override string ToString()
        {
            return $"ReactionValue: {ReactionValue}, ReacterUserID: {ReacterUserID}) \n";
        }
    }
}
