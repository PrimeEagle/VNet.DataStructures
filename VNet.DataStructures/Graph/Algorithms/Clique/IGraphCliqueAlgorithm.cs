namespace VNet.DataStructures.Graph.Algorithms.Clique
{
    public interface IGraphCliqueAlgorithm<TNode, TValue> : IGraphAlgorithm<TNode, TValue>
        where TNode : notnull, INode<TValue>
                                                             where TValue : notnull
    {
    }
}