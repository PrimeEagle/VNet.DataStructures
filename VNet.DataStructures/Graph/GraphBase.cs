using VNet.DataStructures.Graph.Algorithms.AllPairsShortestPath;
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
using VNet.Utility.Extensions;

namespace VNet.DataStructures.Graph
{
    public abstract class GraphBase<TNode, TEdge, TValue> : IGraph<TNode, TEdge, TValue>
                                                            where TNode : notnull, INode<TValue>
                                                            where TEdge : notnull, IEdge<TNode, TValue>
                                                            where TValue : notnull, IComparable<TValue>
    {
        public abstract bool IsStandardGraph { get; }
        public abstract bool IsHyperGraph { get; }
        public abstract bool IsLineGraph { get; }
        public abstract bool IsMultiOrParallelGraph { get; }
        public abstract bool IsDirected { get; }
        public abstract bool IsWeighted { get; }
        public abstract bool HasNegativeWeights { get; }

        public void Validate(IGraphValidationArgs args)
        {
            if(AdjacencyList.Count == 0) throw new InvalidOperationException("Graph has no nodes or edges.");
            if(!AdjacencyList.Values.SelectMany(e => e.Select(v => v)).Any()) throw new InvalidOperationException("Graph has no edges.");
            if (args.MustBeStandardGraph && !IsStandardGraph) throw new InvalidOperationException("Graph must be a standard graph.");
            if (args.MustBeHyperGraph && !IsHyperGraph) throw new InvalidOperationException("Graph must be a hyper graph.");
            if (args.MustBeLineGraph && !IsLineGraph) throw new InvalidOperationException("Graph must be a line graph.");
            if (args.MustBeMultiOrParallelGraph && !IsMultiOrParallelGraph) throw new InvalidOperationException("Graph must be a multi graph or a parallel graph.");
            if (args.MustBeDirected && !IsDirected) throw new InvalidOperationException("Graph must be a directed graph.");
            if (args.MustBeWeighted && !IsWeighted) throw new InvalidOperationException("Graph must be weighted.");
            if (args.MustHaveNegativeWeights && !HasNegativeWeights) throw new InvalidOperationException("Graph must have negative weights.");
            if (args.CannotBeStandardGraph && IsStandardGraph) throw new InvalidOperationException("Graph cannot be a standard graph.");
            if (args.CannotBeHyperGraph && IsHyperGraph) throw new InvalidOperationException("Graph cannot be a hyper graph.");
            if (args.CannotBeLineGraph && IsLineGraph) throw new InvalidOperationException("Graph cannot be a line graph.");
            if (args.CannotBeMultiOrParallelGraph && IsMultiOrParallelGraph) throw new InvalidOperationException("Graph cannot be a multi graph or a parallel graph.");
            if (args.CannotBeDirected && IsDirected) throw new InvalidOperationException("Graph cannot be a directed graph.");
            if (args.CannotBeWeighted && IsWeighted) throw new InvalidOperationException("Graph cannot be weighted.");
            if (args.CannotHaveNegativeWeights && HasNegativeWeights) throw new InvalidOperationException("Graph cannot have negative weights.");
        }

        public int Count => AdjacencyList.Count;

        List<TEdge> IGraph<TNode, TEdge, TValue>.this[TNode node]
        {
            get => AdjacencyList[node];
            set => AdjacencyList[node] = value;
        }

        public IList<TNode> Nodes => AdjacencyList.Keys.ToList();
        public IList<TEdge> Edges => AdjacencyList.Values.SelectMany(e => e.Select(x => x)).ToList();
        public List<TEdge> this[TNode node] 
        {
            get => AdjacencyList[node];
            set => AdjacencyList[node] = value;
        }

        public virtual Dictionary<TNode, List<TEdge>> AdjacencyList { get; init; } = new();

        public virtual IGraph<TNode, TEdge, TValue> Clone(bool deep = false)
        {
            var d1 = this.GetType();
            var typeArgs = new[] { typeof(TNode), typeof(TEdge), typeof(TValue) };
            var constructed = d1.MakeGenericType(typeArgs);
            var result = Activator.CreateInstance(constructed) as IGraph<TNode, TEdge, TValue>;

            foreach (var key in AdjacencyList.Keys)
            {
                var newKey = deep ? key.Clone() : key;

                var edgeList = AdjacencyList[key].ToList();
                var newEdgeList = AdjacencyList[key].Select(edge => deep ? (TEdge)edge.Clone(deep) : edge).ToList<TEdge>();

                result?.AdjacencyList.Add(newKey, newEdgeList);
            }

            return result ?? throw new InvalidOperationException();
        }
          
        public double GetEdgeWeight(TNode startNode, TNode endNode)
        {
            if (!IsWeighted) return 1.0d;

            var edge = AdjacencyList[startNode].Where(e => ((IStandardEdge<TNode, TValue>)e).EndNode.Equals(endNode)).FirstOrDefault();
            if (edge is null && !IsDirected)
            {
                edge = AdjacencyList[endNode].Where(e => ((IStandardEdge<TNode, TValue>)e).EndNode.Equals(startNode)).FirstOrDefault();
            }

            if (edge is null) throw new ArgumentException("Edge not found.");

            return ((IWeightedStandardEdge<TNode, TValue>)edge).Weight;
        }

        public virtual void PerformTraversal(IGraphTraversalAlgorithm<TNode, TEdge, TValue> algorithm, IGraphTraversalAlgorithmArgs<TNode, TEdge, TValue> args)
        {
            throw new NotImplementedException();
        }

        public void PerformOptimization<TProblem, TSolution>(IGraphOptimizationAlgorithm<TNode, TEdge, TValue, TSolution> algorithm, IGraphOptimizationAlgorithmArgs<TNode, TEdge, TValue> args) where TSolution : notnull
        {
            throw new NotImplementedException();
        }
        public virtual void PerformTopologicalSort(IGraphTopologicalSortAlgorithm<TNode, TEdge, TValue> algorithm, IGraphTopologicalSortAlgorithmArgs<TNode, TEdge, TValue> args)
        {
            throw new NotImplementedException();
        }
        public virtual void PerformSingleSourceShortestPath(IGraphSingleSourceShortestPathAlgorithm<TNode, TEdge, TValue> algorithm, IGraphSingleSourceShortestPathAlgorithmArgs<TNode, TEdge, TValue> args)
        {
            throw new NotImplementedException();
        }
        public virtual void PerformAllPairsShortestPath(IGraphAllPairsShortestPathAlgorithm<TNode, TEdge, TValue> algorithm, IGraphAllPairsShortestPathAlgorithmArgs<TNode, TEdge, TValue> args)
        {
            throw new NotImplementedException();
        }
        public virtual void PerformSearch(IGraphSearchAlgorithm<TNode, TEdge, TValue> algorithm, IGraphSearchAlgorithmArgs<TNode, TEdge, TValue> args)
        {
            throw new NotImplementedException();
        }
        public virtual void PerformPathFinding(IGraphPathFindingAlgorithm<TNode, TEdge, TValue> algorithm, IGraphPathFindingAlgorithmArgs<TNode, TEdge, TValue> args)
        {
            throw new NotImplementedException();
        }
        public virtual void PerformPartition(IGraphPartitioningAlgorithm<TNode, TEdge, TValue> algorithm, IGraphPartitioningAlgorithmArgs<TNode, TEdge, TValue> args)
        {
            throw new NotImplementedException();
        }
        public virtual void PerformMinimumSpanningTree(IGraphMinimumSpanningTreeAlgorithm<TNode, TEdge, TValue> algorithm, IGraphMinimumSpanningTreeAlgorithmArgs<TNode, TEdge, TValue> args)
        {
            throw new NotImplementedException();
        }
        public virtual void PerformMaximumFlow(IGraphMaximumFlowAlgorithm<TNode, TEdge, TValue> algorithm, IGraphMaximumFlowAlgorithmArgs<TNode, TEdge, TValue> args)
        {
            throw new NotImplementedException();
        }
        public virtual void PerformMatch(IGraphMatchingAlgorithm<TNode, TEdge, TValue> algorithm, IGraphMatchingAlgorithmArgs<TNode, TEdge, TValue> args)
        {
            throw new NotImplementedException();
        }
        public virtual void PerformConnectivity(IGraphConnectivityAlgorithm<TNode, TEdge, TValue> algorithm, IGraphConnectivityAlgorithmArgs<TNode, TEdge, TValue> args)
        {
            throw new NotImplementedException();
        }
        public virtual void PerformColoring(IGraphColoringAlgorithm<TNode, TEdge, TValue> algorithm, IGraphColoringAlgorithmArgs<TNode, TEdge, TValue> args)
        {
            throw new NotImplementedException();
        }
        public virtual void PerformClique(IGraphCliqueAlgorithm<TNode, TEdge, TValue> algorithm, IGraphCliqueAlgorithmArgs<TNode, TEdge, TValue> args)
        {
            throw new NotImplementedException();
        }
    }
}