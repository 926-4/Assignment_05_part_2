namespace UBB_SE_2024_Team_42.Domain.Category
{
    internal class CategoryFactory
    {
        public Category Instance = new ();

        public CategoryFactory NewCategory()
        {
            Instance = new ();
            return this;
        }
        public CategoryFactory SetCategoryName(string categoryName)
        {
            Instance.CategoryName = categoryName;
            return this;
        }
        public Category Get()
        {
            Category returnValue = Instance;
            Instance = new ();
            return returnValue;
        }
    }
}
