namespace VNet.DataStructures.Graph.Algorithms.PathFinding
{
    public interface IGraphPathFindingAlgorithm<TNode, TEdge> where TNode : notnull, INode
                                                              where TEdge : notnull, IEdge
    {
        public List<TNode> Find();
    }
}