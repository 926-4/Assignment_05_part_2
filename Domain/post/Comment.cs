using UBB_SE_2024_Team_42.Domain.Reactions;
using UBB_SE_2024_Team_42.Utils;
using UBB_SE_2024_Team_42.Utils.Functionals;

namespace UBB_SE_2024_Team_42.Domain.Post
{
    public class Comment : IComment
    {
        public long ID { get; set; }

        public long UserID { get; set; }

        public string Content { get; set; }

        public DateTime DatePosted { get; set; }

        public DateTime DateOfLastEdit { get; set; }
        public List<IReaction> Reactions { get; set; }
        internal Comment()
        {
            ID = IDGenerator.RandomLong();
            UserID = IDGenerator.RandomLong();
            Content = string.Empty;
            DatePosted = DateTime.Now;
            DateOfLastEdit = DateTime.Now;
            Reactions = new ();
        }
        public Comment(long postingUserID, string content)
        {
            ID = IDGenerator.RandomLong();
            UserID = postingUserID;
            Content = content;
            DatePosted = DateTime.Now;
            DateOfLastEdit = DateTime.Now;
#pragma warning disable IDE0028 // Simplify collection initialization
            Reactions = new ();
#pragma warning restore IDE0028 // Simplify collection initialization
        }

        internal Comment(long postID, long userID, string content, DateTime postTime, DateTime editTime, List<IReaction> reactions)
        {
            ID = postID;
            UserID = userID;
            Content = content;
            DatePosted = postTime;
            DateOfLastEdit = editTime;
            Reactions = reactions;
        }

        public override string ToString()
        {
            return $"Comment {{postID: {ID}, userID: {UserID}, datePosted: {DatePosted}, dateOfLastEdit: {DateOfLastEdit}) \n"
                + $"{Content} \n"
                + $"reactions: {CollectionStringifier<IReaction>.ApplyTo(Reactions)}}} \n";
        }
    }
}
