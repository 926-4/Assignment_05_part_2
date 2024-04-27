namespace UBB_SE_2024_Team_42.Domain.notification
{
    public interface INotification
    {
        long? BadgeID { get; }
        long NotificationId { get; }
        long? PostID { get; }
        string Text { get; set; }
    }
}