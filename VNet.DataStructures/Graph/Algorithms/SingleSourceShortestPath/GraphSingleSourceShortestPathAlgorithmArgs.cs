namespace VNet.DataStructures.Graph.Algorithms.SingleSourceShortestPath
{
    public class GraphSingleSourceShortestPathAlgorithmArgs<TNode, TEdge, TValue> : IGraphSingleSourceShortestPathAlgorithmArgs<TNode, TEdge, TValue>
                                                                        where TNode : notnull, INode<TValue>
                                                                        where TEdge : notnull, IEdge<TNode, TValue>
                                                                        where TValue : notnull, IComparable<TValue>
    {
        public GraphSingleSourceShortestPathAlgorithmArgs(IGraph<TNode, TEdge, TValue> graph, TNode startNode, TNode endNode)
        {
            Graph = graph;
            StartNode = startNode;
            EndNode = endNode;
        }

        public TNode StartNode { get; init; }
        public TNode EndNode { get; init; }

        public IGraph<TNode, TEdge, TValue> Graph { get; init; }
    }
}