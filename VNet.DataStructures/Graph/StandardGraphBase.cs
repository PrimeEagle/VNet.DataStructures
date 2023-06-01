namespace VNet.DataStructures.Graph
{
    public abstract class StandardGraphBase<TNode, TEdge, TValue> : GraphBase<TNode, TEdge, TValue>, IStandardGraph<TNode, TEdge, TValue>
                                                                    where TNode : notnull, INode<TValue>
                                                                    where TEdge : notnull, IStandardEdge<TNode, TValue>
                                                                    where TValue : notnull, IComparable<TValue>
    {
        public TEdge? GetEdge(TNode startNode, TNode endNode)
        {
            return AdjacencyList[startNode].FirstOrDefault(n => n.EndNode.Equals(endNode));
        }

        public double[,] GetWeightMatrix()
        {
            if (!IsWeighted) throw new NotSupportedException("The GetWeightMatrix method is only available for weighted standard graphs.");

            var nodeCount = Nodes.Count;
            var distances = new double[nodeCount, nodeCount];

            for (var i = 0; i < nodeCount; i++)
            {
                for (var j = 0; j < nodeCount; j++)
                {
                    if (i == j)
                    {
                        distances[i, j] = 0; // Distance from a node to itself is 0
                        continue;
                    }

                    var startNode = Nodes[i];
                    var endNode = Nodes[j];

                    var edge = AdjacencyList[startNode].FirstOrDefault(e => e.EndNode.Equals(endNode));
                    distances[i, j] = edge != null ? ((IWeightedStandardEdge<TNode, TValue>)edge).Weight : double.PositiveInfinity;
                }
            }

            return distances;
        }

        public override bool HasNegativeWeights
        {
            get
            {
                return IsWeighted && Edges.Any(e => (e.GetType() == typeof(IWeightedStandardEdge<TNode, TValue>) && ((IWeightedStandardEdge<TNode, TValue>)e).Weight < 0));
            }
        }
    }
}