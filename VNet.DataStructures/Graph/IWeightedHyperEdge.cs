namespace VNet.DataStructures.Graph
{
    public interface IWeightedHyperEdge : IUnweightedHyperEdge
    {
        public double Weight { get; set; }
    }
}