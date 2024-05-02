using System.Drawing;
using UBB_SE_2024_Team_42.Utils;
namespace UBB_SE_2024_Team_42.Domain.Badge
{
    public class Badge : IBadge
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Image? Image { get; set; }
        public Badge()
        {
            ID = IDGenerator.Default();
            Name = "New Badge";
            Description = "None provided";
            Image = null;
        }
        public override string ToString()
        {
            return $"Badge(id: {ID}, badgeName: {Name}, image: {Image})"
                + $"badgeDescription: {Description}";
        }
    }
}

