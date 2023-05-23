namespace VNet.DataStructures.Graph
{
    public class DirectedUnweightedHyperGraph<INode<T>> : GraphBase<INode<T>> where T : notnull
    {
        public override void AddEdge(T startNode, T endNode, double weight)
        {
            if (AdjacencyList != null && AdjacencyList.ContainsKey(startNode) &&
                AdjacencyList[startNode].Any(e => e.EndNode.Equals(endNode)))
                throw new ArgumentException("Edge already exists in adjacency list.");

            var edge = new WeightedEdge<INode<T>>(startNode, endNode, weight);
            AddEdge(edge);
        }

        public override void AddEdge(T startNode, T endNode)
        {
            if (AdjacencyList != null && AdjacencyList.ContainsKey(startNode) &&
                AdjacencyList[startNode].Any(e => e.EndNode.Equals(endNode)))
                throw new ArgumentException("Edge already exists in adjacency list.");

            var edge = new Edge<INode<T>>(startNode, endNode);
            AddEdge(edge);
        }

        public override void AddEdge(Edge<INode<T>> edge)
        {
            if (!edge.IsDirected) throw new ArgumentException("Edge must be directed.");
            if (AdjacencyList is not null && AdjacencyList.ContainsKey(edge.StartNode) &&
                AdjacencyList[edge.StartNode].Contains(edge))
                throw new ArgumentException("Edge already exists in adjacency list.");

            base.AddVertex(edge.StartNode);
            base.AddVertex(edge.EndNode);
            AdjacencyList?[edge.StartNode].Add(edge);
        }

        public override void RemoveEdge(T startNode, T endNode)
        {
            var edge = AdjacencyList[startNode].Find(e => e.EndNode.Equals(endNode));

            if (edge != null) RemoveEdge(edge);
        }

        public override void RemoveEdge(Edge<INode<T>> edge)
        {
            AdjacencyList[edge.StartNode].Remove(edge);
        }

        public override DirectedGraph<INode<T>> Clone()
        {
            var result = new DirectedGraph<INode<T>>();

            foreach (var key in AdjacencyList.Keys)
            {
                var edgeList = AdjacencyList[key].ToList();
                result.AdjacencyList.Add(key, edgeList);
            }

            return result;
        }
    }
}