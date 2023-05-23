using VNet.DataStructures.Graph.Basic;

namespace VNet.DataStructures.Graph
{
    public class DirectedGraph<T> : NormalGraphBase<T> where T : notnull, INode<T>
    {
        public override void AddEdge(INode<T> startNode, INode<T> endNode, double weight)
        {
            if (IsEdgeFunctionallyDuplicate(startNode, endNode)) throw new ArgumentException("Edge already exists in adjacency list.");

            var edge = new WeightedEdge<T>(startNode, endNode, weight, true);
            AddEdge(edge);
        }

        public override void AddEdge(INode<T> startNode, INode<T> endNode)
        {
            if (IsEdgeFunctionallyDuplicate(startNode, endNode)) throw new ArgumentException("Edge already exists in adjacency list.");

            var edge = new NormalEdge<T>(startNode, endNode, true);
            AddEdge(edge);
        }

        public override void AddEdge(IUnweightedNormalEdge<T> edge)
        {
            if (IsEdgeDuplicate(edge)) throw new ArgumentException("Edge already exists in adjacency list.");

            if (!AdjacencyList.ContainsKey(edge.StartNode))
            {
                AdjacencyList.Add(edge.StartNode, new List<IEdge<T>>());
            }

            AdjacencyList[edge.StartNode].Add(edge);
        }

        public override void RemoveEdge(INode<T> startNode, INode<T> endNode)
        {
            IEdge<T> edge = null;
            if (AdjacencyList.TryGetValue(startNode, out var value))
            {
                edge = value.FirstOrDefault(e => ((IUnweightedNormalEdge<T>)e).EndNode.Equals(endNode))!;
            }

            if (edge != null) RemoveEdge(edge);
        }

        public override void RemoveEdge(IUnweightedNormalEdge<T> edge)
        {
            if (AdjacencyList.TryGetValue(edge.StartNode, out var value))
            {
                edge = value.FirstOrDefault(e => ((IUnweightedNormalEdge<T>)e).EndNode.Equals(edge.EndNode))!;
            }

            if (edge != null) AdjacencyList[edge.StartNode].Remove(edge);
        }

        public override DirectedGraph<T> Clone()
        {
            var result = new DirectedGraph<T>();

            foreach (var key in AdjacencyList.Keys)
            {
                var edgeList = AdjacencyList[key].ToList();
                result.AdjacencyList.Add(key, edgeList);
            }

            return result;
        }
    }
}