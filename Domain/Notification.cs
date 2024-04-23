using System.Dynamic;

namespace UBB_SE_2024_Team_42.Domain
{
    public class Notification
    {
        public long NotificationId { get; }
        public string Text { get; set; }
        public long PostID { get; }
        public long BadgeID {  get; } 

        public Notification(long newNotficationID, long newPostID, long newBadgeID)
        {
            this.NotificationId = newNotficationID;
            this.PostID = newPostID;
            this.BadgeID = BadgeID;
            if (newPostID != null)
            {
                this.Text = "Someone replied to one of your posts";
            }
            else if (newBadgeID != null)
            {
                this.Text = "You have a new badge";
            }
        }

        public override string ToString()
        {
            return $"Notification(notificationID: {NotificationId}, postID: {PostID}, badgeID: {BadgeID}) \n" + $"notificationText: {Text} \n";
        }

    }
}
