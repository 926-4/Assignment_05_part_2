using UBB_SE_2024_Team_42.Domain.Category;

namespace UBB_SE_2024_Team_42.Service.EntityCreationServices
{
    internal class CategoryFactory
    {
        public Category Instance = new();

        public CategoryFactory NewCategory()
        {
            Instance = new();
            return this;
        }
        public CategoryFactory SetID(long categoryID)
        {
            Instance.ID = categoryID;
            return this;
        }
        public CategoryFactory SetName(string categoryName)
        {
            Instance.Name = categoryName;
            return this;
        }
        public Category Get()
        {
            Category returnValue = Instance;
            Instance = new();
            return returnValue;
        }
    }
}
