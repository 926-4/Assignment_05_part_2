using UBB_SE_2024_Team_42.Utils;

namespace UBB_SE_2024_Team_42.Domain.Category
{
    public class Category : ICategory
    {
        public long CategoryID { get; }
        public string CategoryName { get; set; }
        public Category()
        {
            CategoryID = IDGenerator.RandomLong();
            CategoryName = "Unnamed category";
        }
        internal Category(long newCategoryID, string newCategoryName)
        {
            CategoryID = newCategoryID;
            CategoryName = newCategoryName;
        }

        public override string ToString()
        {
            return $"Category(categoryID: {CategoryID}, categoryName: {CategoryName}) \n";
        }
    }
}
