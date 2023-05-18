using System.Numerics;
using VNet.Mathematics.Randomization.Generation;

namespace VNet.DataStructures.Algorithms.Shuffle;

public class Satollo<T> : IShuffleAlgorithm<T> where T : struct, INumber<T>
{
    private readonly IRandomGenerationAlgorithm _random;
    private int _currentPosition;


    public Satollo()
    {
        _random = new DotNet();
        _random.MinValue = 0;
    }

    public Satollo(IRandomGenerationAlgorithm randomGenerator)
    {
        _random = randomGenerator;
        _random.MinValue = 0;
    }

    public IList<T> Shuffle(IList<T> collection)
    {
        var tempCollection = new List<T>(collection);

        var n = tempCollection.Count;

        for (var i = n - 1; i > 0; i--)
        {
            _random.MaxValue = i;
            var j = (int) _random.Next();
            (tempCollection[i], tempCollection[j]) = (tempCollection[j], tempCollection[i]);
        }

        return tempCollection;
    }
}