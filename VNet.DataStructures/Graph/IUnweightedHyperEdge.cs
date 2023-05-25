namespace VNet.DataStructures.Graph
{
    public interface IUnweightedHyperEdge<TNode, out TValue> : IHyperEdge<TNode, TValue> where TNode : notnull, INode<TValue>
                                                                                         where TValue : notnull
    {
        public new IUnweightedHyperEdge<TNode, TValue> Reverse();
        public new IUnweightedHyperEdge<TNode, TValue> Clone();
    }
}