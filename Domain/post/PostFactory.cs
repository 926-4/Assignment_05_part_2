using UBB_SE_2024_Team_42.Domain.Reactions;

namespace UBB_SE_2024_Team_42.Domain.Posts
{
    internal partial class PostFactory
    {
        internal static IPost CreateNewPost(PostType option, long userID, string content)
        {
            return option switch
            {
                PostType.COMMENT => new Comment(userID, content),
                PostType.TEXT_POST => new TextPost(userID, content),
                PostType.QUESTION => new Question(userID, content),
                PostType.ANSWER => new Answer(userID, content),
                _ => throw new NotImplementedException()
            };
        }
        internal static IPost ConstructExistingPost(PostType option, long postID, long userID, string content, DateTime postDate, DateTime editDate, List<IReaction> reactions)
        {
            return option switch
            {
                PostType.COMMENT => new Comment(postID, userID, content, postDate, editDate, reactions),
                PostType.TEXT_POST => new TextPost(postID, userID, content, postDate, editDate, reactions),
                PostType.ANSWER => new Answer(postID, userID, content, postDate, editDate, reactions),
                _ => throw new NotImplementedException()
            };
        }
    }
}
