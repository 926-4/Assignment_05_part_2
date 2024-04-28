using System.Drawing;

namespace UBB_SE_2024_Team_42.Domain.Badge
{
    internal class BadgeFactory
    {
        public Badge Instance = new ();
        public BadgeFactory NewBadge()
        {
            Instance = new ();
            return this;
        }
        public BadgeFactory SetName(string name)
        {
            Instance.Name = name;
            return this;
        }
        public BadgeFactory SetDescription(string description)
        {
            Instance.Description = description;
            return this;
        }
        public BadgeFactory SetImage(Image image)
        {
            Instance.Image = image;
            return this;
        }
        public Badge Get()
        {
            Badge returnValue = Instance;
            Instance = new ();
            return returnValue;
        }
    }
}
