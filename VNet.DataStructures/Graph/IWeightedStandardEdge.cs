namespace VNet.DataStructures.Graph
{
    public interface IWeightedStandardEdge<TNode, out TValue> : IUnweightedStandardEdge<TNode, TValue> where TNode : notnull, INode<TValue>
                                                                                                   where TValue : notnull
    {
        public double Weight { get; set; }
    }
}