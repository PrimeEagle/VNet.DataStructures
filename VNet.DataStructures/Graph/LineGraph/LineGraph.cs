// ReSharper disable MemberCanBePrivate.Global
namespace VNet.DataStructures.Graph.LineGraph
{
    public class LineGraph<TLineNode, TLineEdge, TNode, TValue> : LineGraphBase<TLineNode, TLineEdge, TNode, TValue>
                                                                  where TLineNode : notnull, IHyperEdge<TNode, TValue>
                                                                  where TLineEdge : notnull, ILineEdge<TLineNode, TNode, TValue>
                                                                  where TNode : notnull, INode<TValue>
                                                                  where TValue : notnull
    {
        public bool IsDirected => true;
        public bool IsWeighted => false;


        public override Dictionary<TLineNode, List<TLineEdge>> AdjacencyList { get; init; } = new();

        public void AddNode(TLineNode node)
        {
            if (!AdjacencyList.ContainsKey(node)) AdjacencyList.Add(node, new List<TLineEdge>());
        }

        public void AddEdge(TLineNode startNode, TLineNode endNode)
        {
            var edge = (TLineEdge)(ILineEdge<TLineNode,TNode, TValue>)new LineEdge<TLineNode, TNode, TValue>(startNode, endNode);
            if (edge == null) throw new ArgumentNullException(nameof(edge));
            AddEdge(edge);
        }

        public void AddEdge(TLineEdge edge)
        {
            var startNode = (TLineNode)edge.StartEdge;

            if (AdjacencyList.Values.Any(edgeList => edgeList.Any(e => (e.StartEdge.Equals(edge.StartEdge) && e.EndEdge.Equals(edge.EndEdge)) ||
                                                                                                (e.StartEdge.Equals(edge.EndEdge) && e.EndEdge.Equals(edge.StartEdge)))))
            {
                throw new ArgumentException("Line graphs can only have one edge between nodes.");
            }

            if (!AdjacencyList.ContainsKey(startNode)) AdjacencyList.Add(startNode, new List<TLineEdge>());
            AdjacencyList[startNode].Add(edge);
        }

        public void RemoveNode(TLineNode node)
        {
            AdjacencyList.Remove(node);

            foreach (var edgeList in AdjacencyList.Values)
            {
                edgeList.RemoveAll(e => e.StartEdge.Equals(node) || e.EndEdge.Equals(node));
            }
        }

        public void RemoveEdge(TLineNode startEdge, TLineNode endEdge)
        {
            AdjacencyList[startEdge].RemoveAll(e => e.EndEdge.Equals(endEdge));
            AdjacencyList[endEdge].RemoveAll(e => e.EndEdge.Equals(startEdge));
        }

        public void RemoveEdge(TLineEdge edge)
        {
            var startNode = (TLineNode)edge.StartEdge;
            var endNode = (TLineNode)edge.EndEdge;

            AdjacencyList[startNode].Remove(edge);
            AdjacencyList[endNode].Remove((TLineEdge)edge.Reverse());
        }
    }
}