namespace VNet.DataStructures.Graph
{
    public class WeightedHyperEdge : UnweightedHyperEdge, IUnweightedHyperEdge
    {
        public double Weight { get; set; }


        public WeightedHyperEdge(IList<INode> startNodes, IList<INode> endNodes, bool directed, double weight) : base(startNodes, endNodes, directed)
        {
            Weight = weight;
        }
    }
}