namespace VNet.DataStructures.Graph
{
    public interface IWeightedHyperEdge<T> : IHyperEdge<T> where T : notnull
    {
        public new double Weight { get; init; }
        public new IWeightedHyperEdge<T> Reverse();
        public new IWeightedSimpleEdge<T> Clone();
    }
}