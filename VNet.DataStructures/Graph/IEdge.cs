namespace VNet.DataStructures.Graph
{
    //public interface IEdge
    //{
    //    public bool Directed { get; init; }
    //}

    public interface IEdge<out T> where T : notnull
    {
        public new bool Directed { get; init; }
    }
}