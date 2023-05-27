namespace VNet.DataStructures.List.Algorithms.Sort
{
    public interface IListSortAlgorithm<T> : IListAlgorithm
        where T : notnull
    {
        public IList<T> Sort(IList<T> list, IListSortAlgorithmArgs args);
    }
}