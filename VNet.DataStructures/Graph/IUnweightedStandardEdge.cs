namespace VNet.DataStructures.Graph
{
    public interface IUnweightedStandardEdge<TNode, out TValue> : IStandardEdge<TNode, TValue> where TNode : notnull, INode<TValue>
                                                                                           where TValue : notnull
    {
        public IUnweightedStandardEdge<TNode, TValue> Reverse();
        public IUnweightedStandardEdge<TNode, TValue> Clone();
    }
}