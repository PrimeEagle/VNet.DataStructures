namespace VNet.DataStructures.Graph.Algorithms.MinimumSpanningTree
{
    public interface IGraphMinimumSpanningTreeAlgorithmArgs<TNode, TEdge, TValue> : IGraphAlgorithmArgs<TNode, TEdge, TValue>
                                                                                    where TNode : notnull, INode<TValue>
                                                                                    where TEdge : notnull, IEdge<TNode, TValue>
                                                                                    where TValue : notnull
    {

    }
}