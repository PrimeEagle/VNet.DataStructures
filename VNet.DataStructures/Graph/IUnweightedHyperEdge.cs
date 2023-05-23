namespace VNet.DataStructures.Graph
{
    //public interface IUnweightedHyperEdge : IHyperEdge
    //{

    //}

    public interface IUnweightedHyperEdge<T> : IHyperEdge<T> where T : notnull
    {

    }
}