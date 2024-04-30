using System.Drawing;
using UBB_SE_2024_Team_42.Domain.Badge;
using UBB_SE_2024_Team_42.Domain.Category;
using UBB_SE_2024_Team_42.Domain.Notification;
using UBB_SE_2024_Team_42.Utils;
using UBB_SE_2024_Team_42.Utils.Functionals;

namespace UBB_SE_2024_Team_42.Domain.User
{
    public class User : IUser
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public List<INotification> NotificationList { get; set; }
        public List<ICategory> CategoriesModeratedList { get; set; }
        public List<IBadge> BadgeList { get; set; }
        public Image? ProfilePicture { get; set; }
        public User()
        {
            ID = IDGenerator.Default();
            Name = string.Empty;
            NotificationList = new ();
            CategoriesModeratedList = new ();
            BadgeList = new ();
        }
        private string ToStringNotificationList() => CollectionStringifier<INotification>.ApplyTo(NotificationList);
        private string ToStringCategoryList() => CollectionStringifier<ICategory>.ApplyTo(CategoriesModeratedList);
        private string ToStringBadgeList() => CollectionStringifier<IBadge>.ApplyTo(BadgeList);

        public override string ToString()
        {
            return $"User(id: {ID}, name: {Name}) \n" + $"notifications: {ToStringNotificationList()} \n" + $"categoriesModerated: {ToStringCategoryList()} \n" + $"badges: {ToStringBadgeList()} \n";
        }
    }
}
