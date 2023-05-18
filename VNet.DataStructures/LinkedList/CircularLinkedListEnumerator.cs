using System.Collections;

namespace VNet.DataStructures.LinkedList
{
    internal class CircularLinkedListEnumerator<T> : IEnumerator<T>
    {
        internal CircularLinkedListNode<T> CurrentNode;
        internal CircularLinkedListNode<T> ReferenceNode;

        internal CircularLinkedListEnumerator(ref CircularLinkedListNode<T> referenceNode)
        {
            this.ReferenceNode = referenceNode;
        }

        public bool MoveNext()
        {
            if (ReferenceNode == null)
                return false;

            if (CurrentNode == null)
            {
                CurrentNode = ReferenceNode;
                return true;
            }

            if (CurrentNode.Next != null && CurrentNode.Next != ReferenceNode)
            {
                CurrentNode = CurrentNode.Next;
                return true;
            }

            return false;
        }

        public void Reset()
        {
            CurrentNode = ReferenceNode;
        }


        object IEnumerator.Current => Current;

        public T Current => CurrentNode.Data;

        public void Dispose()
        {
            ReferenceNode = null;
            CurrentNode = null;
        }
    }
}
