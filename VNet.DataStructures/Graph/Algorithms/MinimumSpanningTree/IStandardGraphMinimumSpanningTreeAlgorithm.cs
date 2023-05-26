namespace VNet.DataStructures.Graph.Algorithms.Traversal
{
    public interface IStandardGraphMinimumSpanningTreeAlgorithm<TNode, TValue> : IGraphMinimumSpanningTreeAlgorithm<TNode, TValue>
        where TNode : notnull, INode<TValue>
                                                             where TValue : notnull
    { 
    }
}