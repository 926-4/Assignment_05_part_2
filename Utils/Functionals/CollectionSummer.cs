namespace UBB_SE_2024_Team_42.Utils.Functionals
{
    internal class CollectionSummer<InputType>
    {
        private readonly CollectionReducer<InputType, int> reducer;
        private static readonly Func<int, int, int> Sum = (e1, e2) => e1 + e2;
        internal CollectionSummer(Func<InputType, int> mapper)
        {
            reducer = new (
                mapper: mapper,
                folder: Sum,
                defaultResult: 0);
            ApplyTo = (list) => reducer.MapThenFold(list);
        }
        internal Func<IEnumerable<InputType>, int> ApplyTo;
    }
}
