using UBB_SE_2024_Team_42.Domain.Tag;

namespace UBB_SE_2024_Team_42.Service.EntityCreationServices
{
    internal class TagFactory : AbstractEntityFactory<ITag, Tag>
    {
        public override TagFactory Begin()
            => (TagFactory)base.Begin();

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
    }
}
