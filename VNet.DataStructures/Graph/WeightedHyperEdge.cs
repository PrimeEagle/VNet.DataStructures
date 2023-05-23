namespace VNet.DataStructures.Graph
{
    public class WeightedHyperEdge<T> : UnweightedHyperEdge<T>, IWeightedHyperEdge<T> where T : notnull
    {
        public double Weight { get; init; }


        public WeightedHyperEdge(IList<INode<T>> startNodes, IList<INode<T>> endNodes, bool directed, double weight) : base(startNodes, endNodes, directed)
        {
            Weight = weight;
        }
    }
}