namespace VNet.DataStructures.Graph.Algorithms.ShortestPath
{
    public class GraphShortestPathAlgorithmArgs<TNode, TEdge, TValue> : IGraphShortestPathAlgorithmArgs<TNode, TEdge, TValue>
                                                                        where TNode : notnull, INode<TValue>
                                                                        where TEdge : notnull, IEdge<TNode, TValue>
                                                                        where TValue : notnull, IComparable<TValue>
    {
        public GraphShortestPathAlgorithmArgs(IGraph<TNode, TEdge, TValue> graph, TNode startNode, TNode endNode)
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