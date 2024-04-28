namespace UBB_SE_2024_Team_42.Utils.functionbros
{
    internal class CollectionSummer<T>
    {
        private readonly CollectionReducer<T, int> reducer;
        private static readonly Func<int, int, int> sum = (e1, e2) => e1 + e2;
        internal CollectionSummer(Func<T, int> mapper)
        {
            reducer = new(
                mapper: mapper,
                folder: sum,
                defaultResult: 0
                );
            ApplyTo = (list) => reducer.MapThenFold(list);
        }

        internal Func<IEnumerable<T>, int> ApplyTo;
    }
}
