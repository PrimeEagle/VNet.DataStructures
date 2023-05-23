namespace VNet.DataStructures.Graph
{
    //public interface IHyperEdge : IEdge
    //{
    //    public IList<INode> StartNodes { get; init; }
    //    public IList<INode> EndNodes { get; init; }
    //}

    public interface IHyperEdge<T> : IEdge<T> where T : notnull
    {
        public new IList<INode<T>> StartNodes { get; init; }
        public new IList<INode<T>> EndNodes { get; init; }
    }
}