namespace UBB_SE_2024_Team_42.Utils.functionbros
{
    internal class CollectionStringifier<T>
    {
        private static readonly CollectionReducer<T, string> listReducer =
            new(transformer: e => e?.ToString() ?? "",
                aggregator: (e1, e2) => $"{e1} {e2}",
                defaultResult: "None");
        internal static string ApplyTo(List<T> list) => listReducer.TransformAndReduce(list);
    }
}
