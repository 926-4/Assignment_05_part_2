using UBB_SE_2024_Team_42.Utils;

namespace UBB_SE_2024_Team_42.Domain.Notification
{
    public class Notification : INotification
    {
        public long ID { get; }
        public string Text { get; set; }
        public long? PostID { get; set; }
        public long? BadgeID { get; set; }
        internal Notification(NotificationOption option, long referenceID)
        {
            ID = IDGenerator.RandomLong();
            switch (option)
            {
                case NotificationOption.NONE:
                    Text = "?";
                    break;
                case NotificationOption.REPLY:
                    Text = "Someone replied to one of your posts";
                    PostID = referenceID;
                    break;
                case NotificationOption.BADGE:
                    Text = "You have a new badge";
                    BadgeID = referenceID;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        internal Notification(long newNotficationID, long? newPostID, long? newBadgeID)
        {
            ID = newNotficationID;
            PostID = newPostID;
            BadgeID = newBadgeID;
            Text = newPostID != null
                ? "Someone replied to one of your posts"
                : newBadgeID != null
                    ? "You have a new badge"
                    : "?";
        }

        public Notification()
        {
            ID = IDGenerator.RandomLong();
            Text = "None";
        }

        public override string ToString()
        {
            return $"Notification{{notificationID: {ID}, postID: {PostID}, badgeID: {BadgeID}\n"
                + $"notificationText: {Text}}} \n";
        }
    }
}
