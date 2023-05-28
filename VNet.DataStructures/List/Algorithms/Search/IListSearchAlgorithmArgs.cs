namespace VNet.DataStructures.List.Algorithms.Search
{
    public interface IListSearchAlgorithmArgs<T> : IListAlgorithmArgs<T>
                                                   where T : notnull, IComparable<T>
    {
        T Value { get; set; }
        public bool HasWildcards { get; set; }
    }
}