namespace VNet.DataStructures.Graph.Algorithms.PathFinding
{
    public interface IPathfindingAlgorithm
    {
        public int[] Find(Basic.Graph graph, int source = 0);
    }
}
