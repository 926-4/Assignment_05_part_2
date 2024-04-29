using System.Drawing;
using UBB_SE_2024_Team_42.Domain.Badge;

namespace UBB_SE_2024_Team_42.Service.EntityCreationServices
{
    internal class BadgeFactory
    {
        private Badge instance = new ();
        public BadgeFactory NewBadge()
        {
            instance = new ();
            return this;
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
        public Badge Get()
        {
            Badge returnValue = instance;
            instance = new ();
            return returnValue;
        }

        internal BadgeFactory SetID(long id)
        {
            instance.ID = id;
            return this;
        }
    }
}
