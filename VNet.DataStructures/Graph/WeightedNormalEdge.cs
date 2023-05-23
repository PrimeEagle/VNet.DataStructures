namespace VNet.DataStructures.Graph
{
    public class  WeightedNormalEdge : UnweightedNormalEdge, IUnweightedNormalEdge
    {
        public double Weight { get; init; }


        public WeightedNormalEdge(INode startNode, INode endNode, bool directed, double weight) : base(startNode, endNode, directed)
        {
            Weight = weight;
        }
    }
}