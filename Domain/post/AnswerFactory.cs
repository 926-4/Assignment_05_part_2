using UBB_SE_2024_Team_42.Domain.Reactions;

namespace UBB_SE_2024_Team_42.Domain.Posts
{
    internal class AnswerFactory
    {
        private Answer instance = new ();
        public AnswerFactory NewAnswer()
        {
            instance = new ();
            return this;
        }
        public AnswerFactory SetId(long id)
        {
            instance.ID = id;
            return this;
        }
        public AnswerFactory SetUserId(long userId)
        {
            instance.UserID = userId;
            return this;
        }
        public AnswerFactory SetContent(string content)
        {
            instance.Content = content;
            return this;
        }
        public AnswerFactory SetDatePosted(DateTime datePosted)
        {
            instance.DatePosted = datePosted;
            return this;
        }
        public AnswerFactory SetDateOfLastEdit(DateTime dateOfLastEdit)
        {
            instance.DateOfLastEdit = dateOfLastEdit;
            return this;
        }
        public AnswerFactory SetReactions(List<IReaction> reactions)
        {
            instance.Reactions = reactions;
            return this;
        }
        public Answer Get()
        {
            Answer answer = instance;
            instance = new ();
            return answer;
        }
    }
}
