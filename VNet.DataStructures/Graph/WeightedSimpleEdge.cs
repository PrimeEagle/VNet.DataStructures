namespace VNet.DataStructures.Graph
{
    public class  WeightedSimpleEdge<T> : UnweightedSimpleEdge<T>, IWeightedSimpleEdge<T> where T : notnull
    {
        public double Weight { get; init; }


        public WeightedSimpleEdge(INode<T> startNode, INode<T> endNode, bool directed, double weight) : base(startNode, endNode, directed)
        {
            Weight = weight;
        }
    }
}