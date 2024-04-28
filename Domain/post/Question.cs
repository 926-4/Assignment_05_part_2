using UBB_SE_2024_Team_42.Domain.Category;
using UBB_SE_2024_Team_42.Domain.Reactions;
using UBB_SE_2024_Team_42.Domain.Tag;
using UBB_SE_2024_Team_42.Utils.Functionals;

namespace UBB_SE_2024_Team_42.Domain.Posts
{
    public class Question : IQuestion
    {
        public string? Title { get; set; }
        public ICategory? Category { get; }
        private readonly IPost ipost;

        public List<ITag> Tags { get; set; }

        public long PostID => ipost.PostID;

        public long UserID => ipost.UserID;

        public string Content => ipost.Content;

        public DateTime DatePosted => ipost.DatePosted;

        public DateTime DateOfLastEdit => ipost.DateOfLastEdit;
        public List<IReaction> Reactions => ipost.Reactions;

        string IPost.Content
        {
            get => ipost.Content; set { ipost.Content = value; }
        }
        DateTime IPost.DateOfLastEdit
        {
            get => ipost.DateOfLastEdit; set { ipost.DateOfLastEdit = value; }
        }
        List<IReaction> IPost.Reactions
        {
            get => ipost.Reactions; set { ipost.Reactions = value; }
        }
        public Question(long userID, string content)
        {
            ipost = new TextPost(userID, content);
            Title = string.Empty;
            Category = null;
            Tags = new ();
        }
        public Question(long userID, string content, ICategory category)
        {
            ipost = new TextPost(userID, content);
            Title = string.Empty;
            Category = category;
            Tags = new ();
        }
        public Question(long userID, string content, ICategory category, string title)
        {
            ipost = new TextPost(userID, content);
            Title = title;
            Category = category;
            Tags = new ();
        }

        public Question(string title, ICategory category, long userID, string content)
        {
            ipost = new TextPost(userID, content);
            Title = title;
            Category = category;
            Tags = new ();
        }
        public Question(string title, ICategory category, List<ITag> tags, long userID, string content)
        {
            ipost = new TextPost(userID, content);
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
            ipost = new TextPost(postID, userID, content, postTime, editTime, reactions);
            Title = title;
            Category = category;
            Tags = tags;
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