namespace VNet.DataStructures.Graph.Algorithms.Connectivity
{
    public class GraphConnectivityAlgorithmArgs<TNode, TEdge, TValue> : IGraphConnectivityAlgorithmArgs<TNode, TEdge, TValue>
                                                                        where TNode : notnull, INode<TValue>
                                                                        where TEdge : notnull, IEdge<TNode, TValue>
                                                                        where TValue : notnull
    {
        public GraphConnectivityAlgorithmArgs(IGraph<TNode, TEdge, TValue> graph)
        {
            Graph = graph;
        }

        public IGraph<TNode, TEdge, TValue> Graph { get; init; }
    }
}