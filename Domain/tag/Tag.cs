using UBB_SE_2024_Team_42.Utils;

namespace UBB_SE_2024_Team_42.Domain.Tag
{
    public class Tag : ITag
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public Tag()
        {
            Id = IDGenerator.RandomLong();
            Name = "None";
        }
        internal Tag(long newTagId, string newTagName)
        {
            Id = newTagId;
            Name = newTagName;
        }
        public override string ToString() => $"Tag {{id: {Id}, name: {Name}}}";
    }
}
