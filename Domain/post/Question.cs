using UBB_SE_2024_Team_42.Domain.Category;
using UBB_SE_2024_Team_42.Domain.Post.Interfaces;
using UBB_SE_2024_Team_42.Domain.Reactions;
using UBB_SE_2024_Team_42.Domain.Tag;
using UBB_SE_2024_Team_42.Utils.Functionals;

namespace UBB_SE_2024_Team_42.Domain.Posts
{
    public class Question : IQuestion
    {
        public string? Title { get; set; }
        public ICategory? Category { get; set; }
        private readonly IPost post;
        public List<ITag> Tags { get; set; }
        public long ID
        {
            get => post.ID; set { post.ID = value; }
        }
        public long UserID
        {
            get => post.UserID; set { post.UserID = value; }
        }

        public string Content
        {
            get => post.Content; set { post.Content = value; }
        }
        public DateTime DatePosted
        {
            get => post.DatePosted; set { post.DatePosted = value; }
        }
        public DateTime DateOfLastEdit
        {
            get => post.DateOfLastEdit; set { post.DateOfLastEdit = value; }
        }
        public List<IReaction> Reactions
        {
            get => post.Reactions; set { post.Reactions = value; }
        }

        public Question(long userID, string content)
        {
            post = new TextPost(userID, content);
            Title = string.Empty;
            Category = null;
#pragma warning disable IDE0028 // Simplify collection initialization
            Tags = new ();
#pragma warning restore IDE0028 // Simplify collection initialization
        }
        public Question(long userID, string content, ICategory category, string title)
        {
            post = new TextPost(userID, content);
            Title = title;
            Category = category;
#pragma warning disable IDE0028 // Simplify collection initialization
            Tags = new ();
#pragma warning restore IDE0028 // Simplify collection initialization
        }

        public Question(string title, ICategory category, long userID, string content)
        {
            post = new TextPost(userID, content);
            Title = title;
            Category = category;
#pragma warning disable IDE0028 // Simplify collection initialization
            Tags = new ();
#pragma warning restore IDE0028 // Simplify collection initialization
        }
        public Question(long postID,
                        string title,
                        ICategory category,
                        List<ITag> tags,
                        long userID,
                        string content,
                        DateTime postTime,
                        DateTime editTime,
                        List<IReaction> reactions)
        {
            post = new TextPost(postID, userID, content, postTime, editTime, reactions);
            Title = title;
            Category = category;
            Tags = tags;
        }

        public Question()
        {
            post = new TextPost();
#pragma warning disable IDE0028 // Simplify collection initialization
            Tags = new ();
#pragma warning restore IDE0028 // Simplify collection initialization
        }

        public override string ToString()
        {
            return $"Question(postID: {ID}, userID: {UserID}, title:{Title} , category: {Category}) \n"
                + $"{Content} \n"
                + $"reactions: {CollectionStringifier<IReaction>.ApplyTo(Reactions)}\n"
                + $"tags: {CollectionStringifier<ITag>.ApplyTo(Tags)}";
        }
    }
}