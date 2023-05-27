using VNet.DataStructures.List.Algorithms.Search;
using VNet.DataStructures.List.Algorithms.Shuffle;
using VNet.DataStructures.List.Algorithms.Sort;
using VNet.Utility.Extensions;

namespace VNet.DataStructures.List
{
    public static class ListExtensions
    {
        public static IList<T> PerformSearch<T>(this IList<T> list, IListSearchAlgorithm<T> algorithm, IListSearchAlgorithmArgs args) where T : notnull
        {
            return algorithm.Search(list.Clone(), args);
        }

        public static IList<T> PerformSort<T>(this IList<T> list, IListSortAlgorithm<T> algorithm, IListSortAlgorithmArgs args) where T : notnull
        {
            return algorithm.Sort(list.Clone(), args);
        }

        public static IList<T> PerformShuffle<T>(this IList<T> list, IListShuffleAlgorithm<T> algorithm, IListShuffleAlgorithmArgs args) where T : notnull
        {
            return algorithm.Shuffle(list.Clone(),args);
        }
    }
}