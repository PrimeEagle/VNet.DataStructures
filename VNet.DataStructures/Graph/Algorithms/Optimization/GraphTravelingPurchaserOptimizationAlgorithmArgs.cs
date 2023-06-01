namespace VNet.DataStructures.Graph.Algorithms.Optimization
{
    public class GraphTravelingPurchaserOptimizationAlgorithmArgs<TNode, TEdge, TValue, TSolution> : IGraphTravelingPurchaserOptimizationAlgorithmArgs<TNode, TEdge, TValue>
                                                                                                     where TNode : notnull, INode<TValue>
                                                                                                     where TEdge : notnull, IEdge<TNode, TValue>
                                                                                                     where TValue : notnull, IComparable<TValue>
                                                                                                     where TSolution : notnull
    {
        public IGraph<TNode, TEdge, TValue> Graph { get; init; }

        public Dictionary<TNode, TValue> PurchaseCosts { get; init; }

        public GraphTravelingPurchaserOptimizationAlgorithmArgs(IGraph<TNode, TEdge, TValue> graph, Dictionary<TNode, TValue> purchaseCosts)
        {
            Graph = graph;
            PurchaseCosts = purchaseCosts;
        }
    }
}