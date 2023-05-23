using VNet.DataStructures.Graph.Algorithms.PathFinding;
using VNet.DataStructures.Graph.Algorithms.Search;

namespace VNet.DataStructures.Graph
{
    public abstract class GraphBase<TNode, TEdge, TValue> where TNode : notnull, INode<TValue>
                                                          where TEdge : notnull, IEdge<TValue>
                                                          where TValue : notnull
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

        public virtual void RemoveEdge(TEdge edge)
        {
            foreach (var edgeList in AdjacencyList.Values)
            {
                edgeList.RemoveAll(e => e.Equals(edge));
            }
        }
        public abstract GraphBase<TNode, TEdge, TValue> Clone();
        
        public int Search(IGraphSearchAlgorithm<TNode, TEdge, TValue> searchAlgorithm)
        {
            var results = searchAlgorithm.Search(this);

            return results;
        }
        
        public List<TNode> FindPath(IGraphPathFindingAlgorithm<TNode, TEdge, TValue> pathFindingAlgorithm)
        {
            var results = pathFindingAlgorithm.Find();

            return results;
        }
    }
}