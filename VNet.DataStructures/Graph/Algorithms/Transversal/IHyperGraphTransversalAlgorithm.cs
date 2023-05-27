namespace VNet.DataStructures.Graph.Algorithms.Transversal
{
    public interface IHyperGraphTransversalAlgorithm<TNode, TValue> : IGraphTransversalAlgorithm<TNode, TValue>
        where TNode : notnull, INode<TValue>
                                                             where TValue : notnull
    {
    }
}