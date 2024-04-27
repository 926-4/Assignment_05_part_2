namespace UBB_SE_2024_Team_42.Domain.tag
{
    public interface ITag
    {
        long Id { get; }
        string Name { get; set; }
        string ToString() => $"ITag {{id: {Id}, name: {Name}}}";
    }
}