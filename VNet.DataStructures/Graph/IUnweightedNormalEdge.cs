namespace VNet.DataStructures.Graph
{
    public interface IUnweightedNormalEdge : IEdge
    {
        public INode StartNode { get; init; }
        public INode EndNode { get; init; }
    }
}