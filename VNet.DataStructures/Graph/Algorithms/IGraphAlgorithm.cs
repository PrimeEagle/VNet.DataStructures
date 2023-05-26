using VNet.DataStructures.Algorithms;

namespace VNet.DataStructures.Graph.Algorithms.Traversal
{
    public interface IGraphAlgorithm<TNode, TValue> : IDataStructureAlgorithm
        where TNode : notnull, INode<TValue>
                                                             where TValue : notnull
    {
    }
}