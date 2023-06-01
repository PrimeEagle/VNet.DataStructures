namespace VNet.DataStructures.Graph.Algorithms.Optimization
{
    public class GraphVehicleRoutingOptimizationAlgorithmArgs<TNode, TEdge, TValue, TSolution> : IGraphVehicleRoutingOptimizationAlgorithmArgs<TNode, TEdge, TValue>
                                                                                                 where TNode : notnull, INode<TValue>
                                                                                                 where TEdge : notnull, IEdge<TNode, TValue>
                                                                                                 where TValue : notnull, IComparable<TValue>
                                                                                                 where TSolution : notnull
    {
        public IGraph<TNode, TEdge, TValue> Graph { get; init; }

        public TNode Depot { get; init; }
        public int VehicleCount { get; init; }

        public GraphVehicleRoutingOptimizationAlgorithmArgs(IGraph<TNode, TEdge, TValue> graph, TNode depot, int vehicleCount)
        {
            Graph = graph;
            Depot = depot;
            VehicleCount = vehicleCount;
        }
    }
}