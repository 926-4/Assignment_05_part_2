using UBB_SE_2024_Team_42.Domain.category;
using UBB_SE_2024_Team_42.Domain.tag;

namespace UBB_SE_2024_Team_42.Domain.Posts
{
    public interface IQuestion : IPost
    {
        ICategory? Category { get; }
        List<ITag> Tags { get; set; }
        string? Title { get; set; }
    }
}