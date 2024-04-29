namespace UBB_SE_2024_Team_42.Domain.Tag
{
    public interface ITag
    {
        long Id { get; set; }
        string Name { get; set; }
        string ToString() => $"ITag {{id: {Id}, name: {Name}}}";
    }
}