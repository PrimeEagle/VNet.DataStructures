namespace VNet.DataStructures.Graph
{
    public class WeightedHyperEdge<TNode, TValue> : UnweightedHyperEdge<TNode, TValue>, IWeightedHyperEdge<TNode, TValue> where TNode : notnull, INode<TValue>
                                                                                                                          where TValue : notnull
    {
        public double Weight { get; init; }


        public WeightedHyperEdge(HashSet<TNode> startNodes, HashSet<TNode> endNodes, bool directed, double weight) : base(startNodes, endNodes, directed)
        {
            Weight = weight;
        }

        public IWeightedHyperEdge<TNode, TValue> Clone()
        {
            return new WeightedHyperEdge<TNode, TValue>(StartNodes, EndNodes, Directed, Weight);
        }

        public IWeightedHyperEdge<TNode, TValue> Reverse()
        {
            return new WeightedHyperEdge<TNode, TValue>(EndNodes, StartNodes, Directed, Weight);
        }
    }
}