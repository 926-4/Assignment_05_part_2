using UBB_SE_2024_Team_42.Utils;

namespace UBB_SE_2024_Team_42.Domain.Reactions
{
    public class Reaction : IReaction
    {
        public int Value { get; set; }
        public long UserID { get; set; }
        public Reaction()
        {
            Value = 0;
            UserID = IDGenerator.Default();
        }

        public override string ToString()
        {
            return $"Value: {Value}, ID: {UserID}) \n";
        }
    }
}
