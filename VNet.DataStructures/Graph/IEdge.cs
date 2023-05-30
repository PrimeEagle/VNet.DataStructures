namespace VNet.DataStructures.Graph
{
    public interface IEdge<out TNode, out TValue> where TNode : notnull, INode<TValue>
                                                  where TValue : notnull
    {
        public bool Directed { get; init; }
        public IEdge<TNode, TValue> Clone(bool deep = false);
    }
}