using NUnit.Framework.Interfaces;

namespace UBB_SE_2024_Team_42.Utils.Functionals
{
    internal class CollectionStringifier<InputType>
    {
        private static string StringifyAnything(InputType inputObject) => inputObject?.ToString() ?? string.Empty;
        private static string ConcatStringWithSpaceBetween(string firstString, string secondString) => $"{firstString} {secondString}";
        private static readonly CollectionReducer<InputType, string> Reducer =
            new (mapper: StringifyAnything,
                folder: ConcatStringWithSpaceBetween,
                defaultResult: "None");
        public static string ApplyTo(IEnumerable<InputType> collection) => Reducer.MapThenFold(collection);
    }
}
