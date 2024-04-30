using System.Drawing;
using UBB_SE_2024_Team_42.Domain.Badge;

namespace UBB_SE_2024_Team_42.Service.EntityCreationServices
{
    public class BadgeFactory : AbstractEntityFactory<IBadge, Badge>
    {
        public override BadgeFactory Begin()
        {
            return (BadgeFactory)base.Begin();
        }
        public BadgeFactory SetName(string name)
        {
            instance.Name = name;
            return this;
        }
        public BadgeFactory SetDescription(string description)
        {
            instance.Description = description;
            return this;
        }
        public BadgeFactory SetImage(Image? image)
        {
            instance.Image = image;
            return this;
        }

        internal BadgeFactory SetID(long id)
        {
            instance.ID = id;
            return this;
        }
    }
}
