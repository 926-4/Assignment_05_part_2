namespace UBB_SE_2024_Team_42.Utils.functionbros
{
    internal class CollectionReducer<T1, T2>(Func<T1, T2> transformer, Func<T2, T2, T2> aggregator, T2 defaultResult)
    {
        private readonly Func<T1, T2> Transformer = transformer;
        private readonly Func<T2, T2, T2> Aggregator = aggregator;
        private readonly T2 DefaultResult = defaultResult;
        internal T2 TransformAndReduce(IEnumerable<T1> collection) =>
            collection
            .Select(e => Transformer(e))
            .Aggregate((e1, e2) => Aggregator(e1, e2)) ?? DefaultResult;
    }
}
