using System.Numerics;
using VNet.Mathematics.Randomization.Generation;

namespace VNet.DataStructures.List.Algorithms.Shuffle;

public class SatolloShuffle<T> : IShuffleAlgorithm<T> where T : struct, INumber<T>
{
    private readonly IRandomGenerationAlgorithm _random;


    public SatolloShuffle()
    {
        _random = new DotNetGenerator();
    }

    public SatolloShuffle(IRandomGenerationAlgorithm randomGenerator)
    {
        _random = randomGenerator;
    }

    public IList<T> Shuffle(IListShuffleAlgorithmArgs<T> args)
    {
        var tempCollection = new List<T>((List<T>)args.List);

        var n = tempCollection.Count;

        for (var i = n - 1; i > 0; i--)
        {
            var j = _random.NextInclusive(0, i);
            (tempCollection[i], tempCollection[j]) = (tempCollection[j], tempCollection[i]);
        }

        return tempCollection;
    }
}