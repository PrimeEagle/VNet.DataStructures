namespace VNet.DataStructures.Graph
{
    public interface IStandardEdge<TNode, out TValue> : IEdge<TNode, TValue> where TNode : notnull, INode<TValue>
                                                                           where TValue : notnull
    {
        public TNode StartNode { get; init; }
        public TNode EndNode { get; init; }
    }
}