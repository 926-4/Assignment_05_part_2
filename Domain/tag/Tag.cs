namespace UBB_SE_2024_Team_42.Domain.tag
{
    public class Tag(long newTagId, string newTagName) : ITag
    {
        public long Id { get; } = newTagId;
        public string Name { get; set; } = newTagName;
        public override string ToString() => $"Tag {{id: {Id}, name: {Name}}}";
    }
}
