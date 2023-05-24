namespace VNet.DataStructures.Graph
{
    public interface IUnweightedHyperEdge<T> : IHyperEdge<T> where T : notnull
    {
        public new IUnweightedHyperEdge<T> Reverse();
        public new IUnweightedSimpleEdge<T> Clone();
    }
}