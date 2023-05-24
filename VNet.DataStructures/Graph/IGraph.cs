using VNet.DataStructures.Graph.Algorithms.PathFinding;
using VNet.DataStructures.Graph.Algorithms.Search;

namespace VNet.DataStructures.Graph
{
    public interface IGraph<TNode, TEdge, TValue> where TNode : notnull, INode<TValue>
                                                  where TEdge : notnull, IEdge<TValue>
                                                  where TValue : notnull
    {
        public Dictionary<TNode, List<TEdge>> AdjacencyList { get; init;  }


        public void AddNode(TNode node);
        public TValue Search(IGraphSearchAlgorithm<TNode, TEdge, TValue> searchAlgorithm, TValue value);
        public List<TNode> FindPath(IGraphPathFindingAlgorithm<TNode, TEdge, TValue> pathFindingAlgorithm, INode<TValue> start, INode<TValue> end);
        public IGraph<TNode, TEdge, TValue> Clone();
    }
}