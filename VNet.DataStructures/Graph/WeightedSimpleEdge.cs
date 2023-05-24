namespace VNet.DataStructures.Graph
{
    public class  WeightedSimpleEdge<T> : UnweightedSimpleEdge<T>, IWeightedSimpleEdge<T> where T : notnull
    {
        public double Weight { get; init; }


        public WeightedSimpleEdge(INode<T> startNode, INode<T> endNode, bool directed, double weight) : base(startNode, endNode, directed)
        {
            Weight = weight;
        }

        public new IWeightedSimpleEdge<T> Clone()
        {
            return new WeightedSimpleEdge<T>(StartNode, EndNode, Directed, Weight);
        }

        public new IWeightedSimpleEdge<T> Reverse()
        {
            return new WeightedSimpleEdge<T>(EndNode, StartNode, Directed, Weight);
        }

        ISimpleEdge<T> ISimpleEdge<T>.Reverse()
        {
            return Clone();
        }

        ISimpleEdge<T> ISimpleEdge<T>.Clone()
        {
            return Reverse();
        }

        IEdge<T> IEdge<T>.Reverse()
        {
            return Reverse();
        }

        IEdge<T> IEdge<T>.Clone()
        {
            return Clone();
        }
    }
}