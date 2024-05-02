namespace UBB_SE_2024_Team_42.Utils.Functionals
{
    internal class CollectionSummerFactory<InputType>
    {
        static public CollectionSummer<InputType> GetFromMapping(Func<InputType, int> mapping)
        {
            return new CollectionSummer<InputType>(mapping);
        }
    }
}
