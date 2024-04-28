namespace UBB_SE_2024_Team_42.Domain.Posts
{
    internal class QuestionFactory
    {
        public Question Instance = new ();
        public QuestionFactory NewQuestion()
        {
            Instance = new ();
            return this;
        }
        public QuestionFactory NewUserId(long userId)
        {
            return this;
        }
    }
}
