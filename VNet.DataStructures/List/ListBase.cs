using VNet.DataStructures.List.Algorithms.Search;
using VNet.DataStructures.List.Algorithms.Shuffle;
using VNet.DataStructures.List.Algorithms.Sort;

namespace VNet.DataStructures.List
{
    public abstract class ListBase<T> where T : notnull, IComparable<T>
    {
        // ReSharper disable once InconsistentNaming
        protected List<T> _list;



        protected ListBase()
        {

            _list = new List<T>();
        }

        protected ListBase(List<T> list)
        {
            _list = list;
        }

        public virtual IList<T> PerformSearch(IListSearchAlgorithm<T> algorithm, IListSearchAlgorithmArgs<T> args)
        {
            args.List = _list;
            return algorithm.Search(args);
        }

        public virtual IList<T> PerformSort(IListSortAlgorithm<T> algorithm, IListSortAlgorithmArgs<T> args)
        {
            args.List = _list;
            return algorithm.Sort(args);
        }

        public virtual IList<T> PerformShuffle(IListShuffleAlgorithm<T> algorithm, IListShuffleAlgorithmArgs<T> args)
        {
            args.List = _list;
            return algorithm.Shuffle(args);
        }
    }
}