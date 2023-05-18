using System.Numerics;
using VNet.Mathematics.Randomization.Generation;

namespace VNet.DataStructures.Algorithms.Shuffle;

public class GilbertShannonReedsUniform<T> : IShuffleAlgorithm<T> where T : struct, INumber<T>
{
    private readonly IRandomGenerationAlgorithm _random;

    public GilbertShannonReedsUniform()
    {
        _random = new DotNet();
        _random.MinValue = 0;
    }

    public GilbertShannonReedsUniform(IRandomGenerationAlgorithm randomGenerator)
    {
        _random = randomGenerator;
        _random.MinValue = 0;
    }

    public IList<T> Shuffle(IList<T> collection)
    {
        var tempCollection = new List<T>(collection);

        var n = tempCollection.Count;
        for (var i = 0; i < n - 1; i++)
        for (var j = i + 1; j < n; j++)
            if (ShouldSwap())
            {
                (tempCollection[i], tempCollection[j]) = (tempCollection[j], tempCollection[i]);
            }

        return tempCollection;
    }

    private bool ShouldSwap()
    {
        // Modify the probability distribution as per your requirements
        // Here, we use a uniform distribution (50% probability of swapping)
        _random.MaxValue = 2;
        return (int) _random.Next() == 0;
    }
}