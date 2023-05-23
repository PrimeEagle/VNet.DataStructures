using VNet.DataStructures.Graph.Algorithms.PathFinding;
using VNet.DataStructures.Graph.Algorithms.Search;

namespace VNet.DataStructures.Graph
{
    public abstract class GraphBase<TNode, TEdge> where TNode : notnull, INode
                                                  where TEdge : notnull, IEdge
    {
        protected virtual Dictionary<TNode, List<TEdge>> AdjacencyList { get; }



        protected GraphBase()
        {
            AdjacencyList = new Dictionary<TNode, List<TEdge>>();
        }



        public void AddNode(TNode node)
        {
            if (!AdjacencyList.ContainsKey(node)) AdjacencyList[node] = new List<TEdge>();
        }

        public abstract void RemoveNode(TNode node);
        public abstract void AddEdge(TNode startNode, TNode endNode);
        public abstract void RemoveEdge(TNode startNode, TNode endNode);
        public abstract GraphBase<TNode, TEdge> Clone();
        
        public List<TNode> Search(IGraphSearchAlgorithm<TNode, TEdge> searchAlgorithm)
        {
            var results = searchAlgorithm.Search();

            return results;
        }
        
        public List<TNode> FindPath(IGraphPathFindingAlgorithm<TNode, TEdge> pathFindingAlgorithm)
        {
            var results = pathFindingAlgorithm.Find();

            return results;
        }
    }
}