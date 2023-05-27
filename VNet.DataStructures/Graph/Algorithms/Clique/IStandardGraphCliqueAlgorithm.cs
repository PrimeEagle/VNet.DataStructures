namespace VNet.DataStructures.Graph.Algorithms.Clique
{
    public interface IStandardGraphCliqueAlgorithm<TNode, TEdge, TValue> : IGraphAlgorithm<TNode, TEdge, TValue>
                                                                           where TNode : notnull, INode<TValue>
                                                                           where TEdge : notnull, IEdge<TNode, TValue>
                                                                           where TValue : notnull
    {
        public void Traverse(TNode starTNode, Action<TNode> preVisit, Action<TNode> postVisit);
    }
}