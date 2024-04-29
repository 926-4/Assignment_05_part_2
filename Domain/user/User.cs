﻿using System.Drawing;
using UBB_SE_2024_Team_42.Domain.Badge;
using UBB_SE_2024_Team_42.Domain.Category;
using UBB_SE_2024_Team_42.Domain.Notification;
using UBB_SE_2024_Team_42.Utils;
using UBB_SE_2024_Team_42.Utils.Functionals;

namespace UBB_SE_2024_Team_42.Domain.User
{
    public class User : IUser
    {
        public long ID { get; }
        public string Name { get; set; }
        public List<INotification> NotificationList { get; set; }
        public List<ICategory> CategoriesModeratedList { get; set; }
        public List<IBadge> BadgeList { get; set; }
        public Image? ProfilePicture { get; set; }
        public User()
        {
            ID = IDGenerator.RandomLong();
            Name = string.Empty;
#pragma warning disable IDE0028 // Simplify collection initialization
            NotificationList = new ();
#pragma warning restore IDE0028 // Simplify collection initialization
#pragma warning disable IDE0028 // Simplify collection initialization
            CategoriesModeratedList = new ();
#pragma warning restore IDE0028 // Simplify collection initialization
#pragma warning disable IDE0028 // Simplify collection initialization
            BadgeList = new ();
#pragma warning restore IDE0028 // Simplify collection initialization
        }
        public User(string username)
        {
            ID = IDGenerator.RandomLong();
            Name = username;
#pragma warning disable IDE0028 // Simplify collection initialization
            NotificationList = new ();
#pragma warning restore IDE0028 // Simplify collection initialization
#pragma warning disable IDE0028 // Simplify collection initialization
            CategoriesModeratedList = new ();
#pragma warning restore IDE0028 // Simplify collection initialization
#pragma warning disable IDE0028 // Simplify collection initialization
            BadgeList = new ();
#pragma warning restore IDE0028 // Simplify collection initialization
        }
        internal User(long id, string name, List<INotification> notificationList, List<ICategory> categoriesModeratedList, List<IBadge> badgeList)
        {
            ID = id;
            Name = name;
            NotificationList = notificationList;
            CategoriesModeratedList = categoriesModeratedList;
            BadgeList = badgeList;
        }
        private string ToStringNotificationList() => CollectionStringifier<INotification>.ApplyTo(NotificationList);
        private string ToStringCategoryList() => CollectionStringifier<ICategory>.ApplyTo(CategoriesModeratedList);
        private string ToStringBadgeList() => CollectionStringifier<IBadge>.ApplyTo(BadgeList);

        public override string ToString()
        {
            return $"User(id: {ID}, name: {Name}) \n" + $"notifications: {ToStringNotificationList()} \n" + $"categoriesModerated: {ToStringCategoryList()} \n" + $"badges: {ToStringBadgeList()} \n";
        }
    }
}
