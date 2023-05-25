namespace VNet.DataStructures.Graph
{
    public interface IEdge<out TNode, out TValue> where TNode : notnull, INode<TValue>
                                              where TValue : notnull
    {
        public bool Directed { get; init; }
    }
}