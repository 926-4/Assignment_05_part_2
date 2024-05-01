using UBB_SE_2024_Team_42.Domain.Tag;

namespace UBB_SE_2024_Team_42.Service.EntityCreationServices
{
    public class TagBuilder : AbstractEntityBuilder<ITag, Tag>
    {
        public override TagBuilder Begin()
            => (TagBuilder)base.Begin();

        public TagBuilder SetID(long id)
        {
            instance.Id = id;
            return this;
        }
        public TagBuilder SetName(string name)
        {
            instance.Name = name;
            return this;
        }
    }
}
