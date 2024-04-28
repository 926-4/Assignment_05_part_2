using System.Drawing;
using UBB_SE_2024_Team_42.Domain.badge;
using UBB_SE_2024_Team_42.Domain.category;
using UBB_SE_2024_Team_42.Domain.notification;

namespace UBB_SE_2024_Team_42.Domain.user
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