using UBB_SE_2024_Team_42.Service.EntityCreationServices;

namespace UBB_SE_2024_Team_42.Domain.Notification
{
    public class NotificationBuilder : AbstractEntityBuilder<INotification, Notification>
    {
        public override NotificationBuilder Begin()
        => (NotificationBuilder)base.Begin();
        public NotificationBuilder SetID(long id)
        {
            instance.ID = id;
            return this;
        }
        public NotificationBuilder SetUserID(long userID)
        {
            instance.UserID = userID;
            return this;
        }
        public NotificationBuilder SetText(string text)
        {
            instance.Text = text;
            return this;
        }
        public NotificationBuilder SetPostID(long postId)
        {
            instance.PostID = postId;
            return this;
        }
        public NotificationBuilder SetBadgeId(long badgeId)
        {
            instance.BadgeID = badgeId;
            return this;
        }
    }
}
