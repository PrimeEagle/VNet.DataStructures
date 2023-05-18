using System.Diagnostics.CodeAnalysis;

namespace VNet.DataStructures.LinkedList
{
    public class DoubleLinkedListNode<T>
    {
        public T Data;
        [AllowNull]
        public DoubleLinkedListNode<T> Previous;
        [AllowNull]
        public DoubleLinkedListNode<T> Next;


        public DoubleLinkedListNode(T data)
        {
            Data = data;
        }
    }
}
