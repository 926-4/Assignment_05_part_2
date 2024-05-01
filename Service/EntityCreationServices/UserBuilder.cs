using UBB_SE_2024_Team_42.Domain.Badge;
using UBB_SE_2024_Team_42.Domain.Category;
using UBB_SE_2024_Team_42.Domain.Notification;
using UBB_SE_2024_Team_42.Domain.User;

namespace UBB_SE_2024_Team_42.Service.EntityCreationServices
{
    public class UserBuilder : AbstractEntityBuilder<IUser, User>
    {
        public override UserBuilder Begin()
            => (UserBuilder)base.Begin();
        public UserBuilder SetName(string name)
        {
            instance.Name = name;
            return this;
        }
        public UserBuilder SetNotificationList(List<INotification> notifications)
        {
            instance.NotificationList = notifications;
            return this;
        }
        public UserBuilder SetCategoriesModeratedList(List<ICategory> categories)
        {
            instance.CategoriesModeratedList = categories;
            return this;
        }
        public UserBuilder SetBadgeList(List<IBadge> badges)
        {
            instance.BadgeList = badges;
            return this;
        }
    }
}
