namespace VNet.DataStructures.Graph.Algorithms.PathFinding
{
    public interface IGraphPathFindingAlgorithm<TNode, TEdge, TValue> : IGraphAlgorithm<TNode, TEdge, TValue>
                                                                        where TNode : notnull, INode<TValue>
                                                                        where TEdge : notnull, IEdge<TNode, TValue>
                                                                        where TValue : notnull, IComparable<TValue>
    {
        public Path<TNode> FindPath(IGraphPathFindingAlgorithmArgs<TNode, TEdge, TValue> args);
    }
}