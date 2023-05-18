using System.Collections;

namespace VNet.DataStructures.LinkedList
{
    public class SingleLinkedList<T> : IEnumerable<T>
    {
        public SingleLinkedListNode<T> Head;

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new SingleLinkedListEnumerator<T>(ref Head);
        }

        public void InsertFirst(T data)
        {
            var newNode = new SingleLinkedListNode<T>(data);

            newNode.Next = Head;

            Head = newNode;
        }

        public void InsertLast(T data)
        {
            var newNode = new SingleLinkedListNode<T>(data);

            if (Head == null)
            {
                Head = new SingleLinkedListNode<T>(data);
            }
            else
            {
                var current = Head;

                while (current.Next != null) current = current.Next;

                current.Next = newNode;
            }
        }

        public T DeleteFirst()
        {
            if (Head == null) throw new NullReferenceException("Head is null.");

            var firstData = Head.Data;

            Head = Head.Next;

            return firstData;
        }

        public T DeleteLast()
        {
            if (Head == null) throw new NullReferenceException("Head is null.");

            var current = Head;
            SingleLinkedListNode<T> prev = null;
            while (current.Next != null)
            {
                prev = current;
                current = current.Next;
            }

            var lastData = prev.Next.Data;
            prev.Next = null;
            return lastData;
        }

        public void Delete(T element)
        {
            if (Head == null) throw new NullReferenceException("Head is null.");

            var current = Head;
            SingleLinkedListNode<T> prev = null;

            do
            {
                if (current.Data.Equals(element))
                {
                    if (current.Next == null)
                    {
                        if (prev == null)
                            Head = null;
                        else
                            prev.Next = null;
                    }
                    else
                    {
                        if (prev == null)
                            Head = current.Next;
                        else
                            prev.Next = current.Next;
                    }

                    break;
                }

                prev = current;
                current = current.Next;
            } while (current != null);
        }

        public bool IsEmpty()
        {
            return Head == null;
        }

        public void Clear()
        {
            Head = null;
        }

        public void InsertFirst(SingleLinkedListNode<T> current)
        {
            current.Next = Head;
            Head = current;
        }
    }
}