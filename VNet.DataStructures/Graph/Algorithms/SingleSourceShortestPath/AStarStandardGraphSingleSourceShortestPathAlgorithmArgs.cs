namespace VNet.DataStructures.Graph.Algorithms.SingleSourceShortestPath
{
    public class AStarStandardGraphSingleSourceShortestPathAlgorithmArgs<TNode, TEdge, TValue> : IAStarStandardGraphSingleSourceShortestPathAlgorithmArgs<TNode, TEdge, TValue>
                                                                                                 where TNode : notnull, INode<TValue>
                                                                                                 where TEdge : notnull, IEdge<TNode, TValue>
                                                                                                 where TValue : notnull, IComparable<TValue>
    {
        public IGraph<TNode, TEdge, TValue> Graph { get; init; }
        public TNode? StartNode { get; set; }
        public TNode? EndNode { get; set; }
        public Func<TNode, TNode, double> Heuristic { get; init; }



        public AStarStandardGraphSingleSourceShortestPathAlgorithmArgs(IGraph<TNode, TEdge, TValue> graph, TNode startNode, TNode endNode, Func<TNode, TNode, double> heuristic)
        {
            Graph = graph;
            StartNode = startNode;
            EndNode = endNode;
            Heuristic = heuristic;
        }
    }
}