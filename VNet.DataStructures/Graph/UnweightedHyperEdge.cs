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

        public new IUnweightedHyperEdge<T> Clone()
        {
            return new UnweightedHyperEdge<T>(StartNodes, EndNodes, Directed);
        }

        public new IUnweightedHyperEdge<T> Reverse()
        {
            return new UnweightedHyperEdge<T>(EndNodes, StartNodes, Directed);
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