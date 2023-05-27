namespace VNet.DataStructures.List.Algorithms.Search
{
    public interface IListSearchAlgorithmArgs<T> : IListAlgorithmArgs
                                                   where T : notnull, IComparable<T>
    {
        IList<T> List { get; init; }
        T Value { get; init; }
    }
}