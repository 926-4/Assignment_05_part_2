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
        public List<INotification> NotificationList { get; set; }
        public List<ICategory> CategoriesModeratedList { get; set; }
        public List<IBadge> BadgeList { get; set; }
        public Image? ProfilePicture { get; set; }
        public User() 
        {
            UserID = IDGenerator.RandomLong();
            UserName = "";
            NotificationList = [];
            CategoriesModeratedList = [];
            BadgeList = [];
        }
        public User(string username)
        {
            UserID = IDGenerator.RandomLong();
            UserName = username;
            NotificationList = [];
            CategoriesModeratedList = [];
            BadgeList = [];
        }
        internal User(long userID, string userName, List<INotification> notificationList, List<ICategory> categoriesModeratedList, List<IBadge> badgeList)
        {
            UserID = userID;
            UserName = userName;
            NotificationList = notificationList;
            CategoriesModeratedList = categoriesModeratedList;
            BadgeList = badgeList;
        }
        private string ToStringNotificationList() => CollectionStringifier<INotification>.ApplyTo(NotificationList);
        private string ToStringCategoryList() => CollectionStringifier<ICategory>.ApplyTo(CategoriesModeratedList);
        private string ToStringBadgeList() => CollectionStringifier<IBadge>.ApplyTo(BadgeList);

        public override string ToString()
        {
            return $"User(userID: {UserID}, userName: {UserName}) \n" + $"notifications: {ToStringNotificationList()} \n" + $"categoriesModerated: {ToStringCategoryList()} \n" + $"badges: {ToStringBadgeList()} \n";
        }
    }
}
