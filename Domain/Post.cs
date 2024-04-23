namespace UBB_SE_2024_Team_42.Domain
{
    public class Post(long newPostID, long newUserID, string newContent, string newPostType, List<Vote> newVoteList, DateTime newDatePosted, DateTime newDateOfLastEdit)
    {
        public const string QUESTION_TYPE = "question";
        public const string COMMENT_TYPE = "comment";
        public const string ANSWER_TYPE = "answer";
        public long PostID { get; } = newPostID;
        public long UserID { get; } = newUserID;
        public string Content { get; set; } = newContent;
        public DateTime datePosted { get; } = newDatePosted;
        public DateTime dateOfLastEdit { get; set; } = newDateOfLastEdit;
        public string PostType { get; } = newPostType;
        public List<Vote> VoteList { get; set; } = newVoteList;

        protected string ToStringVoteList()
        {
            string result = "";
            foreach (Vote elem in VoteList)
            {
                result += elem;
            }
            return result;
        }


        public override string ToString()
        {
            return $"{PostType}(postID: {PostID}, userID: {UserID}, datePosted: {datePosted}, dateOfLastEdit: {dateOfLastEdit}) \n" + $"{Content} \n" + $"votes: {ToStringVoteList()} \n";
        }

        public void AddVote(Vote vote)
        {
            VoteList.Add(vote);
        }

        public int getScore()
        {
            int score = 0;
            foreach (Vote vote in VoteList)
            {
                score += vote.VoteValue;
            }
            return score;
        }

    }
}
