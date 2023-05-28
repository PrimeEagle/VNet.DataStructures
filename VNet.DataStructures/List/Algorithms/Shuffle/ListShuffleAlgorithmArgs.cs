namespace VNet.DataStructures.List.Algorithms.Shuffle
{
    public class ListShuffleAlgorithmArgs<T> : IListShuffleAlgorithmArgs<T>
                                               where T : notnull
    {
        public IList<T> List { get; set; }



        public ListShuffleAlgorithmArgs(IList<T> list)
        {
            List = list;
        }
    }
}