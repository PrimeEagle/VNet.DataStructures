using System.Numerics;
using VNet.Mathematics.Randomization.Generation;

namespace VNet.DataStructures.List.Algorithms.Shuffle;

public class InsideOutShuffle<T> : IShuffleAlgorithm<T> where T : struct, INumber<T>
{
    private readonly IRandomGenerationAlgorithm _random;

    public InsideOutShuffle()
    {
        _random = new DotNetGenerator();
        _random.MinValue = 0;
    }

    public InsideOutShuffle(IRandomGenerationAlgorithm randomGenerator)
    {
        _random = randomGenerator;
        _random.MinValue = 0;
    }

    public IList<T> Shuffle(IListShuffleAlgorithmArgs<T> args)
    {
        var tempCollection = new List<T>((List<T>)args.List);

        var n = tempCollection.Count;
        for (var i = 0; i < n; i++)
        {
            _random.MaxValue = i + 1;

            var j = _random.Next();
            if (i != j) (tempCollection[i], tempCollection[j]) = (tempCollection[j], tempCollection[i]);
        }

        return tempCollection;
    }
}