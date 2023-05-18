using System.Numerics;
using VNet.Mathematics.Randomization.Generation;

namespace VNet.DataStructures.Algorithms.Shuffle;

public class InsideOut<T> : IShuffleAlgorithm<T> where T : struct, INumber<T>
{
    private readonly IRandomGenerationAlgorithm _random;

    public InsideOut()
    {
        _random = new DotNet();
        _random.MinValue = 0;
    }

    public InsideOut(IRandomGenerationAlgorithm randomGenerator)
    {
        _random = randomGenerator;
        _random.MinValue = 0;
    }

    public IList<T> Shuffle(IList<T> collection)
    {
        var tempCollection = new List<T>(collection);

        var n = tempCollection.Count;
        for (var i = 0; i < n; i++)
        {
            _random.MaxValue = i + 1;

            var j = (int) _random.Next();
            if (i != j) (tempCollection[i], tempCollection[j]) = (tempCollection[j], tempCollection[i]);
        }

        return tempCollection;
    }
}