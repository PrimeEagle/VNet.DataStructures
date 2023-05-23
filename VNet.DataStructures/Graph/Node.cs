namespace VNet.DataStructures.Graph
{
    public class Node<T> : INode<T> where T : notnull
    {
        public T Value { get; init; }

        public Node(T value):base()
        {
            Value = value;
        }
    }
}