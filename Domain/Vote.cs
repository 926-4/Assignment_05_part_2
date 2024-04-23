namespace UBB_SE_2024_Team_42.Domain
{
    public class Vote(int newVoteValue, long newUserIDWhoVoted)
    {
        public int VoteValue { get; set; } = newVoteValue;
        public long UserIDWhoVoted { get; } = newUserIDWhoVoted;

        public override string ToString()
        {
            return $"VoteValue: {VoteValue}, userID: {UserIDWhoVoted}) \n";
        }
    }
}
