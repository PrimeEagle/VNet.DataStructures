namespace VNet.DataStructures.List.Algorithms.Shuffle
{
    public interface IShuffleAlgorithm<T>
    {
        public IList<T> Shuffle(IList<T> collection);
    }
}