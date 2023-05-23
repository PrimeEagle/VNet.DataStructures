// ReSharper disable MemberCanBeProtected.Global

using System.Collections;

namespace VNet.DataStructures.Graph
{
    public class UnweightedHyperEdge<T> : IUnweightedHyperEdge<T> where T : notnull
    {
        public IList<INode<T>> StartNodes { get; init; }
        public IList<INode<T>> EndNodes { get; init; }


        public UnweightedHyperEdge(IList<INode<T>> startNodes, IList<INode<T>> endNodes, bool directed)
        {
            StartNodes = startNodes;
            EndNodes = endNodes;
        }

        public bool Directed { get; init; }
    }
}