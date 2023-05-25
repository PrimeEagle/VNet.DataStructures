﻿//// ReSharper disable MemberCanBePrivate.Global
//namespace VNet.DataStructures.Graph.LineGraph
//{
//    public class UndirectedUnweightedLineGraph<TNode, TEdge, TValue> : LineGraphBase<TNode, TEdge, TValue>
//                                                                       where TNode : notnull, IUnweightedHyperEdge<TNode, TValue>
//                                                                       where TEdge : notnull, IUnweightedLineEdge<TNode, TValue>
//                                                                       where TValue : notnull
//    {
//        public override Dictionary<TLineNode, List<TLineEdge>> AdjacencyList { get; init; } = new();

//        public void AddNode(TNode node)
//        {
//            if (!AdjacencyList.ContainsKey(node)) AdjacencyList.Add(node, new List<TEdge>());
//        }

//        public void AddEdge(TNode startNode, TNode endNode)
//        {
//            var edge = (TEdge)(IUnweightedSimpleEdge<TNode, TValue>)new UnweightedSimpleEdge<TNode, TValue>(startNode, endNode, false);
//            if (edge == null) throw new ArgumentNullException(nameof(edge));
//            AddEdge(edge);
//        }

//        public void AddEdge(TEdge edge)
//        {
//            var startNode = (TNode)edge.StartNode;
//            var endNode = (TNode)edge.EndNode;

//            if (AdjacencyList.Values.Any(edgeList => edgeList.Any(e => (e.StartNode.Equals(edge.StartNode) && e.EndNode.Equals(edge.EndNode)) ||
//                                                                                                (e.StartNode.Equals(edge.EndNode) && e.EndNode.Equals(edge.StartNode)))))
//            {
//                throw new ArgumentException("Line graphs can only have one edge between nodes.");
//            }

//            if (!AdjacencyList.ContainsKey(startNode)) AdjacencyList.Add(startNode, new List<TEdge>());
//            if (!AdjacencyList.ContainsKey(endNode)) AdjacencyList.Add(endNode, new List<TEdge>());
//            AdjacencyList[startNode].Add(edge);
//            AdjacencyList[endNode].Add((TEdge)edge.Reverse());
//        }

//        public void RemoveNode(TNode node)
//        {
//            AdjacencyList.Remove(node);

//            foreach (var edgeList in AdjacencyList.Values)
//            {
//                edgeList.RemoveAll(e => e.StartNode.Equals(node) || e.EndNode.Equals(node));
//            }
//        }

//        public void RemoveEdge(TNode startNode, TNode endNode)
//        {
//            AdjacencyList[startNode].RemoveAll(e => e.EndNode.Equals(endNode));
//            AdjacencyList[endNode].RemoveAll(e => e.EndNode.Equals(startNode));
//        }

//        public void RemoveEdge(TEdge edge)
//        {
//            var startNode = (TNode)edge.StartNode;
//            var endNode = (TNode)edge.EndNode;

//            AdjacencyList[startNode].Remove(edge);
//            AdjacencyList[endNode].Remove((TEdge)edge.Reverse());
//        }
//    }
//}