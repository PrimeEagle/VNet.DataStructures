namespace VNet.DataStructures.Graph
{
    //public interface INormalEdge : IEdge
    //{
    //    public INode StartNode { get; init; }
    //    public INode EndNode { get; init; }
    //}

    public interface INormalEdge<T> : IEdge<T> where T : notnull
    {
        public new INode<T> StartNode { get; init; }
        public new INode<T> EndNode { get; init; }
    }
}