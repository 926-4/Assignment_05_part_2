using UBB_SE_2024_Team_42.Domain.Reactions;
using UBB_SE_2024_Team_42.Utils;
using UBB_SE_2024_Team_42.Utils.Functionals;

namespace UBB_SE_2024_Team_42.Domain.Posts
{
    public class Answer : IPost
    {
        public long ID { get; set; }

        public long UserID { get; set; }

        public string Content { get; set; }

        public DateTime DatePosted { get; set; }

        public DateTime DateOfLastEdit { get; set; }
        public List<IReaction> Reactions { get; set; }

        public Answer(long userID, string content)
        {
            ID = IDGenerator.RandomLong();
            UserID = userID;
            Content = content;
            DatePosted = DateTime.Now;
            DateOfLastEdit = DateTime.Now;
#pragma warning disable IDE0028 // Simplify collection initialization
            Reactions = new ();
#pragma warning restore IDE0028 // Simplify collection initialization
        }

        internal Answer(long id, long userID, string content, DateTime postTime, DateTime editTime, List<IReaction> reactions)
        {
            ID = id;
            UserID = userID;
            Content = content;
            DatePosted = postTime;
            DateOfLastEdit = editTime;
            Reactions = reactions;
        }

        public Answer()
        {
            ID = IDGenerator.RandomLong();
            Content = "None";
#pragma warning disable IDE0028 // Simplify collection initialization
            Reactions = new ();
#pragma warning restore IDE0028 // Simplify collection initialization
        }

        public override string ToString()
        {
            return $"Answer {{id: {ID}, userID: {UserID}, datePosted: {DatePosted}, dateOfLastEdit: {DateOfLastEdit}) \n"
                + $"{Content} \n"
                + $"reactions: {CollectionStringifier<IReaction>.ApplyTo(Reactions)}}} \n";
        }
    }
}
