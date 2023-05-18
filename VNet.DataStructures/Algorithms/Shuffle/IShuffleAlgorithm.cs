namespace VNet.DataStructures.Algorithms.Shuffle
{
    public interface IShuffleAlgorithm<T>
    {
        public IList<T> Shuffle(IList<T> collection);
    }
}