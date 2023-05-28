namespace VNet.DataStructures.List.Algorithms.Shuffle
{
    public interface IShuffleAlgorithm<T>
    {
        public IList<T> Shuffle(IListShuffleAlgorithmArgs<T> args);
    }
}