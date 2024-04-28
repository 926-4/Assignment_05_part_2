using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace UBB_SE_2024_Team_42.Domain.user
{
    internal class UserFactory
    {
        public User? Instance;
        public UserFactory NewUser()
        {
            Instance = new();
            return this;
        }
        public UserFactory SetName(string name)
        {
            Instance.UserName = name;
            return this;
        }
        public UserFactory SetNotificationList(List<notification.Notification> notifications)
        {
            Instance.NotificationList = notifications;
            return this;
        }
        public UserFactory SetCategoriesModeratedList(List<category.Category> categories)
        {
            Instance.CategoriesModeratedList = categories;
            return this;
        }
        public UserFactory SetBadgeList(List<badge.Badge> badges)
        {
            Instance.BadgeList = badges;
            return this;
        }
        public User Get()
        {
            User returnValue = Instance;
            Instance = null;
            return returnValue;
        }
    }
}
