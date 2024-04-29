using UBB_SE_2024_Team_42.Domain.Tag;

namespace UBB_SE_2024_Team_42.Service.EntityCreationServices
{
    internal class TagFactory
    {
        private ITag instance = new Tag();

        public TagFactory NewTag()
        {
            instance = new Tag();
            return this;
        }

        public TagFactory SetID(long id)
        {
            instance.Id = id;
            return this;
        }
        public TagFactory SetName(string name)
        {
            instance.Name = name;
            return this;
        }

        public ITag Get()
        {
            ITag returnValue = instance;
            instance = new Tag();
            return returnValue;
        }
    }
}
