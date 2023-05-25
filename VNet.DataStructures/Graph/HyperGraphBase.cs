using VNet.DataStructures.Graph.SimpleGraph;

namespace VNet.DataStructures.Graph
{
    public abstract class HyperGraphBase<TNode, TEdge, TValue> : GraphBase<TNode, TEdge, TValue>, IHyperGraph<TNode, TEdge, TValue>
                                                                 where TNode : notnull, INode<TValue>
                                                                 where TEdge : notnull, IHyperEdge<TNode, TValue>
                                                                 where TValue : notnull
    {
        //public UndirectedUnweightedLineGraph<THyperEdgeNode, TOutputEdge, TValue> ToLineGraph<TOutputEdge>() where TOutputEdge : notnull, IUnweightedSimpleEdge<THyperEdgeNode, TValue>
        //{
        //    var lineGraph = new UndirectedUnweightedLineGraph<THyperEdgeNode, TOutputEdge, TValue>();

        //    // Add each hyperedge from the hypergraph as a node in the line graph.h
        //    foreach (var hyperEdge in AdjacencyList.Values.SelectMany(e => e))
        //    {
        //        lineGraph.AddNode(hyperEdge);
        //    }

        //    // Connect two node in the line graph if their corresponding hyperedges in the hypergraph share at least one vertex.
        //    foreach (var hyperEdge1 in lineGraph.AdjacencyList.Keys)
        //    {
        //        var h1Nodes = ((IHyperEdge<THyperEdgeNode, TValue>)hyperEdge1).StarTHyperEdgeNodes.Union(((IHyperEdge<THyperEdgeNode, TValue>)hyperEdge1).EndNodes).Distinct();
                
        //        foreach (var hyperEdge2 in lineGraph.AdjacencyList.Keys)
        //        {
        //            var h2Nodes = ((IHyperEdge<THyperEdgeNode, TValue>)hyperEdge2).StarTHyperEdgeNodes.Union(((IHyperEdge<THyperEdgeNode, TValue>)hyperEdge2).EndNodes).Distinct();

        //            if (!hyperEdge1.Equals(hyperEdge2) && h1Nodes.Any(vertex => h2Nodes.Contains(vertex)))
        //            {
        //                lineGraph.AddEdge(new LineEdge<IHyperEdge<THyperEdgeNode, TValue>, THyperEdgeNode, TValue>((IHyperEdge<THyperEdgeNode, TValue>)hyperEdge1, (IHyperEdge<THyperEdgeNode, TValue>)hyperEdge2));
        //            }
        //        }
        //    }

        //    return lineGraph;
        //}
    }
}