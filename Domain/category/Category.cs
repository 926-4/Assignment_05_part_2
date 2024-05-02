using UBB_SE_2024_Team_42.Utils;

namespace UBB_SE_2024_Team_42.Domain.Category
{
    public class Category : ICategory
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public Category()
        {
            ID = IDGenerator.Default();
            Name = "Unnamed category";
        }

        public override string ToString()
        {
            return $"Category(categoryID: {ID}, categoryName: {Name})";
        }
    }
}
