using UBB_SE_2024_Team_42.Domain.Category;

namespace UBB_SE_2024_Team_42.Service.EntityCreationServices
{
    internal class CategoryFactory : AbstractEntityFactory<ICategory, Category>
    {
        public override CategoryFactory Begin()
            => (CategoryFactory)base.Begin();

        public CategoryFactory SetID(long categoryID)
        {
            instance.ID = categoryID;
            return this;
        }
        public CategoryFactory SetName(string categoryName)
        {
            instance.Name = categoryName;
            return this;
        }
    }
}
