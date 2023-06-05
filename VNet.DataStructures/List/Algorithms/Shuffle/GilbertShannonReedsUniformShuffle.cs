using System.Numerics;
using VNet.Mathematics.Randomization.Generation;

namespace VNet.DataStructures.List.Algorithms.Shuffle;

public class GilbertShannonReedsUniformShuffle<T> : IShuffleAlgorithm<T> where T : struct, INumber<T>
{
    private readonly IRandomGenerationAlgorithm _random;

    public GilbertShannonReedsUniformShuffle()
    {
        _random = new DotNetGenerator();
        _random.MinValue = 0;
    }

    public GilbertShannonReedsUniformShuffle(IRandomGenerationAlgorithm randomGenerator)
    {
        _random = randomGenerator;
        _random.MinValue = 0;
    }

    public IList<T> Shuffle(IListShuffleAlgorithmArgs<T> args)
    {
        var tempCollection = new List<T>(args.List);

        var n = tempCollection.Count;
        for (var i = 0; i < n - 1; i++)
        for (var j = i + 1; j < n; j++)
            if (ShouldSwap())
                (tempCollection[i], tempCollection[j]) = (tempCollection[j], tempCollection[i]);

        return tempCollection;
    }

    private bool ShouldSwap()
    {
        // Modify the probability distribution as per your requirements
        // Here, we use a uniform distribution (50% probability of swapping)
        _random.MaxValue = 2;
        return _random.Next() == 0;
    }
}