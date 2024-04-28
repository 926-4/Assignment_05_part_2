using System.IO.Packaging;
using System.Windows.Media.TextFormatting;
using UBB_SE_2024_Team_42.Domain.category;
using UBB_SE_2024_Team_42.Domain.Reactions;
using UBB_SE_2024_Team_42.Domain.tag;
using UBB_SE_2024_Team_42.Utils.functionbros;

namespace UBB_SE_2024_Team_42.Domain.Posts
{
    public class Question : IQuestion
    {
        public string? Title { get; set; }
        public ICategory? Category { get; }
        private readonly IPost Ipost;

        public List<ITag> Tags { get; set; }

        public long PostID => Ipost.PostID;

        public long UserID => Ipost.UserID;

        public string Content => Ipost.Content;

        public DateTime DatePosted => Ipost.DatePosted;

        public DateTime DateOfLastEdit => Ipost.DateOfLastEdit;
        public List<IReaction> Reactions => Ipost.Reactions;

        string IPost.Content { get => Ipost.Content; set { Ipost.Content = value; } }
        DateTime IPost.DateOfLastEdit { get => Ipost.DateOfLastEdit; set { Ipost.DateOfLastEdit = value; } }
        List<IReaction> IPost.Reactions { get => Ipost.Reactions; set { Ipost.Reactions = value; } }
        public Question(long userID, string content)
        {
            Ipost = new TextPost(userID, content);
            Title = "";
            Category = null;
            Tags = [];
        }
        public Question(long userID, string content, ICategory category)
        {
            Ipost = new TextPost(userID, content);
            Title = "";
            Category = category;
            Tags = [];
        }
        public Question(long userID, string content, ICategory category, string title)
        {
            Ipost = new TextPost(userID, content);
            Title = title;
            Category = category;
            Tags = [];
        }

        public Question(string title, ICategory category, long userID, string content)
        {
            Ipost = new TextPost(userID, content);
            Title = title;
            Category = category;
            Tags = [];
        }
        public Question(String title, ICategory category, List<ITag> tags, long userID, string content)
        {
            Ipost = new TextPost(userID, content);
            Title = title;
            Category = category;
            Tags = tags;
        }
        public Question(long postID,
                        String title,
                        ICategory category,
                        List<ITag> tags,
                        long userID,
                        string content,
                        DateTime postTime,
                        DateTime editTime,
                        List<IReaction> reactions)
        {
            Ipost = new TextPost(postID, userID, content, postTime, editTime, reactions);
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
