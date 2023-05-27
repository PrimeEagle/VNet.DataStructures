namespace VNet.DataStructures.Graph.Algorithms.Transversal
{
    public interface IStandardGraphTransversalAlgorithm<TNode, TValue> : IGraphTransversalAlgorithm<TNode, TValue>
        where TNode : notnull, INode<TValue>
                                                             where TValue : notnull
    {
    }
}