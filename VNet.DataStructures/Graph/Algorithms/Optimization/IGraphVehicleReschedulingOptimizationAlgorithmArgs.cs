namespace VNet.DataStructures.Graph.Algorithms.Optimization
{
    public interface IGraphVehicleReschedulingOptimizationAlgorithmArgs<TNode, TEdge, TValue, TSolution> : IGraphOptimizationAlgorithmArgs<TNode, TEdge, TValue>
                                                                                                           where TNode : notnull, INode<TValue>
                                                                                                           where TEdge : notnull, IEdge<TNode, TValue>
                                                                                                           where TValue : notnull, IComparable<TValue>
                                                                                                           where TSolution : notnull
    {
        public List<Path<TNode, TValue>> InitialRoutes { get; init; }
    }
}