namespace VNet.DataStructures.Graph.Algorithms.Optimization
{
    public interface IGraphVehicleRoutingOptimizationAlgorithmArgs<TNode, TEdge, TValue> : IGraphOptimizationAlgorithmArgs<TNode, TEdge, TValue>
                                                                                           where TNode : notnull, INode<TValue>
                                                                                           where TEdge : notnull, IEdge<TNode, TValue>
                                                                                           where TValue : notnull, IComparable<TValue>
    {
        public TNode Depot { get; init; }
        public int VehicleCount { get; init; }
    }
}