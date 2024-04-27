using System.Drawing;
using UBB_SE_2024_Team_42.Domain.badge;
using UBB_SE_2024_Team_42.Domain.category;
using UBB_SE_2024_Team_42.Domain.notification;

namespace UBB_SE_2024_Team_42.Domain.user
{
    public interface IUser
    {
        List<Badge> BadgeList { get; set; }
        List<Category> CategoriesModeratedList { get; set; }
        List<Notification> NotificationList { get; set; }
        Image? ProfilePicture { get; set; }
        long UserID { get; }
        string UserName { get; set; }
    }
}