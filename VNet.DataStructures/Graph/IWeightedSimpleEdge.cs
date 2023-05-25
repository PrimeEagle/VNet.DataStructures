namespace VNet.DataStructures.Graph
{
    public interface IWeightedSimpleEdge<TNode, out TValue> : IUnweightedSimpleEdge<TNode, TValue> where TNode : notnull, INode<TValue>
                                                                                                   where TValue : notnull
    {
        public double Weight { get; init; }
    }
}