using System.Numerics;
using VNet.Mathematics.Randomization.Generation;

namespace VNet.DataStructures.List.Algorithms.Shuffle;

public class InsideOutShuffle<T> : IShuffleAlgorithm<T> where T : struct, INumber<T>
{
    private readonly IRandomGenerationAlgorithm _random;

    public InsideOutShuffle()
    {
        _random = new DotNetGenerator();
    }

    public InsideOutShuffle(IRandomGenerationAlgorithm randomGenerator)
    {
        _random = randomGenerator;
    }

    public IList<T> Shuffle(IListShuffleAlgorithmArgs<T> args)
    {
        var tempCollection = new List<T>((List<T>)args.List);

        var n = tempCollection.Count;
        for (var i = 0; i < n; i++)
        {
            var j = _random.NextInclusive(0, i + 1);
            if (i != j) (tempCollection[i], tempCollection[j]) = (tempCollection[j], tempCollection[i]);
        }

        return tempCollection;
    }
}