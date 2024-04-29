namespace UBB_SE_2024_Team_42.Domain.Tag
{
    internal class TagFactory
    {
        private Tag instance = new ();

        public TagFactory NewTag()
        {
            instance = new ();
            return this;
        }

        public TagFactory SetName(string name)
        {
            instance.Name = name;
            return this;
        }

        public Tag Get()
        {
            Tag returnValue = instance;
            instance = new ();
            return returnValue;
        }
    }
}
