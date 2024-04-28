namespace UBB_SE_2024_Team_42.Utils.functionbros
{
    internal class CollectionStringifier<T>
    {
        private static readonly CollectionReducer<T, string> reducer =
            new(mapper: e => e?.ToString() ?? "",
                folder: (e1, e2) => $"{e1} {e2}",
                defaultResult: "None"
                );

        internal static Func<IEnumerable<T>, string> ApplyTo = (list) => reducer.MapThenFold(list);
    }
}
