namespace VNet.DataStructures.Graph
{
    public class LineEdge<THyperEdge, TNode, TValue> : ILineEdge<THyperEdge, TNode, TValue>
                                                       where THyperEdge : notnull, IHyperEdge<TNode, TValue>
                                                       where TNode : notnull, INode<TValue>
                                                       where TValue : notnull
    {
        public bool Directed { get; init; }
        public THyperEdge StartEdge { get; init; }
        public THyperEdge EndEdge { get; init; }


        public LineEdge(THyperEdge startEdge, THyperEdge endEdge)
        {
            StartEdge = startEdge;
            EndEdge = endEdge;
            Directed = false;
        }


        public ILineEdge<THyperEdge, TNode, TValue> Clone()
        {
            return new LineEdge<THyperEdge, TNode, TValue>(StartEdge, EndEdge);
        }

        public ILineEdge<THyperEdge, TNode, TValue> Reverse()
        {
            return new LineEdge<THyperEdge, TNode, TValue>(StartEdge, EndEdge);
        }
    }
}