namespace VNet.DataStructures.Graph
{
    //public interface IWeightedNormalEdge : INormalEdge
    //{
    //    public double Weight { get; init; }
    //}

    public interface IWeightedNormalEdge<T> : INormalEdge<T> where T : notnull
    {
        public new double Weight { get; init; }
    }
}