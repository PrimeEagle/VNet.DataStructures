// ReSharper disable MemberCanBeProtected.Global

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



        public IUnweightedHyperEdge<TNode, TValue> Clone()
        {
            return new UnweightedHyperEdge<TNode, TValue>(StartNodes, EndNodes, Directed);
        }

        public IUnweightedHyperEdge<TNode, TValue> Reverse()
        {
            return new UnweightedHyperEdge<TNode, TValue>(EndNodes, StartNodes, Directed);
        }
    }
}