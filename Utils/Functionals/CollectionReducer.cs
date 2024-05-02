namespace UBB_SE_2024_Team_42.Utils.Functionals
{
    internal class CollectionReducer<InputType, OutputType>(Func<InputType, OutputType> mapper, Func<OutputType, OutputType, OutputType> folder, OutputType defaultResult)
    {
        private readonly Func<InputType, OutputType> mapper = mapper;
        private readonly Func<OutputType, OutputType, OutputType> twoByTwoFolder = folder;
        private readonly OutputType defaultResult = defaultResult;
        internal OutputType MapThenFold(IEnumerable<InputType> collection) =>
            collection
            .Select(mapper)
            .Aggregate(twoByTwoFolder)
            ?? defaultResult;
    }
}
