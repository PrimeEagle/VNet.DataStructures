namespace VNet.DataStructures.Graph.Algorithms.SingleSourceShortestPath
{
    public class GraphSingleSourceShortestPathAlgorithmArgs<TNode, TEdge, TValue> : IGraphSingleSourceShortestPathAlgorithmArgs<TNode, TEdge, TValue>
                                                                        where TNode : notnull, INode<TValue>
                                                                        where TEdge : notnull, IEdge<TNode, TValue>
                                                                        where TValue : notnull, IComparable<TValue>
    {
        public TNode? StartNode { get; set; }
        public TNode? EndNode { get; set; }

        public IGraph<TNode, TEdge, TValue> Graph { get; init; }


        public GraphSingleSourceShortestPathAlgorithmArgs(IGraph<TNode, TEdge, TValue> graph)
        {
            Graph = graph;
        }

        public GraphSingleSourceShortestPathAlgorithmArgs(IGraph<TNode, TEdge, TValue> graph, TNode startNode, TNode endNode)
        {
            Graph = graph;
            StartNode = startNode;
            EndNode = endNode;
        }
    }
}