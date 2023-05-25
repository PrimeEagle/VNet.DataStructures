namespace VNet.DataStructures.Graph
{
    public interface IHyperEdge<TNode, out TValue> : IEdge<TNode, TValue> where TNode : notnull, INode<TValue>
                                                                          where TValue : notnull
    {
        public HashSet<TNode> StartNodes { get; init; }
        public HashSet<TNode> EndNodes { get; init; }
    }
}