using VNet.DataStructures.Graph.Algorithms.Traversal;

#pragma warning disable CS8618

namespace VNet.DataStructures.Graph.Algorithms.Search
{
    public class GraphSearchAlgorithmArgs<TNode, TEdge, TValue> : IGraphSearchAlgorithmArgs<TNode, TEdge, TValue>
                                                                  where TNode : notnull, INode<TValue>
                                                                  where TEdge : notnull, IEdge<TNode, TValue>
                                                                  where TValue : notnull, IComparable<TValue>
    {
        public IGraph<TNode, TEdge, TValue> Graph { get; init; }
        public IGraphTraversalAlgorithm<TNode, TEdge, TValue> TraversalAlgorithm { get; set; }
        public IGraphTraversalAlgorithmArgs<TNode, TEdge, TValue> TraversalArgs { get; set; }
        public TNode StartNode { get; set; }
        public TNode? NodeToFind { get; set; }
        public TValue? ValueToFind { get; set; }
        public bool ValueToFindHasWildcards { get; set; }


        public GraphSearchAlgorithmArgs(TNode startNode)
        {
            StartNode = startNode;
        }
    }
}