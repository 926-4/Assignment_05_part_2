using System.Data;

namespace UBB_SE_2024_Team_42.Utils.Functionals
{
    internal class StreamProcessor<S, T>
    {
        public static IEnumerable<S> FilterCollection(IEnumerable<S> source, Func<S, bool> condition)
            => source.Where(condition);
        public static T MapOne(IEnumerable<S> source, Func<S, T> mapping)
            => mapping(source.First());
        public static IEnumerable<T> MapCollection(IEnumerable<S> source, Func<S, T> mapping)
            => source.Select(mapping);
        public static T FilterAndMapOne(IEnumerable<S> source, Func<S, bool> condition, Func<S, T> mapping)
            => mapping(source.Where(condition).First());
        public static IEnumerable<T> FilterAndMapCollection(IEnumerable<S> source, Func<S, bool> condition, Func<S, T> mapping)
            => source.Where(condition).Select(mapping);
    }
}
