using VNet.DataStructures.Graph.Algorithms.Traversal;

namespace VNet.DataStructures.Graph.Algorithms.Search
{
    public interface IGraphSearchAlgorithmArgs<TNode, TEdge, TValue> : IGraphAlgorithmArgs<TNode, TEdge, TValue>
                                                                       where TNode : notnull, INode<TValue>
                                                                       where TEdge : notnull, IEdge<TNode, TValue>
                                                                       where TValue : notnull, IComparable<TValue>
    {
        public IGraphTraversalAlgorithm<TNode, TEdge, TValue>? TraversalAlgorithm { get; set; }
        public IGraphTraversalAlgorithmArgs<TNode, TEdge, TValue>? TraversalArgs { get; set; }
        public TNode? StartNode { get; set; }
        public TNode? NodeToFind { get; set; }
        public TValue? ValueToFind { get; set; }
        public bool ValueToFindHasWildcards { get; set; }
    }
}