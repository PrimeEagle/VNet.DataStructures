namespace VNet.DataStructures.Graph
{
    public class Edge : IEdge
    {
        public bool Directed { get; init; }


        public Edge(bool directed)
        {
            Directed = directed;
        }
    }
}