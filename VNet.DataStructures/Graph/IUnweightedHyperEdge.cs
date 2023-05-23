namespace VNet.DataStructures.Graph
{
    public interface IUnweightedHyperEdge : IEdge
    {
        public IList<INode> StartNodes { get; init; }
        public IList<INode> EndNodes { get; init; }
    }
}