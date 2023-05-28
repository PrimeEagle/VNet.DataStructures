namespace VNet.DataStructures.List.Algorithms
{
    public class ListAlgorithmArgs<T> : IListAlgorithmArgs<T>
                                        where T : notnull
    {
        public IList<T> List { get; set; }

        public ListAlgorithmArgs()
        {
            List = new List<T>();
        }

        public ListAlgorithmArgs(IList<T> list)
        {
            List = list;
        }
    }
}