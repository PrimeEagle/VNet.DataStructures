namespace VNet.DataStructures.Graph
{
    public class WeightedHyperEdge<T> : UnweightedHyperEdge<T>, IWeightedHyperEdge<T> where T : notnull
    {
        public double Weight { get; init; }


        public WeightedHyperEdge(IList<INode<T>> startNodes, IList<INode<T>> endNodes, bool directed, double weight) : base(startNodes, endNodes, directed)
        {
            Weight = weight;
        }


        public new IWeightedHyperEdge<T> Clone()
        {
            return new WeightedHyperEdge<T>(StartNodes, EndNodes, Directed, Weight);
        }

        public new IWeightedHyperEdge<T> Reverse()
        {
            return new WeightedHyperEdge<T>(EndNodes, StartNodes, Directed, Weight);
        }

        IHyperEdge<T> IHyperEdge<T>.Reverse()
        {
            return Clone();
        }

        IHyperEdge<T> IHyperEdge<T>.Clone()
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