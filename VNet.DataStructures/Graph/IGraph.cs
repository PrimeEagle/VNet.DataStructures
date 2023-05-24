namespace VNet.DataStructures.Graph
{
    public interface IGraph<TNode, TEdge, TValue> where TNode : notnull, INode<TValue>
                                                  where TEdge : notnull, IEdge<TValue>
                                                  where TValue : notnull
    {
        public Dictionary<TNode, List<TEdge>> AdjacencyList { get; init;  }
    }
}