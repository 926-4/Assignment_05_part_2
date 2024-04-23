using System.Drawing;
namespace UBB_SE_2024_Team_42.Domain
{
    public class Badge(long newBadgeID, string newBadgeName, string newBadgeDescription, Image newImage)
    {
        public long BadgeID { get; set; } = newBadgeID;
        public string BadgeName { get; set; } = newBadgeName;
        public string BadgeDescription { get; set; } = newBadgeDescription;
        public Image Image { get; set; } = newImage;

        public override string ToString()
        {
            return $"Badge(badgeID: {BadgeID}, badgeName: {BadgeName}, image: {Image}) \n" + $"badgeDescription: {BadgeDescription} \n";
        }
    }
}

