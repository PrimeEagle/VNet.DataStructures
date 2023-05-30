// ReSharper disable MemberCanBeProtected.Global

using VNet.Utility.Extensions;

namespace VNet.DataStructures.Graph
{
    public class UnweightedHyperEdge<TNode, TValue> : IUnweightedHyperEdge<TNode, TValue> where TNode : notnull, INode<TValue>
                                                                                          where TValue : notnull
    {
        public HashSet<TNode> StartNodes { get; init; }
        public HashSet<TNode> EndNodes { get; init; }


        public UnweightedHyperEdge(HashSet<TNode> startNodes, HashSet<TNode> endNodes, bool directed)
        {
            StartNodes = startNodes;
            EndNodes = endNodes;
        }

        public UnweightedHyperEdge(TNode startNode, TNode endNode, bool directed)
        {
            StartNodes = new HashSet<TNode>() { startNode };
            EndNodes = new HashSet<TNode>() { endNode };
        }

        public bool Directed { get; init; }



        public IEdge<TNode, TValue> Clone(bool deep = false)
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

            return new UnweightedHyperEdge<TNode, TValue>(startNodes, endNodes, Directed);
        }

        public IUnweightedHyperEdge<TNode, TValue> Reverse()
        {
            return new UnweightedHyperEdge<TNode, TValue>(EndNodes, StartNodes, Directed);
        }
    }
}