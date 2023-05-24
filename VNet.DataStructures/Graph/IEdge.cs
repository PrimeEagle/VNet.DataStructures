namespace VNet.DataStructures.Graph
{
    public interface IEdge<out T> where T : notnull
    {
        public new bool Directed { get; init; }


        public IEdge<T> Reverse();
        public IEdge<T> Clone();
    }
}