using UBB_SE_2024_Team_42.Utils;

namespace UBB_SE_2024_Team_42.Domain.Tag
{
    public class Tag : ITag
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public Tag()
        {
            Id = IDGenerator.Default();
            Name = "None";
        }
        public override string ToString() => $"Tag {{id: {Id}, name: {Name}}}";
    }
}
