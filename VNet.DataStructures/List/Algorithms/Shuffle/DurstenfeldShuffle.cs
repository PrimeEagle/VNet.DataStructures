using System.Numerics;
using VNet.Mathematics.Randomization.Generation;

namespace VNet.DataStructures.List.Algorithms.Shuffle;

public class DurstenfeldShuffle<T> : IShuffleAlgorithm<T> where T : struct, INumber<T>
{
    private readonly IRandomGenerationAlgorithm _random;
    private int _currentPosition;


    public DurstenfeldShuffle()
    {
        _random = new DotNetGenerator();
        _random.MinValue = 0;
    }

    public DurstenfeldShuffle(IRandomGenerationAlgorithm randomGenerator)
    {
        _random = randomGenerator;
        _random.MinValue = 0;
    }

    public IList<T> Shuffle(IListShuffleAlgorithmArgs<T> args)
    {
        var shuffledCollection = new T[args.List.Count].ToList();

        for (_currentPosition = 0; _currentPosition < shuffledCollection.Count; _currentPosition++)
            RollAndSwap(args.List, shuffledCollection);

        return shuffledCollection;
    }

    private void RollAndSwap(IList<T> list, IList<T> shuffledList)
    {
        var roll = Roll();
        shuffledList[_currentPosition] = shuffledList[roll];
        shuffledList[roll] = list[_currentPosition];
    }

    private int Roll()
    {
        _random.MaxValue = _currentPosition + 1;

        return _random.Next();
    }
}