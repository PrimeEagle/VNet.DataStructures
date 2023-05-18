using System.Diagnostics.CodeAnalysis;

namespace VNet.DataStructures.LinkedList
{
    public class SingleLinkedListNode<T>
    {
        public T Data;
        [AllowNull]
        public SingleLinkedListNode<T> Next;


        public SingleLinkedListNode(T data)
        {
            Data = data;
        }
    }
}
