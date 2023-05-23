namespace VNet.DataStructures.Graph
{
    public class UnweightedHyperEdge : Edge, IUnweightedHyperEdge
    {
        public IList<INode> StartNodes { get; init; }
        public IList<INode> EndNodes { get; init; }


        public UnweightedHyperEdge(IList<INode> startNodes, IList<INode> endNodes, bool directed) : base(directed)
        {
            StartNodes = startNodes;
            EndNodes = endNodes;
        }
    }
}