using UBB_SE_2024_Team_42.Domain.Reactions;
using UBB_SE_2024_Team_42.Utils.Functionals;

namespace UBB_SE_2024_Team_42.Domain
{
    public interface IPost
    {
        long ID { get; set; }
        long UserID { get; set; }
        string Content { get; set; }
        DateTime DatePosted { get; set; }
        DateTime DateOfLastEdit { get; set; }
        List<IReaction> Reactions { get; set; }
        private static readonly Func<IReaction, int> MapReactionToInt = (IReaction ireaction) => ireaction.Value;
        int Score() => CollectionSummerFactory<IReaction>.GetFromMapping(MapReactionToInt).ApplyTo(Reactions);
        void AddReaction(IReaction reaction) => Reactions.Add(reaction);
    }
}
