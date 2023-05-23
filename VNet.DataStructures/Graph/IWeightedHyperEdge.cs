namespace VNet.DataStructures.Graph
{
    //public interface IWeightedHyperEdge : IHyperEdge
    //{
    //    public double Weight { get; set; }
    //}

    public interface IWeightedHyperEdge<T> : IHyperEdge<T> where T : notnull
    {
        public new double Weight { get; init; }
    }
}