using UBB_SE_2024_Team_42.Domain.Badge;
using UBB_SE_2024_Team_42.Domain.Category;
using UBB_SE_2024_Team_42.Domain.Notification;
using UBB_SE_2024_Team_42.Domain.User;

namespace UBB_SE_2024_Team_42.Service.EntityCreationServices
{
    internal class UserFactory
    {
        private User instance = new();
        public UserFactory NewUser()
        {
            instance = new();
            return this;
        }
        public UserFactory SetName(string name)
        {
            instance.Name = name;
            return this;
        }
        public UserFactory SetNotificationList(List<INotification> notifications)
        {
            instance.NotificationList = notifications;
            return this;
        }
        public UserFactory SetCategoriesModeratedList(List<ICategory> categories)
        {
            instance.CategoriesModeratedList = categories;
            return this;
        }
        public UserFactory SetBadgeList(List<IBadge> badges)
        {
            instance.BadgeList = badges;
            return this;
        }
        public User Get()
        {
            User returnValue = instance;
            instance = new();
            return returnValue;
        }
    }
}
