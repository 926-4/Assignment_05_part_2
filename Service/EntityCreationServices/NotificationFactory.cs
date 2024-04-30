using UBB_SE_2024_Team_42.Service.EntityCreationServices;

namespace UBB_SE_2024_Team_42.Domain.Notification
{
    internal class NotificationFactory : AbstractEntityFactory<INotification, Notification>
    {
        public override NotificationFactory Begin()
        => (NotificationFactory)base.Begin();
        public NotificationFactory SetID(long id)
        {
            instance.ID = id;
            return this;
        }
        public NotificationFactory SetText(string text)
        {
            instance.Text = text;
            return this;
        }
        public NotificationFactory SetPostID(long postId)
        {
            instance.PostID = postId;
            return this;
        }
        public NotificationFactory SetBadgeId(long badgeId)
        {
            instance.BadgeID = badgeId;
            return this;
        }
    }
}
