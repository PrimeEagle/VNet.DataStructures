namespace VNet.DataStructures.Graph.Algorithms.Partitioning
{
    public class GraphPartitioningAlgorithmArgs<TNode, TEdge, TValue> : IGraphPartitioningAlgorithmArgs<TNode, TEdge, TValue>
                                                                        where TNode : notnull, INode<TValue>
                                                                        where TEdge : notnull, IEdge<TNode, TValue>
                                                                        where TValue : notnull, IComparable<TValue>
    {
        public GraphPartitioningAlgorithmArgs(IGraph<TNode, TEdge, TValue> graph)
        {
            Graph = graph;
        }

        public IGraph<TNode, TEdge, TValue> Graph { get; init; }
    }
}