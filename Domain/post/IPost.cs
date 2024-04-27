using UBB_SE_2024_Team_42.Domain.Reactions;
using UBB_SE_2024_Team_42.Utils;

namespace UBB_SE_2024_Team_42.Domain
{
    public interface IPost
    {
        long PostID { get; }
        long UserID { get; }
        string Content { get; set; }
        DateTime DatePosted { get; }
        DateTime DateOfLastEdit { get; set; }
        List<IReaction> Reactions { get; set; }
        int Score() => Reactions.Select(reaction => reaction.ReactionValue).Aggregate((r1, r2) => r1 + r2);
        void AddReaction(IReaction reaction) => Reactions.Add(reaction) ;
    }
}
