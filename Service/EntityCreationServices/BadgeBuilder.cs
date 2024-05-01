using System.Drawing;
using UBB_SE_2024_Team_42.Domain.Badge;

namespace UBB_SE_2024_Team_42.Service.EntityCreationServices
{
    public class BadgeBuilder : AbstractEntityBuilder<IBadge, Badge>
    {
        public override BadgeBuilder Begin()
        {
            return (BadgeBuilder)base.Begin();
        }
        public BadgeBuilder SetName(string name)
        {
            instance.Name = name;
            return this;
        }
        public BadgeBuilder SetDescription(string description)
        {
            instance.Description = description;
            return this;
        }
        public BadgeBuilder SetImage(Image? image)
        {
            instance.Image = image;
            return this;
        }

        internal BadgeBuilder SetID(long id)
        {
            instance.ID = id;
            return this;
        }
    }
}
