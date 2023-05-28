namespace VNet.DataStructures.List.Algorithms.Search
{
    public class ListSearchAlgorithmArgs<T> : IListSearchAlgorithmArgs<T>
                                              where T : notnull, IComparable<T>
    {
        public IList<T> List { get; set; }
        public T Value { get; set; }
        public bool HasWildcards { get; set; }

        public ListSearchAlgorithmArgs(IList<T> list)
        {
            List = list;
            Value = default!;
        }
    }
}