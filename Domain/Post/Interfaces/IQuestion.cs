using UBB_SE_2024_Team_42.Domain.Category;
using UBB_SE_2024_Team_42.Domain.Tag;

namespace UBB_SE_2024_Team_42.Domain.Post.Interfaces
{
    public interface IQuestion : IPost
    {
        ICategory? Category { get; set; }
        List<ITag> Tags { get; set; }
        string? Title { get; set; }
    }
}