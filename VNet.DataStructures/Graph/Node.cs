namespace VNet.DataStructures.Graph
{
    public class Node<T> : INode<T> where T : notnull, new()
    {
        public T Value { get; init; }

        public Node(T value):base()
        {
            Value = value;
        }

        public Node<T> Clone()
        {
            return new Node<T>(Value);
        }
    }
}