using System.Collections;

namespace VNet.DataStructures.LinkedList
{
    public class CircularLinkedList<T> : IEnumerable<T>
    {
        public CircularLinkedListNode<T> ReferenceNode;

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new CircularLinkedListEnumerator<T>(ref ReferenceNode);
        }

        public CircularLinkedListNode<T> Insert(T data)
        {
            var newNode = new CircularLinkedListNode<T>(data);

            if (ReferenceNode == null)
            {
                newNode.Next = newNode;
                newNode.Previous = newNode;
            }
            else
            {
                newNode.Previous = ReferenceNode;
                newNode.Next = ReferenceNode.Next;

                ReferenceNode.Next.Previous = newNode;
                ReferenceNode.Next = newNode;
            }

            ReferenceNode = newNode;

            return newNode;
        }

        public void Delete(CircularLinkedListNode<T> current)
        {
            if (ReferenceNode.Next == ReferenceNode)
            {
                if (ReferenceNode != current) throw new NullReferenceException("ReferenceNode is null.");

                ReferenceNode = null;
                return;
            }

            current.Previous.Next = current.Next;
            current.Next.Previous = current.Previous;

            if (current == ReferenceNode) ReferenceNode = current.Next;
        }

        public void Delete(T data)
        {
            if (ReferenceNode.Next == ReferenceNode)
            {
                if (ReferenceNode.Data.Equals(data))
                {
                    ReferenceNode = null;
                    return;
                }

                throw new NullReferenceException("Not found.");
            }

            var current = ReferenceNode;
            var found = false;
            while (true)
            {
                if (current.Data.Equals(data))
                {
                    current.Previous.Next = current.Next;
                    current.Next.Previous = current.Previous;

                    if (current == ReferenceNode) ReferenceNode = current.Next;

                    found = true;
                    break;
                }

                if (current.Next == ReferenceNode) break;

                current = current.Next;
            }

            if (found == false) throw new NullReferenceException("Not found.");
        }

        public bool IsEmpty()
        {
            return ReferenceNode == null;
        }

        public void Clear()
        {
            ReferenceNode = null;
        }

        public void Union(CircularLinkedList<T> newList)
        {
            ReferenceNode.Previous.Next = newList.ReferenceNode;
            ReferenceNode.Previous = newList.ReferenceNode.Previous;

            newList.ReferenceNode.Previous.Next = ReferenceNode;
            newList.ReferenceNode.Previous = ReferenceNode.Previous;
        }
    }  
}
