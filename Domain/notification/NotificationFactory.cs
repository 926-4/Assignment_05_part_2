namespace UBB_SE_2024_Team_42.Domain.Notification
{
    internal class NotificationFactory
    {
        public Notification Instance = new ();

        public NotificationFactory NewNotification()
        {
            Instance = new ();
            return this;
        }
        public NotificationFactory SetText(string text)
        {
            Instance.Text = text;
            return this;
        }
        public NotificationFactory SetPostId(long postId)
        {
            Instance.PostID = postId;
            return this;
        }
        public NotificationFactory SetBadgeId(long badgeId)
        {
            Instance.BadgeID = badgeId;
            return this;
        }
        public Notification Get()
        {
            Notification returnValue = Instance;
            Instance = new ();
            return returnValue;
        }
    }
}
