﻿using VNet.DataStructures.Graph.Algorithms.AllPairsShortestPath;
using VNet.DataStructures.Graph.Algorithms.Clique;
using VNet.DataStructures.Graph.Algorithms.Coloring;
using VNet.DataStructures.Graph.Algorithms.Connectivity;
using VNet.DataStructures.Graph.Algorithms.Matching;
using VNet.DataStructures.Graph.Algorithms.MaximumFlow;
using VNet.DataStructures.Graph.Algorithms.MinimumSpanningTree;
using VNet.DataStructures.Graph.Algorithms.Optimization;
using VNet.DataStructures.Graph.Algorithms.Partitioning;
using VNet.DataStructures.Graph.Algorithms.PathFinding;
using VNet.DataStructures.Graph.Algorithms.Search;
using VNet.DataStructures.Graph.Algorithms.SingleSourceShortestPath;
using VNet.DataStructures.Graph.Algorithms.TopologicalSorting;
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
        public IList<TNode> Nodes { get; }
        public IList<TEdge> Edges { get;  }
        public int Count { get; }
        public Dictionary<TNode, List<TEdge>> AdjacencyList { get; init; }
        public double GetEdgeWeight(TNode startNode, TNode endNode);

        public void Validate(IGraphValidationArgs args);

        public IGraph<TNode, TEdge, TValue> Clone(bool deep = false);

        public void PerformTraversal(IGraphTraversalAlgorithm<TNode, TEdge, TValue> algorithm, IGraphTraversalAlgorithmArgs<TNode, TEdge, TValue> args);
        public void PerformOptimization<TProblem, TSolution>(IGraphOptimizationAlgorithm<TNode, TEdge, TValue, TSolution> algorithm, IGraphOptimizationAlgorithmArgs<TNode, TEdge, TValue> args) where TSolution : notnull;
        public void PerformTopologicalSort(IGraphTopologicalSortAlgorithm<TNode, TEdge, TValue> algorithm, IGraphTopologicalSortAlgorithmArgs<TNode, TEdge, TValue> args);
        public void PerformSingleSourceShortestPath(IGraphSingleSourceShortestPathAlgorithm<TNode, TEdge, TValue> algorithm, IGraphSingleSourceShortestPathAlgorithmArgs<TNode, TEdge, TValue> args);
        public void PerformAllPairsShortestPath(IGraphAllPairsShortestPathAlgorithm<TNode, TEdge, TValue> algorithm, IGraphAllPairsShortestPathAlgorithmArgs<TNode, TEdge, TValue> args);
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