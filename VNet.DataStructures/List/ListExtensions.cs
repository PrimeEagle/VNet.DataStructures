using VNet.DataStructures.List.Algorithms.Search;
using VNet.DataStructures.List.Algorithms.Shuffle;
using VNet.DataStructures.List.Algorithms.Sort;

namespace VNet.DataStructures.List
{
    public static class ListExtensions
    {
        public static IList<T> PerformSearch<T>(this IList<T> list, IListSearchAlgorithm<T> algorithm, IListSearchAlgorithmArgs<T> args) where T : notnull, IComparable<T>
        {
            args.List = list;
            return algorithm.Search(args);
        }

        public static IList<T> PerformSort<T>(this IList<T> list, IListSortAlgorithm<T> algorithm, IListSortAlgorithmArgs<T> args) where T : notnull
        {
            args.List = list;
            return algorithm.Sort(args);
        }

        public static IList<T> PerformShuffle<T>(this IList<T> list, IListShuffleAlgorithm<T> algorithm, IListShuffleAlgorithmArgs<T> args) where T : notnull
        {
            args.List = list;
            return algorithm.Shuffle(args);
        }
    }
}