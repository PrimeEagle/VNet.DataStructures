namespace VNet.DataStructures.Graph
{
    //public interface IUnweightedNormalEdge : INormalEdge
    //{
    //}

    public interface IUnweightedNormalEdge<T> : INormalEdge<T> where T : notnull
    {

    }
}