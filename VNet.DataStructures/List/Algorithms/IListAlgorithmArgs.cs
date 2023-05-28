using VNet.DataStructures.Algorithms;

namespace VNet.DataStructures.List.Algorithms
{
    public interface IListAlgorithmArgs<T> : IDataStructureAlgorithmArgs
                                             where T : notnull
    {
        public IList<T> List { get; set; }
    }
}