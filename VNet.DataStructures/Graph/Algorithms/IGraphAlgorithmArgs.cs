using VNet.DataStructures.Algorithms;

namespace VNet.DataStructures.Graph.Algorithms
{
    public interface IGraphAlgorithmArgs<TNode, TEdge, TValue> : IDataStructureAlgorithmArgs
                                                                 where TNode : notnull, INode<TValue>
                                                                 where TEdge : notnull, IEdge<TNode, TValue>
                                                                 where TValue : notnull
    {
        public IGraph<TNode, TEdge, TValue> Graph { get; init; }
    }
}