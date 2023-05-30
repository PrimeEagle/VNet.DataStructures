using VNet.Utility.Extensions;

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

        public new IEdge<TNode, TValue> Clone(bool deep = false)
        {
            HashSet<TNode> startNodes;
            HashSet<TNode> endNodes;

            if (deep)
            {
                startNodes = new HashSet<TNode>();
                foreach (var s in StartNodes)
                {
                    startNodes.Add(s.Clone());
                }

                endNodes = new HashSet<TNode>();
                foreach (var e in EndNodes)
                {
                    endNodes.Add(e.Clone());
                }
            }
            else
            {
                startNodes = StartNodes.ToHashSet();
                endNodes = EndNodes.ToHashSet();
            }

            return new WeightedHyperEdge<TNode, TValue>(startNodes, endNodes, Directed, Weight);
        }

        public IWeightedHyperEdge<TNode, TValue> Reverse()
        {
            return new WeightedHyperEdge<TNode, TValue>(EndNodes, StartNodes, Directed, Weight);
        }
    }
}