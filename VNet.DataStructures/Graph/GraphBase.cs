using VNet.DataStructures.Graph.Algorithms.AllPairsShortestPath;
using VNet.DataStructures.Graph.Algorithms.Clique;
using VNet.DataStructures.Graph.Algorithms.Coloring;
using VNet.DataStructures.Graph.Algorithms.Connectivity;
using VNet.DataStructures.Graph.Algorithms.Matching;
using VNet.DataStructures.Graph.Algorithms.MaximumFlow;
using VNet.DataStructures.Graph.Algorithms.MinimumSpanningTree;
using VNet.DataStructures.Graph.Algorithms.Partitioning;
using VNet.DataStructures.Graph.Algorithms.PathFinding;
using VNet.DataStructures.Graph.Algorithms.Search;
using VNet.DataStructures.Graph.Algorithms.SingleSourceShortestPath;
using VNet.DataStructures.Graph.Algorithms.TopologicalSorting;
using VNet.DataStructures.Graph.Algorithms.Transversal;
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

        public virtual void PerformTraversal(IGraphTraversalAlgorithm<TNode, TEdge, TValue> algorithm, IGraphTransversalAlgorithmArgs<TNode, TEdge, TValue> args)
        {
            throw new NotImplementedException();
        }
        public virtual void PerformTransversal(IGraphTransversalAlgorithm<TNode, TEdge, TValue> algorithm, IGraphTransversalAlgorithmArgs<TNode, TEdge, TValue> args)
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