namespace UBB_SE_2024_Team_42.Utils.Functionals
{
    internal class CollectionReducer<T1, T2>(Func<T1, T2> mapper, Func<T2, T2, T2> folder, T2 defaultResult)
    {
        private readonly Func<T1, T2> mapper = mapper;
        private readonly Func<T2, T2, T2> twoByTwoFolder = folder;
        private readonly T2 defaultResult = defaultResult;
        internal T2 MapThenFold(IEnumerable<T1> collection) =>
            collection
            .Select(mapper)
            .Aggregate(twoByTwoFolder)
            ?? defaultResult;
    }
}
