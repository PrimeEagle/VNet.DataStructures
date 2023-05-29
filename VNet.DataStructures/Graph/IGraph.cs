using VNet.DataStructures.Graph.Algorithms.Clique;
using VNet.DataStructures.Graph.Algorithms.Coloring;
using VNet.DataStructures.Graph.Algorithms.Connectivity;
using VNet.DataStructures.Graph.Algorithms.Matching;
using VNet.DataStructures.Graph.Algorithms.MaximumFlow;
using VNet.DataStructures.Graph.Algorithms.MinimumSpanningTree;
using VNet.DataStructures.Graph.Algorithms.Partitioning;
using VNet.DataStructures.Graph.Algorithms.PathFinding;
using VNet.DataStructures.Graph.Algorithms.Search;
using VNet.DataStructures.Graph.Algorithms.ShortestPath;
using VNet.DataStructures.Graph.Algorithms.TopologicalSorting;
using VNet.DataStructures.Graph.Algorithms.Transversal;
using VNet.DataStructures.Graph.Algorithms.Traversal;

namespace VNet.DataStructures.Graph
{
    public interface IGraph<TNode, TEdge, TValue> where TNode : notnull, INode<TValue>
                                                  where TEdge : notnull, IEdge<TNode, TValue>
                                                  where TValue : notnull, IComparable<TValue>
    {
        public bool IsStandardGraph { get; }
        public bool IsHyperGraph { get; }
        public bool IsLineGraph { get; }
        public bool IsMultiOrParallelGraph { get; }
        public bool IsDirected { get; }
        public bool IsWeighted { get; }
        public bool HasNegativeWeights { get; }
        public List<TEdge> this[TNode node] { get; set; }
        public IEnumerable<TNode> Nodes { get; }
        public IEnumerable<TEdge> Edges { get;  }
        public int Count { get; }
        public Dictionary<TNode, List<TEdge>> AdjacencyList { get; init; }



        public IGraph<TNode, TEdge, TValue> Clone();

        public void PerformTraversal(IGraphTraversalAlgorithm<TNode, TEdge, TValue> algorithm, IGraphTransversalAlgorithmArgs<TNode, TEdge, TValue> args);
        public void PerformTransversal(IGraphTransversalAlgorithm<TNode, TEdge, TValue> algorithm, IGraphTransversalAlgorithmArgs<TNode, TEdge, TValue> args);
        public void PerformTopologicalSort(IGraphTopologicalSortAlgorithm<TNode, TEdge, TValue> algorithm, IGraphTopologicalSortAlgorithmArgs<TNode, TEdge, TValue> args);
        public void PerformShortestPath(IGraphShortestPathAlgorithm<TNode, TEdge, TValue> algorithm, IGraphShortestPathAlgorithmArgs<TNode, TEdge, TValue> args);
        public void PerformSearch(IGraphSearchAlgorithm<TNode, TEdge, TValue> algorithm, IGraphSearchAlgorithmArgs<TNode, TEdge, TValue> args);
        public void PerformPathFinding(IGraphPathFindingAlgorithm<TNode, TEdge, TValue> algorithm, IGraphPathFindingAlgorithmArgs<TNode, TEdge, TValue> args);
        public void PerformPartition(IGraphPartitioningAlgorithm<TNode, TEdge, TValue> algorithm, IGraphPartitioningAlgorithmArgs<TNode, TEdge, TValue> args);
        public void PerformMinimumSpanningTree(IGraphMinimumSpanningTreeAlgorithm<TNode, TEdge, TValue> algorithm, IGraphMinimumSpanningTreeAlgorithmArgs<TNode, TEdge, TValue> args);
        public void PerformMaximumFlow(IGraphMaximumFlowAlgorithm<TNode, TEdge, TValue> algorithm, IGraphMaximumFlowAlgorithmArgs<TNode, TEdge, TValue> args);
        public void PerformMatch(IGraphMatchingAlgorithm<TNode, TEdge, TValue> algorithm, IGraphMatchingAlgorithmArgs<TNode, TEdge, TValue> args);
        public void PerformConnectivity(IGraphConnectivityAlgorithm<TNode, TEdge, TValue> algorithm, IGraphConnectivityAlgorithmArgs<TNode, TEdge, TValue> args);
        public void PerformColoring(IGraphColoringAlgorithm<TNode, TEdge, TValue> algorithm, IGraphColoringAlgorithmArgs<TNode, TEdge, TValue> args);
        public void PerformClique(IGraphCliqueAlgorithm<TNode, TEdge, TValue> algorithm, IGraphCliqueAlgorithmArgs<TNode, TEdge, TValue> args);
    }
}