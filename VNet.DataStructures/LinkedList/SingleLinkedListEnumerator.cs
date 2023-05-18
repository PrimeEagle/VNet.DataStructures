using System.Collections;

namespace VNet.DataStructures.LinkedList
{
    internal class SingleLinkedListEnumerator<T> : IEnumerator<T>
    {
        internal SingleLinkedListNode<T> CurrentNode;
        internal SingleLinkedListNode<T> HeadNode;

        internal SingleLinkedListEnumerator(ref SingleLinkedListNode<T> headNode)
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
