using UBB_SE_2024_Team_42.Domain.Reactions;
using UBB_SE_2024_Team_42.Utils;
using UBB_SE_2024_Team_42.Utils.functionbros;

namespace UBB_SE_2024_Team_42.Domain.Posts
{
    public class Post : IPost
    {

        public long PostID { get; }
        public long UserID { get; }
        public string Content { get; set; }
        public DateTime DatePosted { get; }
        public DateTime DateOfLastEdit { get; set; }
        public List<IReaction> Reactions { get; set; }
        public Post(long postingUserID, string content)
        {

            PostID = IDGenerator.RandomLong();
            UserID = postingUserID;
            Content = content;
            DatePosted = DateTime.Now;
            DateOfLastEdit = DateTime.Now;
            Reactions = [];
        }

        internal Post(long postID, long userID, string content, DateTime postTime, DateTime editTime, List<IReaction> reactions)
        {
            PostID = postID;
            UserID = userID;
            Content = content;
            DatePosted = postTime;
            DateOfLastEdit = editTime;
            Reactions = reactions;
        }
        public override string ToString()
        {
            return $"Post {{postID: {PostID}, userID: {UserID}, datePosted: {DatePosted}, dateOfLastEdit: {DateOfLastEdit})\n" +
                $"{Content}\n" +
                $"reactions: {CollectionStringifier<IReaction>.ApplyTo(Reactions)}}}";
        }
    }
}
