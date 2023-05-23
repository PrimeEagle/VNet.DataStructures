namespace VNet.DataStructures.Graph
{
    public class Path<INode<T>>
    {
        public List<IEdge<INode<T>>> Edges { get; private set; }

        public Path(List<IEdge<INode<T>>> edges)
        {
            Edges = edges;
        }

        public T Source
        {
            get { return Edges.First().Vertex1; }
        }

        public T Target
        {
            get { return Edges.Last().Vertex2; }
        }

        public int TotalWeight
        {
            get { return Edges.Sum(e => e.Weight); }
        }
    }
}
