namespace VNet.DataStructures.LinkedList
{
    public class CircularLinkedListNode<T>
    {
        public T Data;
        public CircularLinkedListNode<T> Next;
        public CircularLinkedListNode<T> Previous;

        public CircularLinkedListNode(T data)
        {
            Data = data;
        }
    }
}
