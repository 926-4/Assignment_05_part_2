using System.Drawing;
using UBB_SE_2024_Team_42.Domain.Badge;
using UBB_SE_2024_Team_42.Domain.Category;
using UBB_SE_2024_Team_42.Domain.Notification;

namespace UBB_SE_2024_Team_42.Domain.User
{
    public interface IUser
    {
        List<IBadge> BadgeList { get; set; }
        List<ICategory> CategoriesModeratedList { get; set; }
        List<INotification> NotificationList { get; set; }
        Image? ProfilePicture { get; set; }
        long UserID { get; }
        string UserName { get; set; }
    }
}