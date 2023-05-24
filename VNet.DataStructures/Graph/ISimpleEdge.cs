namespace VNet.DataStructures.Graph
{
    public interface ISimpleEdge<T> : IEdge<T> where T : notnull
    {
        public new INode<T> StartNode { get; init; }
        public new INode<T> EndNode { get; init; }

        public new ISimpleEdge<T> Reverse();
        public new ISimpleEdge<T> Clone();
    }
}