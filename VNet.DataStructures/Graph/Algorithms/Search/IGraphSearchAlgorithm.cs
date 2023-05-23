namespace VNet.DataStructures.Graph.Algorithms.Search
{
    public interface IGraphSearchAlgorithm<TNode, TEdge, TValue> where TNode : notnull, INode<TValue>
                                                                 where TEdge : notnull, IEdge<TValue>
                                                                 where TValue : notnull
    {
        public int Search(int[] list, int value);
    }
}