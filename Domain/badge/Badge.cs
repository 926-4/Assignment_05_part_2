using System.Drawing;
namespace UBB_SE_2024_Team_42.Domain.badge
{
    public class Badge(long newBadgeID, string newBadgeName, string newBadgeDescription, Image newImage) : IBadge
    {
        public long ID { get; set; } = newBadgeID;
        public string Name { get; set; } = newBadgeName;
        public string Description { get; set; } = newBadgeDescription;
        public Image Image { get; set; } = newImage;

        public override string ToString()
        {
            return $"Badge(badgeID: {ID}, badgeName: {Name}, image: {Image}) \n" 
                + $"badgeDescription: {Description} \n";
        }
    }
}

