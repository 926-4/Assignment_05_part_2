namespace UBB_SE_2024_Team_42.Utils.Functionals
{
    internal class CollectionSummerFactory<T>
    {
        static public CollectionSummer<T> GetFromMapping(Func<T, int> mapping)
        {
            return new CollectionSummer<T>(mapping);
        }
    }
}
