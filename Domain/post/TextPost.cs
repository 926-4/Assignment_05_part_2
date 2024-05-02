using UBB_SE_2024_Team_42.Domain.Post.Interfaces;
using UBB_SE_2024_Team_42.Domain.Reactions;
using UBB_SE_2024_Team_42.Utils;
using UBB_SE_2024_Team_42.Utils.Functionals;

namespace UBB_SE_2024_Team_42.Domain.Posts
{
    public class TextPost : IPost
    {
        public long ID { get; set; }
        public long UserID { get; set; }
        public string Content { get; set; }
        public DateTime DatePosted { get; set; }
        public DateTime DateOfLastEdit { get; set; }
        public List<IReaction> Reactions { get; set; }
        public TextPost(long postingUserID, string content)
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
        public TextPost()
        {
            ID = IDGenerator.Default();
            Content = "None";
#pragma warning disable IDE0028 // Simplify collection initialization
            Reactions = new ();
#pragma warning restore IDE0028 // Simplify collection initialization
        }

        public override string ToString()
        {
            return $"TextPost {{postID: {ID}, userID: {UserID}, datePosted: {DatePosted}, dateOfLastEdit: {DateOfLastEdit})" +
                $"{Content}" +
                $"reactions: {CollectionStringifier<IReaction>.ApplyTo(Reactions)}}}";
        }
    }
}
