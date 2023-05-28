namespace VNet.DataStructures.Graph.Algorithms.PathFinding
{
    public class GraphPathFindingAlgorithmArgs<TNode, TEdge, TValue> : IGraphPathFindingAlgorithmArgs<TNode, TEdge, TValue>
                                                                       where TNode : notnull, INode<TValue>
                                                                       where TEdge : notnull, IEdge<TNode, TValue>
                                                                       where TValue : notnull
    {
        public GraphPathFindingAlgorithmArgs(IGraph<TNode, TEdge, TValue> graph)
        {
            Graph = graph;
        }

        public IGraph<TNode, TEdge, TValue> Graph { get; init; }
    }
}