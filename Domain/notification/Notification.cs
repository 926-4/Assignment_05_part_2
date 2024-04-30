using UBB_SE_2024_Team_42.Utils;

namespace UBB_SE_2024_Team_42.Domain.Notification
{
    public class Notification : INotification
    {
        public long ID { get; set; }
        public string Text { get; set; }
        public long UserID { get; set; }
        public long? PostID { get; set; }
        public long? BadgeID { get; set; }
        public Notification()
        {
            ID = IDGenerator.Default();
            UserID = IDGenerator.Default();
            Text = "None";
        }

        public override string ToString()
        {
            return $"Notification{{notificationID: {ID}, postID: {PostID}, badgeID: {BadgeID}\n"
                + $"notificationText: {Text}}} \n";
        }
    }
}
