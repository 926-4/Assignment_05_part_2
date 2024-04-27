namespace UBB_SE_2024_Team_42.Domain.Reactions
{
    public interface IReaction
    {
        long ReacterUserID { get; }
        int ReactionValue { get; set; }
    }
}
