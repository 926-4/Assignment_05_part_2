using UBB_SE_2024_Team_42.Domain.Category;
using UBB_SE_2024_Team_42.Domain.Tag;

namespace UBB_SE_2024_Team_42.Domain.Posts
{
    public interface IQuestion : IPost
    {
        ICategory? Category { get; }
        List<ITag> Tags { get; set; }
        string? Title { get; set; }
    }
}