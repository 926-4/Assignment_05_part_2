using UBB_SE_2024_Team_42.Domain.Badge;
using UBB_SE_2024_Team_42.Domain.Category;
using UBB_SE_2024_Team_42.Domain.Notification;

namespace UBB_SE_2024_Team_42.Domain.User
{
    internal class UserFactory
    {
        public User Instance = new ();
        public UserFactory NewUser()
        {
            Instance = new ();
            return this;
        }
        public UserFactory SetName(string name)
        {
            Instance.UserName = name;
            return this;
        }
        public UserFactory SetNotificationList(List<INotification> notifications)
        {
            Instance.NotificationList = notifications;
            return this;
        }
        public UserFactory SetCategoriesModeratedList(List<ICategory> categories)
        {
            Instance.CategoriesModeratedList = categories;
            return this;
        }
        public UserFactory SetBadgeList(List<IBadge> badges)
        {
            Instance.BadgeList = badges;
            return this;
        }
        public User Get()
        {
            User returnValue = Instance;
            Instance = new ();
            return returnValue;
        }
    }
}
