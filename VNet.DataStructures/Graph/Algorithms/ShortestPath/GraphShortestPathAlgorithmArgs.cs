namespace VNet.DataStructures.Graph.Algorithms.ShortestPath
{
    public class GraphShortestPathAlgorithmArgs<TNode, TEdge, TValue> : IGraphShortestPathAlgorithmArgs<TNode, TEdge, TValue>
                                                                        where TNode : notnull, INode<TValue>
                                                                        where TEdge : notnull, IEdge<TNode, TValue>
                                                                        where TValue : notnull
    {
        public GraphShortestPathAlgorithmArgs(IGraph<TNode, TEdge, TValue> graph)
        {
            Graph = graph;
        }

        public IGraph<TNode, TEdge, TValue> Graph { get; init; }
    }
}