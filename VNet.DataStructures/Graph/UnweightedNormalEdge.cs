namespace VNet.DataStructures.Graph
{
    public class UnweightedNormalEdge : Edge, IUnweightedNormalEdge
    {
        public INode StartNode { get; init; }
        public INode EndNode { get; init; }


        public UnweightedNormalEdge(INode startNode, INode endNode, bool directed) : base(directed)
        {
            StartNode = startNode;
            EndNode = endNode;
        }
    }
}