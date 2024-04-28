using System.Drawing;

namespace UBB_SE_2024_Team_42.Domain.Badge
{
    public interface IBadge
    {
        string Description { get; set; }
        long ID { get; set; }
        string Name { get; set; }
        Image? Image { get; set; }
    }
}