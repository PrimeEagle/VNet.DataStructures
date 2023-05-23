namespace VNet.DataStructures.Graph
{
    public interface IWeightedNormalEdge : IUnweightedNormalEdge
    {
        public double Weight { get; init; }
    }
}