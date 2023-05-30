// ReSharper disable MemberCanBePrivate.Global
namespace VNet.DataStructures.Graph.Algorithms.AllPairsShortestPath
{
    public class AllPairsResult<TNode, TValue> where TNode : notnull, INode<TValue>
                                               where TValue : notnull, IComparable<TValue>
    {
        public double[,] WeightMatrix { get; }
        public TNode[,] NextMatrix { get; }
        public List<TNode> Nodes { get; }


        public AllPairsResult()
        {
            WeightMatrix = new double[,]{};
            NextMatrix = new TNode[,]{};
            Nodes = new List<TNode>();
        }

        public AllPairsResult(double[,] distanceMatrix, TNode[,] nextMatrix, IList<TNode> nodes)
        {
            WeightMatrix = distanceMatrix;
            NextMatrix = nextMatrix;
            Nodes = (List<TNode>)nodes;
        }

        public Path<TNode, TValue> ShortestPath(TNode startNode, TNode endNode)
        {
            if (startNode == null || endNode == null)
            {
                throw new ArgumentNullException(nameof(startNode), "Start or end node is null.");
            }

            var startIdx = Nodes.IndexOf(startNode);
            var endIdx = Nodes.IndexOf(endNode);

            if (startIdx == -1 || endIdx == -1)
            {
                throw new ArgumentException("Start or end node is not in the graph.");
            }

            if (double.IsPositiveInfinity(WeightMatrix[startIdx, endIdx]))
            {
                return new Path<TNode, TValue>();
            }

            var allPairsShortestPaths = new AllPairsResult<TNode, TValue>();

            var path = new List<TNode> { startNode };

            while (!startNode.Equals(endNode))
            {
                startIdx = Nodes.IndexOf(startNode);
                startNode = NextMatrix[startIdx, endIdx];
                path.Add(startNode);
            }

            return new Path<TNode, TValue>(path);
        }
    }
}