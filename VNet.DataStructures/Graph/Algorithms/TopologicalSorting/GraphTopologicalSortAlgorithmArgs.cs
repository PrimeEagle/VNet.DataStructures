namespace VNet.DataStructures.Graph.Algorithms.TopologicalSorting
{
    public class GraphTopologicalSortAlgorithmArgs<TNode, TEdge, TValue> : IGraphTopologicalSortAlgorithmArgs<TNode, TEdge, TValue>
                                                                           where TNode : notnull, INode<TValue>
                                                                           where TEdge : notnull, IEdge<TNode, TValue>
                                                                           where TValue : notnull
    {
        public GraphTopologicalSortAlgorithmArgs(IGraph<TNode, TEdge, TValue> graph)
        {
            Graph = graph;
        }

        public IGraph<TNode, TEdge, TValue> Graph { get; init; }
    }
}