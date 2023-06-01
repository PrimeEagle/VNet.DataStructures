namespace VNet.DataStructures.Graph.Algorithms.Optimization
{
    public interface IStandardGraphOptimizationAlgorithm<TNode, TEdge, TValue> : IGraphAlgorithm<TNode, TEdge, TValue>
                                                                                where TNode : notnull, INode<TValue>
                                                                                where TEdge : notnull, IEdge<TNode, TValue>
                                                                                where TValue : notnull, IComparable<TValue>
    {
    }
}