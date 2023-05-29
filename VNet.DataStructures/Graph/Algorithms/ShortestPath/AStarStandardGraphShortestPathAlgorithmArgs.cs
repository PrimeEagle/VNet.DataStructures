namespace VNet.DataStructures.Graph.Algorithms.ShortestPath
{
    public class AStarStandardGraphShortestPathAlgorithmArgs<TNode, TEdge, TValue> : IAStarStandardGraphShortestPathAlgorithmArgs<TNode, TEdge, TValue>
                                                                                     where TNode : notnull, INode<TValue>
                                                                                     where TEdge : notnull, IEdge<TNode, TValue>
                                                                                     where TValue : notnull, IComparable<TValue>
    {
        public IGraph<TNode, TEdge, TValue> Graph { get; init; }
        public TNode StartNode { get; init; }
        public TNode EndNode { get; init; }
        public Func<TNode, TNode, double> Heuristic { get; init; }



        public AStarStandardGraphShortestPathAlgorithmArgs(IGraph<TNode, TEdge, TValue> graph, TNode startNode, TNode endNode, Func<TNode, TNode, double> heuristic)
        {
            Graph = graph;
            StartNode = startNode;
            EndNode = endNode;
            Heuristic = heuristic;
        }

    }
}