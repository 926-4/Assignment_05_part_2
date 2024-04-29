namespace UBB_SE_2024_Team_42.Domain.Notification
{
    internal class NotificationFactory
    {
        private Notification instance = new ();

        public NotificationFactory NewNotification()
        {
            instance = new ();
            return this;
        }
        public NotificationFactory SetText(string text)
        {
            instance.Text = text;
            return this;
        }
        public NotificationFactory SetPostId(long postId)
        {
            instance.PostID = postId;
            return this;
        }
        public NotificationFactory SetBadgeId(long badgeId)
        {
            instance.BadgeID = badgeId;
            return this;
        }
        public Notification Get()
        {
            Notification returnValue = instance;
            instance = new ();
            return returnValue;
        }
    }
}
