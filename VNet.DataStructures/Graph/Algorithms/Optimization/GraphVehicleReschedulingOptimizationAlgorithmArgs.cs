namespace VNet.DataStructures.Graph.Algorithms.Optimization
{
    public class GraphVehicleReschedulingOptimizationAlgorithmArgs<TNode, TEdge, TValue, TSolution> : IGraphVehicleReschedulingOptimizationAlgorithmArgs<TNode, TEdge, TValue, TSolution>
                                                                                                      where TNode : notnull, INode<TValue>
                                                                                                      where TEdge : notnull, IEdge<TNode, TValue>
                                                                                                      where TValue : notnull, IComparable<TValue>
                                                                                                      where TSolution : notnull
    {
        public IGraph<TNode, TEdge, TValue> Graph { get; init; }
        public List<Path<TNode, TValue>> InitialRoutes { get; init; }

        public GraphVehicleReschedulingOptimizationAlgorithmArgs(IGraph<TNode, TEdge, TValue> graph, List<Path<TNode, TValue>> initialRoutes)
        {
            Graph = graph;
            InitialRoutes = initialRoutes;
        }
    }
}