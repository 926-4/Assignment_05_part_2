using UBB_SE_2024_Team_42.Domain.Category;
using UBB_SE_2024_Team_42.Domain.Reactions;
using UBB_SE_2024_Team_42.Domain.Tag;
using UBB_SE_2024_Team_42.Utils.Functionals;

namespace UBB_SE_2024_Team_42.Domain.Posts
{
    public class Question : IQuestion
    {
        public string? Title { get; set; }
        public ICategory? Category { get; set; }
        public IPost Post { get; set; }

        public List<ITag> Tags { get; set; }

        public long PostID => Post.PostID;

        public long UserID => Post.UserID;

        public string Content => Post.Content;

        public DateTime DatePosted => Post.DatePosted;

        public DateTime DateOfLastEdit => Post.DateOfLastEdit;
        public List<IReaction> Reactions => Post.Reactions;

        long IPost.UserID
        {
            get => Post.UserID; set { Post.UserID = value; }
        }

        string IPost.Content
        {
            get => Post.Content; set { Post.Content = value; }
        }
        DateTime IPost.DatePosted
        {
            get => Post.DatePosted; set { Post.DatePosted = value; }
        }
        DateTime IPost.DateOfLastEdit
        {
            get => Post.DateOfLastEdit; set { Post.DateOfLastEdit = value; }
        }
        List<IReaction> IPost.Reactions
        {
            get => Post.Reactions; set { Post.Reactions = value; }
        }

        public Question(long userID, string content)
        {
            Post = new TextPost(userID, content);
            Title = string.Empty;
            Category = null;
            Tags = new ();
        }
        public Question(long userID, string content, ICategory category)
        {
            Post = new TextPost(userID, content);
            Title = string.Empty;
            Category = category;
            Tags = new ();
        }
        public Question(long userID, string content, ICategory category, string title)
        {
            Post = new TextPost(userID, content);
            Title = title;
            Category = category;
            Tags = new ();
        }

        public Question(string title, ICategory category, long userID, string content)
        {
            Post = new TextPost(userID, content);
            Title = title;
            Category = category;
            Tags = new ();
        }
        public Question(string title, ICategory category, List<ITag> tags, long userID, string content)
        {
            Post = new TextPost(userID, content);
            Title = title;
            Category = category;
            Tags = tags;
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
            Post = new TextPost(postID, userID, content, postTime, editTime, reactions);
            Title = title;
            Category = category;
            Tags = tags;
        }

        public Question()
        {
            Post = new TextPost();
            Tags = new ();
        }

        public override string ToString()
        {
            return $"Question(postID: {PostID}, userID: {UserID}, title:{Title} , category: {Category}) \n"
                + $"{Content} \n"
                + $"reactions: {CollectionStringifier<IReaction>.ApplyTo(Reactions)}\n"
                + $"tags: {CollectionStringifier<ITag>.ApplyTo(Tags)}";
        }
    }
}