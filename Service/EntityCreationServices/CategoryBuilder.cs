using UBB_SE_2024_Team_42.Domain.Category;

namespace UBB_SE_2024_Team_42.Service.EntityCreationServices
{
    public class CategoryBuilder : AbstractEntityBuilder<ICategory, Category>
    {
        public override CategoryBuilder Begin()
            => (CategoryBuilder)base.Begin();

        public CategoryBuilder SetID(long categoryID)
        {
            instance.ID = categoryID;
            return this;
        }
        public CategoryBuilder SetName(string categoryName)
        {
            instance.Name = categoryName;
            return this;
        }
    }
}
