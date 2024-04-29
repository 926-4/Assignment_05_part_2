namespace UBB_SE_2024_Team_42.Domain.Reactions
{
    public class Reaction : IReaction
    {
        public int Value { get; set; }
        public long UserID { get; set; }
        public Reaction(long voterId)
        {
            UserID = voterId;
            Value = 0;
        }
        internal Reaction(int voteValue, long voterID)
        {
            Value = voteValue;
            UserID = voterID;
        }

        public Reaction()
        {
        }

        public override string ToString()
        {
            return $"Value: {Value}, ID: {UserID}) \n";
        }
    }
}
