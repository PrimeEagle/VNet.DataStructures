using VNet.DataStructures.List.Algorithms.Search;
using VNet.DataStructures.List.Algorithms.Shuffle;
using VNet.DataStructures.List.Algorithms.Sort;
using VNet.Utility.Extensions;

namespace VNet.DataStructures.List
{
    public abstract class ListBase<T> where T : notnull
    {
        protected List<T> _list;

        public virtual IList<T> PerformSearch(IListSearchAlgorithm<T> algorithm, IListSearchAlgorithmArgs args)
        {
            return algorithm.Search(_list.Clone(), args);
        }

        public virtual IList<T> PerformSort(IListSortAlgorithm<T> algorithm, IListSortAlgorithmArgs args)
        {
            return algorithm.Sort(_list.Clone(), args);
        }

        public virtual IList<T> PerformShuffle(IListShuffleAlgorithm<T> algorithm, IListShuffleAlgorithmArgs args)
        {
            return algorithm.Shuffle(_list.Clone(), args);
        }
    }
}