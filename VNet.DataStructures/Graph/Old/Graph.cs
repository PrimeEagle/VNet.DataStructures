namespace VNet.DataStructures.Graph.Old
{
    public class Graph
    {
        public int VertexCount
        {
            get
            {
                return Vertices.Count;
            }
        }

        public int EdgeCount
        {
            get
            {
                return Edges.Count;
            }
        }
        public IList<Edge> Edges { get; set; }
        public IList<Vertex> Vertices { get; set; }

        public Graph()
        {
            Edges = new List<Edge>();
            Vertices = new List<Vertex>();
        }

        public void AddVertex(char label)
        {
            Vertex vertex = new Vertex();
            vertex.Label = label;
            vertex.Visited = false;

            Vertices.Add(vertex);
        }

        public void AddEdge(int start, int end)
        {
            AddEdge(start, end, 1);
        }

        public void AddEdge(int start, int end, int weight, bool bidirectionial = false)
        {
            Edge edge = new Edge();
            edge.Source = start;
            edge.Destination = end;
            edge.Weight = weight;
            Edges.Add(edge);

            if (bidirectionial)
            {
                edge = new Edge();
                edge.Source = end;
                edge.Destination = start;
                edge.Weight = weight;
                Edges.Add(edge);
            }
        }
    }
}