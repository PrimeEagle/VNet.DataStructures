namespace VNet.DataStructures.Graph.Algorithms.Clique
{
    public interface IStandardGraphCliqueAlgorithm<TNode, TEdge, TValue> : IGraphAlgorithm<TNode, TEdge, TValue>
                                                                           where TNode : notnull, INode<TValue>
                                                                           where TEdge : notnull, IEdge<TNode, TValue>
                                                                           where TValue : notnull, IComparable<TValue>
    {
        public void Traverse(TNode startNode, Action<TNode> preVisit, Action<TNode> postVisit);
    }
}