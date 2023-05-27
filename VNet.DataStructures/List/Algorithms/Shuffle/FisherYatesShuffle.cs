using System.Numerics;
using VNet.Mathematics.Randomization.Generation;

namespace VNet.DataStructures.List.Algorithms.Shuffle;

public class FisherYatesShuffle<T> : IShuffleAlgorithm<T> where T : struct, INumber<T>
{
    private readonly IRandomGenerationAlgorithm _random;

    public FisherYatesShuffle()
    {
        _random = new DotNet();
        _random.MinValue = 0;
    }

    public FisherYatesShuffle(IRandomGenerationAlgorithm randomGenerator)
    {
        _random = randomGenerator;
        _random.MinValue = 0;
    }

    public IList<T> Shuffle(IList<T> collection)
    {
        var tempCollection = new List<T>(collection);

        for (var n = tempCollection.Count - 1; n > 0; --n)
        {
            _random.MaxValue = n + 1;
            var k = _random.Next();

            (tempCollection[n], tempCollection[k]) = (tempCollection[k], tempCollection[n]);
        }

        return tempCollection;
    }
}