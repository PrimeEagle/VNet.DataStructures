namespace VNet.DataStructures.Graph
{
    public interface IWeightedHyperEdge<TNode, out TValue> : IUnweightedHyperEdge<TNode, TValue> where TNode : notnull, INode<TValue>
                                                                                                 where TValue : notnull
    {
        public double Weight { get; init; }
    }
}