using VNet.DataStructures.Graph.Algorithms.Connectivity;
using VNet.DataStructures.Graph.Algorithms.Traversal;
using VNet.DataStructures.Graph.LineGraph;

namespace VNet.DataStructures.Graph
{
    public abstract class HyperGraphBase<TNode, TEdge, TValue> : GraphBase<TNode, TEdge, TValue>, IHyperGraph<TNode, TEdge, TValue>
                                                                 where TNode : notnull, INode<TValue>
                                                                 where TEdge : notnull, IHyperEdge<TNode, TValue>
                                                                 where TValue : notnull, IComparable<TValue>
    {
        public LineGraph<TEdge, ILineEdge<TEdge, TNode, TValue>, TNode, TValue> ToLineGraph()
        {
            var lineGraph = new LineGraph<TEdge, ILineEdge<TEdge, TNode, TValue>, TNode, TValue>();

            // Add each hyperedge from the hypergraph as a node in the line graph.h
            foreach (var hyperEdge in AdjacencyList.Values.SelectMany(e => e))
            {
                lineGraph.AddNode(hyperEdge);
            }

            // Connect two node in the line graph if their corresponding hyperedges in the hypergraph share at least one vertex.
            foreach (var hyperEdge1 in lineGraph.AdjacencyList.Keys)
            {
                var h1Nodes = ((IHyperEdge<TNode, TValue>)hyperEdge1).StartNodes.Union(((IHyperEdge<TNode, TValue>)hyperEdge1).EndNodes).Distinct();
                
                foreach (var hyperEdge2 in lineGraph.AdjacencyList.Keys)
                {
                    var h2Nodes = ((IHyperEdge<TNode, TValue>)hyperEdge2).StartNodes.Union(((IHyperEdge<TNode, TValue>)hyperEdge2).EndNodes).Distinct();

                    if (!hyperEdge1.Equals(hyperEdge2) && h1Nodes.Any(vertex => h2Nodes.Contains(vertex)))
                    {
                        lineGraph.AddEdge(new LineEdge<TEdge, TNode, TValue>(hyperEdge1, hyperEdge2));
                    }
                }
            }

            return lineGraph;
        }
    }
}