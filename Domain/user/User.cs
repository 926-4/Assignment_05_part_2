using System.Drawing;
using UBB_SE_2024_Team_42.Domain.badge;
using UBB_SE_2024_Team_42.Domain.category;
using UBB_SE_2024_Team_42.Domain.notification;
using UBB_SE_2024_Team_42.Utils;
using UBB_SE_2024_Team_42.Utils.functionbros;

namespace UBB_SE_2024_Team_42.Domain.user
{
    public class User : IUser
    {
        public long UserID { get; }
        public string UserName { get; set; }
        public List<Notification> NotificationList { get; set; }
        public List<Category> CategoriesModeratedList { get; set; }
        public List<Badge> BadgeList { get; set; }
        public Image? ProfilePicture { get; set; }
        public User(string username)
        {
            UserID = IDGenerator.RandomLong();
            UserName = username;
            NotificationList = [];
            CategoriesModeratedList = [];
            BadgeList = [];
        }
        internal User(long userID, string userName, List<Notification> notificationList, List<Category> categoriesModeratedList, List<Badge> badgeList)
        {
            UserID = userID;
            UserName = userName;
            NotificationList = notificationList;
            CategoriesModeratedList = categoriesModeratedList;
            BadgeList = badgeList;
        }
        private string ToStringNotificationList() => CollectionStringifier<Notification>.ApplyTo(NotificationList);
        private string ToStringCategoryList() => CollectionStringifier<Category>.ApplyTo(CategoriesModeratedList);
        private string ToStringBadgeList() => CollectionStringifier<Badge>.ApplyTo(BadgeList);

        public override string ToString()
        {
            return $"User(userID: {UserID}, userName: {UserName}) \n" + $"notifications: {ToStringNotificationList()} \n" + $"categoriesModerated: {ToStringCategoryList()} \n" + $"badges: {ToStringBadgeList()} \n";
        }
    }
}
