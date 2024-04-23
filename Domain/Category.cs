namespace UBB_SE_2024_Team_42.Domain
{
    public class Category(long newCategoryID, string newCategoryName)
    {
        public long CategoryID { get; } = newCategoryID;
        public string CategoryName { get; } = newCategoryName;

        public override string ToString()
        {
            return $"Category(categoryID: {CategoryID}, categoryName: {CategoryName}) \n";
        }

        // get CategoryModerators(){}

    }
}
