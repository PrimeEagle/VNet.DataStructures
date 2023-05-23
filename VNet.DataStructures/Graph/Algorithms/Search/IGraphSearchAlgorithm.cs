namespace VNet.DataStructures.Graph.Algorithms.Search
{
    public interface IGraphSearchAlgorithm<TNode, TEdge> where TNode : notnull, INode
                                                         where TEdge : notnull, IEdge
    {
        public List<TNode> Search();
    }
}