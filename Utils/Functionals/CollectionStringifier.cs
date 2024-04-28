using UBB_SE_2024_Team_42.Utils.Functionals;

namespace UBB_SE_2024_Team_42.Utils.Functionals
{
    internal class CollectionStringifier<T>
    {
        private static readonly CollectionReducer<T, string> Reducer =
            new (mapper: e => e?.ToString() ?? string.Empty,
                folder: (e1, e2) => $"{e1} {e2}",
                defaultResult: "None");

        internal static Func<IEnumerable<T>, string> ApplyTo = (list) => Reducer.MapThenFold(list);
    }
}
