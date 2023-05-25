namespace VNet.DataStructures.Graph
{
    public interface IUnweightedSimpleEdge<TNode, out TValue> : ISimpleEdge<TNode, TValue> where TNode : notnull, INode<TValue>
                                                                                           where TValue : notnull
    {
        public IUnweightedSimpleEdge<TNode, TValue> Reverse();
        public IUnweightedSimpleEdge<TNode, TValue> Clone();
    }
}