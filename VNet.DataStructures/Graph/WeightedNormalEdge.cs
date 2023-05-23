namespace VNet.DataStructures.Graph
{
    public class  WeightedNormalEdge<T> : UnweightedNormalEdge<T>, IWeightedNormalEdge<T> where T : notnull
    {
        public double Weight { get; init; }


        public WeightedNormalEdge(INode<T> startNode, INode<T> endNode, bool directed, double weight) : base(startNode, endNode, directed)
        {
            Weight = weight;
        }
    }
}