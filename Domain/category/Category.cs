using UBB_SE_2024_Team_42.Utils;

namespace UBB_SE_2024_Team_42.Domain.Category
{
    public class Category : ICategory
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public Category()
        {
            ID = IDGenerator.RandomLong();
            Name = "Unnamed category";
        }
        internal Category(long id, string name)
        {
            ID = id;
            Name = name;
        }

        public override string ToString()
        {
            return $"Category(categoryID: {ID}, categoryName: {Name}) \n";
        }
    }
}
