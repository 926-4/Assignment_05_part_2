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
            ID = IDGenerator.RandomLong();
            Name = "New Badge";
            Description = "None provided";
            Image = null;
        }
        public Badge(long newBadgeID, string newBadgeName, string newBadgeDescription, Image newImage)
        {
            ID = newBadgeID;
            Name = newBadgeName;
            Description = newBadgeDescription;
            Image = newImage;
        }
        public override string ToString()
        {
            return $"Badge(badgeID: {ID}, badgeName: {Name}, image: {Image}) \n"
                + $"badgeDescription: {Description} \n";
        }
    }
}

