namespace VNet.DataStructures.Graph.Algorithms.Clique
{
    public interface IHyperGraphCliqueAlgorithm<TNode, TValue> : IGraphCliqueAlgorithm<TNode, TValue>
        where TNode : notnull, INode<TValue>
                                                             where TValue : notnull
    {
    }
}