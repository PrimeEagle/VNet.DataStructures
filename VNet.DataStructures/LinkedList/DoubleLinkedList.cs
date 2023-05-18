using System.Collections;

namespace VNet.DataStructures.LinkedList
{
    public class DoublyLinkedList<T> : IEnumerable<T>
    {
        public DoubleLinkedListNode<T> Head;
        public DoubleLinkedListNode<T> Tail;

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new DoubleLinkedListEnumerator<T>(ref Head);
        }

        public DoubleLinkedListNode<T> InsertFirst(T data)
        {
            var newNode = new DoubleLinkedListNode<T>(data);

            if (Head != null) Head.Previous = newNode;

            newNode.Next = Head;
            newNode.Previous = null;

            Head = newNode;

            if (Tail == null) Tail = Head;

            return newNode;
        }

        internal void InsertFirst(DoubleLinkedListNode<T> newNode)
        {
            if (Head != null) Head.Previous = newNode;

            newNode.Next = Head;
            newNode.Previous = null;

            Head = newNode;

            if (Tail == null) Tail = Head;
        }

        public DoubleLinkedListNode<T> InsertAfter(DoubleLinkedListNode<T> node, DoubleLinkedListNode<T> data)
        {
            if (node == null)
                throw new ArgumentNullException("node is null.");

            if (node == Head && node == Tail)
            {
                node.Next = data;
                node.Previous = null;

                data.Previous = node;
                data.Next = null;

                Head = node;
                Tail = data;

                return data;
            }

            if (node != Tail)
            {
                data.Previous = node;
                data.Next = node.Next;

                node.Next.Previous = data;
                node.Next = data;
            }
            else
            {
                data.Previous = node;
                data.Next = null;

                node.Next = data;
                Tail = data;
            }

            return data;
        }

        public DoubleLinkedListNode<T> InsertBefore(DoubleLinkedListNode<T> node, DoubleLinkedListNode<T> data)
        {
            if (node == null)
                throw new ArgumentNullException("node is null.");

            if (node == Head && node == Tail)
            {
                node.Previous = data;
                node.Next = null;
                Tail = node;

                data.Previous = null;
                data.Next = node;

                Head = data;

                return data;
            }

            if (node == Head)
            {
                data.Previous = null;
                data.Next = node;

                node.Previous = data;
                Head = data;
            }
            else
            {
                data.Previous = node.Previous;
                data.Next = node;

                node.Previous.Next = data;
                node.Previous = data;
            }

            return data;
        }

        public DoubleLinkedListNode<T> InsertLast(T data)
        {
            if (Tail == null) return InsertFirst(data);

            var newNode = new DoubleLinkedListNode<T>(data);

            Tail.Next = newNode;

            newNode.Previous = Tail;
            newNode.Next = null;

            Tail = newNode;

            return newNode;
        }

        public T DeleteFirst()
        {
            if (Head == null) throw new NullReferenceException("Head is null.");

            var headData = Head.Data;

            if (Head == Tail)
            {
                Head = null;
                Tail = null;
            }
            else
            {
                Head.Next.Previous = null;
                Head = Head.Next;
            }

            return headData;
        }

        public T DeleteLast()
        {
            if (Tail == null) throw new NullReferenceException("Tail is null.");

            var tailData = Tail.Data;

            if (Tail == Head)
            {
                Head = null;
                Tail = null;
            }
            else
            {
                Tail.Previous.Next = null;
                Tail = Tail.Previous;
            }

            return tailData;
        }

        public void Delete(T data)
        {
            if (Head == null) throw new NullReferenceException("Head is null.");

            if (Head == Tail)
            {
                if (Head.Data.Equals(data)) DeleteFirst();

                return;
            }

            var current = Head;

            while (current != null)
            {
                if (current.Data.Equals(data))
                {
                    if (current.Previous == null)
                    {
                        current.Next.Previous = null;
                        Head = current.Next;
                    }
                    else if (current.Next == null)
                    {
                        current.Previous.Next = null;
                        Tail = current.Previous;
                    }
                    else
                    {
                        current.Previous.Next = current.Next;
                        current.Next.Previous = current.Previous;
                    }

                    break;
                }

                current = current.Next;
            }
        }

        public void Delete(DoubleLinkedListNode<T> node)
        {
            if (Head == null) throw new NullReferenceException("Head is null.");

            if (node == Head && node == Tail)
            {
                Head = null;
                Tail = null;
                return;
            }

            if (node == Head)
            {
                node.Next.Previous = null;
                Head = node.Next;
            }
            else if (node == Tail)
            {
                node.Previous.Next = null;
                Tail = node.Previous;
            }
            else
            {
                node.Previous.Next = node.Next;
                node.Next.Previous = node.Previous;
            }
        }

        internal void Union(DoublyLinkedList<T> newList)
        {
            if (Head == null)
            {
                Head = newList.Head;
                Tail = newList.Tail;
                return;
            }

            if (newList.Head == null)
                return;

            Head.Previous = newList.Tail;
            newList.Tail.Next = Head;

            Head = newList.Head;
        }

        public bool IsEmpty()
        {
            return Head == null;
        }

        public void Clear()
        {
            if (Head == null) throw new NullReferenceException("Head is null.");

            Head = null;
            Tail = null;
        }
    }
}