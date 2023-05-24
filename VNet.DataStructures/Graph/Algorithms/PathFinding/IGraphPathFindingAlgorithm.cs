namespace VNet.DataStructures.Graph.Algorithms.PathFinding
{
    public interface IGraphPathFindingAlgorithm<TNode, TEdge, TValue> where TNode : notnull, INode<TValue>
                                                                      where TEdge : notnull, IEdge<TValue>
                                                                      where TValue : notnull
    {
        public List<TNode> Find(IGraph<TNode, TEdge, TValue> graph, INode<TValue> start, INode<TValue> end);
    }
}