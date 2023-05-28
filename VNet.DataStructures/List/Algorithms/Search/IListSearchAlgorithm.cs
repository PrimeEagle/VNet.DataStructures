namespace VNet.DataStructures.List.Algorithms.Search
{
    public interface IListSearchAlgorithm<T> : IListAlgorithm
                                               where T : notnull, IComparable<T>
    {
        public IList<T> Search(IListSearchAlgorithmArgs<T> args);
    }
}