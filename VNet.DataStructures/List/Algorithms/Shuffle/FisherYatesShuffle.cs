using System.Numerics;
using VNet.Mathematics.Randomization.Generation;

namespace VNet.DataStructures.List.Algorithms.Shuffle;

public class FisherYatesShuffle<T> : IShuffleAlgorithm<T> where T : struct, INumber<T>
{
    private readonly IRandomGenerationAlgorithm _random;

    public FisherYatesShuffle()
    {
        _random = new DotNetGenerator();
    }

    public FisherYatesShuffle(IRandomGenerationAlgorithm randomGenerator)
    {
        _random = randomGenerator;
    }

    public IList<T> Shuffle(IListShuffleAlgorithmArgs<T> args)
    {
        var tempCollection = new List<T>(args.List);

        for (var n = tempCollection.Count - 1; n > 0; --n)
        {
            var k = _random.NextInclusive(0, n + 1);

            (tempCollection[n], tempCollection[k]) = (tempCollection[k], tempCollection[n]);
        }

        return tempCollection;
    }
}