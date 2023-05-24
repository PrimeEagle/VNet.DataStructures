namespace VNet.DataStructures.Graph
{
    public interface IUnweightedSimpleEdge<T> : ISimpleEdge<T> where T : notnull
    {
        public new IUnweightedSimpleEdge<T> Reverse();
        public new IUnweightedSimpleEdge<T> Clone();
    }
}