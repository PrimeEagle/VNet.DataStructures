namespace VNet.DataStructures.Algorithms.Pathfinding
{
    public interface IPathfindingAlgorithm
    {
        public int[] Find(Graph.Graph graph, int source = 0);
    }
}
