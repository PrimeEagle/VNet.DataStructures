namespace VNet.DataStructures.Graph
{
    public class  WeightedSimpleEdge<TNode, TValue> : UnweightedSimpleEdge<TNode, TValue>, IWeightedSimpleEdge<TNode, TValue> where TNode : notnull, INode<TValue>
                                                                                                                              where TValue : notnull
    {
        public double Weight { get; init; }


        public WeightedSimpleEdge(TNode startNode, TNode endNode, bool directed, double weight) : base(startNode, endNode, directed)
        {
            Weight = weight;
        }

        public new IWeightedSimpleEdge<TNode, TValue> Clone()
        {
            return new WeightedSimpleEdge<TNode, TValue>(StartNode, EndNode, Directed, Weight);
        }

        public new IWeightedSimpleEdge<TNode, TValue> Reverse()
        {
            return new WeightedSimpleEdge<TNode, TValue>(EndNode, StartNode, Directed, Weight);
        }
    }
}