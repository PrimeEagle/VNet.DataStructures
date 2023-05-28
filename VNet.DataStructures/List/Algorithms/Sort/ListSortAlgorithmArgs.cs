namespace VNet.DataStructures.List.Algorithms.Sort
{
    public class ListSortAlgorithmArgs<T> : IListAlgorithmArgs<T>
                                            where T : notnull
    {
        public IList<T> List { get; set; }



        public ListSortAlgorithmArgs(IList<T> list)
        {
            List = list;
        }
    }
}