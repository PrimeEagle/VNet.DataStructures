namespace VNet.DataStructures.Graph.Algorithms.MinimumSpanningTree
{
    public class GraphMinimumSpanningTreeAlgorithmArgs<TNode, TEdge, TValue> : IGraphMinimumSpanningTreeAlgorithmArgs<TNode, TEdge, TValue>
                                                                               where TNode : notnull, INode<TValue>
                                                                               where TEdge : notnull, IEdge<TNode, TValue>
                                                                               where TValue : notnull
    {
        public GraphMinimumSpanningTreeAlgorithmArgs(IGraph<TNode, TEdge, TValue> graph)
        {
            Graph = graph;
        }

        public IGraph<TNode, TEdge, TValue> Graph { get; init; }
    }
}