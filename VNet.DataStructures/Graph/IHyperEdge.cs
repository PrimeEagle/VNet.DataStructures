namespace VNet.DataStructures.Graph
{
    public interface IHyperEdge<TNode, out TValue> : IEdge<TNode, TValue> where TNode : notnull, INode<TValue>
                                                                          where TValue : notnull
    {
        public List<TNode> StartNodes { get; init; }
        public List<TNode> EndNodes { get; init; }
    }
}