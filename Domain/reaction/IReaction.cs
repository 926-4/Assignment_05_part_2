namespace UBB_SE_2024_Team_42.Domain.Reactions
{
    public interface IReaction
    {
        long UserID { get; }
        int Value { get; set; }
    }
}
