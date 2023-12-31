﻿namespace VNet.DataStructures.List.Algorithms.Shuffle
{
    public interface IListShuffleAlgorithm<T> : IListAlgorithm
        where T : notnull
    {
        public IList<T> Shuffle(IListShuffleAlgorithmArgs<T> args);
    }
}