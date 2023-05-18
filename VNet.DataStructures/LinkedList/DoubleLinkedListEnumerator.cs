using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace VNet.DataStructures.LinkedList
{
    internal class DoubleLinkedListEnumerator<T> : IEnumerator<T>
    {
        [AllowNull]
        internal DoubleLinkedListNode<T> CurrentNode;
        internal DoubleLinkedListNode<T> HeadNode;

        internal DoubleLinkedListEnumerator(ref DoubleLinkedListNode<T> headNode)
        {
            this.HeadNode = headNode;
        }

        public bool MoveNext()
        {
            if (HeadNode == null)
                return false;

            if (CurrentNode == null)
            {
                CurrentNode = HeadNode;
                return true;
            }

            if (CurrentNode.Next != null)
            {
                CurrentNode = CurrentNode.Next;
                return true;
            }

            return false;
        }

        public void Reset()
        {
            CurrentNode = HeadNode;
        }


        object IEnumerator.Current => Current;

        public T Current => CurrentNode.Data;

        public void Dispose()
        {
            HeadNode = null;
            CurrentNode = null;
        }
    }
}
