using UBB_SE_2024_Team_42.Domain.category;
using UBB_SE_2024_Team_42.Domain.Reactions;
using UBB_SE_2024_Team_42.Domain.tag;
using UBB_SE_2024_Team_42.Utils.functionbros;

namespace UBB_SE_2024_Team_42.Domain.Posts
{
    public class Question : Post
    {
        public string? Title { get; set; }
        public ICategory? Category { get; set; }

        public List<ITag> Tags { get; set; }
        public Question(String title, ICategory category, long userID, string content) : base(userID, content)
        {
            Title = title;
            Category = category;
            Tags = [];
        }
        public Question(String title, ICategory category, List<ITag> tags, long userID, string content) : base(userID, content)
        {
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
            : base(postID, userID, content, postTime, editTime, reactions)
        {
            Title = title;
            Category = category;
            Tags = tags;
        }

        public Question()
        {
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
