namespace VNet.DataStructures.Graph
{
    public class  WeightedStandardEdge<TNode, TValue> : UnweightedStandardEdge<TNode, TValue>, IWeightedStandardEdge<TNode, TValue> where TNode : notnull, INode<TValue>
                                                                                                                              where TValue : notnull
    {
        public double Weight { get; init; }


        public WeightedStandardEdge(TNode startNode, TNode endNode, bool directed, double weight) : base(startNode, endNode, directed)
        {
            Weight = weight;
        }

        public new IWeightedStandardEdge<TNode, TValue> Clone()
        {
            return new WeightedStandardEdge<TNode, TValue>(StartNode, EndNode, Directed, Weight);
        }

        public new IWeightedStandardEdge<TNode, TValue> Reverse()
        {
            return new WeightedStandardEdge<TNode, TValue>(EndNode, StartNode, Directed, Weight);
        }
    }
}