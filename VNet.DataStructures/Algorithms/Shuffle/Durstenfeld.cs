using System.Numerics;
using VNet.Mathematics.Randomization.Generation;

namespace VNet.DataStructures.Algorithms.Shuffle;

public class Durstenfeld<T> : IShuffleAlgorithm<T> where T : struct, INumber<T>
{
    private readonly IRandomGenerationAlgorithm _random;
    private int _currentPosition;


    public Durstenfeld()
    {
        _random = new DotNet();
        _random.MinValue = 0;
    }

    public Durstenfeld(IRandomGenerationAlgorithm randomGenerator)
    {
        _random = randomGenerator;
        _random.MinValue = 0;
    }

    public IList<T> Shuffle(IList<T> collection)
    {
        var shuffledCollection = new T[collection.Count()].ToList();

        for (_currentPosition = 0; _currentPosition < shuffledCollection.Count; _currentPosition++)
            RollAndSwap(collection, shuffledCollection);

        return shuffledCollection;
    }

    private void RollAndSwap(IList<T> collection, IList<T> shuffledCollection)
    {
        var roll = Roll();
        shuffledCollection[_currentPosition] = shuffledCollection[roll];
        shuffledCollection[roll] = collection[_currentPosition];
    }

    private int Roll()
    {
        _random.MaxValue = _currentPosition + 1;

        return (int) _random.Next();
    }
}