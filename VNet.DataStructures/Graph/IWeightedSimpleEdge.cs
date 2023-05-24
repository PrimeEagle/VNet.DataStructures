namespace VNet.DataStructures.Graph
{
    public interface IWeightedSimpleEdge<T> : ISimpleEdge<T> where T : notnull
    {
        public new double Weight { get; init; }
        public new IWeightedSimpleEdge<T> Reverse();
        public new IWeightedSimpleEdge<T> Clone();
    }
}