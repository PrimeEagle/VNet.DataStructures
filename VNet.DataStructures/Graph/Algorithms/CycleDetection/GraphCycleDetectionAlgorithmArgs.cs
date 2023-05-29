namespace VNet.DataStructures.Graph.Algorithms.CycleDetection
{
    public class GraphCycleDetectionAlgorithmArgs<TNode, TEdge, TValue> : IGraphCycleDetectionAlgorithmArgs<TNode, TEdge, TValue>
                                                                          where TNode : notnull, INode<TValue>
                                                                          where TEdge : notnull, IEdge<TNode, TValue>
                                                                          where TValue : notnull, IComparable<TValue>
    {
        public GraphCycleDetectionAlgorithmArgs(IGraph<TNode, TEdge, TValue> graph)
        {
            Graph = graph;
        }

        public IGraph<TNode, TEdge, TValue> Graph { get; init; }
    }
}