namespace VNet.DataStructures.Graph.Algorithms.Traversal
{
    public class GraphTraversalAlgorithmArgs<TNode, TEdge, TValue> : IGraphTraversalAlgorithmArgs<TNode, TEdge, TValue>
                                                                     where TNode : notnull, INode<TValue>
                                                                     where TEdge : notnull, IEdge<TNode, TValue>
                                                                     where TValue : notnull
    {
        public GraphTraversalAlgorithmArgs(IGraph<TNode, TEdge, TValue> graph)
        {
            Graph = graph;
        }

        public IGraph<TNode, TEdge, TValue> Graph { get; init; }
    }
}