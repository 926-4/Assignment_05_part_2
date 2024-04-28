using UBB_SE_2024_Team_42.Domain.Reactions;

namespace UBB_SE_2024_Team_42.Domain.Posts
{
    internal class AnswerFactory
    {
        public Answer Instance = new ();
        public AnswerFactory NewAnswer()
        {
            Instance = new ();
            return this;
        }
        public AnswerFactory SetUserId(long userId)
        {
            Instance.UserID = userId;
            return this;
        }
        public AnswerFactory SetContent(string content)
        {
            Instance.Content = content;
            return this;
        }
        public AnswerFactory SetDatePosted(DateTime datePosted)
        {
            Instance.DatePosted = datePosted;
            return this;
        }
        public AnswerFactory SetDateOfLastEdit(DateTime dateOfLastEdit)
        {
            Instance.DateOfLastEdit = dateOfLastEdit;
            return this;
        }
        public AnswerFactory SetReactions(List<IReaction> reactions)
        {
            Instance.Reactions = reactions;
            return this;
        }
        public Answer Get()
        {
            Answer answer = Instance;
            Instance = new ();
            return answer;
        }
    }
}
