using System.Numerics;
using VNet.Mathematics.Randomization.Generation;

namespace VNet.DataStructures.Algorithms.Shuffle;

public class FisherYates<T> : IShuffleAlgorithm<T> where T : struct, INumber<T>
{
    private readonly IRandomGenerationAlgorithm _random;

    public FisherYates()
    {
        _random = new DotNet();
        _random.MinValue = (int)0;
    }

    public FisherYates(IRandomGenerationAlgorithm randomGenerator)
    {
        _random = randomGenerator;
        _random.MinValue = (int)0;
    }

    public IList<T> Shuffle(IList<T> collection)
    {
        var tempCollection = new List<T>(collection);

        for (var n = tempCollection.Count - 1; n > 0; --n)
        {
            _random.MaxValue = (int)(n + 1);
            var k = (int)_random.Next();

            (tempCollection[n], tempCollection[k]) = (tempCollection[k], tempCollection[n]);
        }

        return tempCollection;
    }
}