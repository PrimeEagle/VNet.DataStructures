using System.Collections;
using System.Numerics;
using VNet.DataStructures.List.Algorithms;
using VNet.DataStructures.List.Algorithms.Shuffle;
using VNet.Mathematics.Randomization.Generation;
using VNet.System.Conversion;

namespace VNet.DataStructures.Bag;

internal class ShuffleBag<T> : IEnumerable<T>, IReadOnlyCollection<T> where T : struct, INumber<T>
{
    private List<T> _data;
    private T _currentItem;
    private int _index = -1;


    public ShuffleBag(int initCapacity)
    {
        _currentItem = default;
        RandomGenerator = new DotNet();
        _data = new List<T>(initCapacity);
        ShuffleProvider = new FisherYatesShuffle<T>(RandomGenerator);
    }

    public ShuffleBag(int initCapacity, IShuffleAlgorithm<T> shuffle)
    {
        _currentItem = default;
        RandomGenerator = new DotNet();
        _data = new List<T>(initCapacity);
        ShuffleProvider = shuffle;
    }

    public ShuffleBag(int initCapacity, IRandomGenerationAlgorithm randomGenerator, IShuffleAlgorithm<T> shuffle)
    {
        _currentItem = default;
        RandomGenerator = randomGenerator;
        _data = new List<T>(initCapacity);
        ShuffleProvider = shuffle;
        ;
    }


    // ReSharper disable once MemberCanBePrivate.Global
    public IShuffleAlgorithm<T> ShuffleProvider { get; set; }

    // ReSharper disable once MemberCanBePrivate.Global
    public IRandomGenerationAlgorithm RandomGenerator { get; set; }

    // ReSharper disable once MemberCanBePrivate.Global
    public int Capacity => _data.Capacity;

    public IEnumerator<T> GetEnumerator()
    {
        return _data.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _data.GetEnumerator();
    }

    public int Count => ((IReadOnlyCollection<T>) _data).Count;

    public void Fill()
    {
        for (var i = 0; i < Capacity; i++) _data.Add(GenericNumber<T>.FromDouble(RandomGenerator.Next()));
        Shuffle();
    }

    // ReSharper disable once MemberCanBePrivate.Global
    public void Shuffle()
    {
        _data = (List<T>)ShuffleProvider.Shuffle(new ListShuffleAlgorithmArgs<T>(_data));
    }

    public void Add(T item)
    {
        _data.Add(item);
        _index = Count - 1;
    }

    public T Next()
    {
        if (_index < 1)
        {
            _index = Count - 1;
            if (_data.Count < 1)
                _currentItem = default;
            _currentItem = _data[0];
        }
        else
        {
            RandomGenerator.MinValue = 0;
            RandomGenerator.MaxValue = _index;
            var pos = (int) RandomGenerator.Next();

            _currentItem = _data[pos];
            _data[pos] = _data[_index];
            _data[_index] = _currentItem;
            _index--;
        }

        return _currentItem;
    }
}