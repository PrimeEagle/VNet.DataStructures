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
    public abstract class GraphBase<TNode, TEdge, TValue> : IGraph<TNode, TEdge, TValue>
                                                            where TNode : notnull, INode<TValue>
                                                            where TEdge : notnull, IEdge<TNode, TValue>
                                                            where TValue : notnull
    {
        List<TEdge> IGraph<TNode, TEdge, TValue>.this[TNode node]
        {
            get => AdjacencyList[node];
            set => AdjacencyList[node] = value;
        }
        public List<TEdge> this[TNode node] 
        {
            get => AdjacencyList[node];
            set => AdjacencyList[node] = value;
        }

        public virtual Dictionary<TNode, List<TEdge>> AdjacencyList { get; init; } = new();

        public virtual IGraph<TNode, TEdge, TValue> Clone()
        {
            var d1 = this.GetType();
            var typeArgs = new[] { typeof(TNode), typeof(TEdge), typeof(TValue) };
            var constructed = d1.MakeGenericType(typeArgs);
            var result = Activator.CreateInstance(constructed) as IGraph<TNode, TEdge, TValue>;

            foreach (var key in AdjacencyList.Keys)
            {
                var edgeList = AdjacencyList[key].ToList();
                result?.AdjacencyList.Add(key, edgeList);
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
        public virtual void PerformShortestPath(IGraphShortestPathAlgorithm<TNode, TEdge, TValue> algorithm, IGraphShortestPathAlgorithmArgs<TNode, TEdge, TValue> args)
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