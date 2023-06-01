namespace VNet.DataStructures.Graph.Algorithms.Optimization
{
    public interface IGraphTravelingPurchaserOptimizationAlgorithmArgs<TNode, TEdge, TValue> : IGraphOptimizationAlgorithmArgs<TNode, TEdge, TValue>
                                                                                               where TNode : notnull, INode<TValue>
                                                                                               where TEdge : notnull, IEdge<TNode, TValue>
                                                                                               where TValue : notnull, IComparable<TValue>
    {
        public Dictionary<TNode, TValue> PurchaseCosts { get; init; }
    }
}