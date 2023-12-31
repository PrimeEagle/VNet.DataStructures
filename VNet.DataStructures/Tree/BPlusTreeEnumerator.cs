﻿using System.Collections;

namespace VNet.DataStructures.Tree
{
    internal class BplusTreeEnumerator<T> : IEnumerator<T> where T : IComparable
    {
        private readonly bool asc;
        private BPlusTreeNode<T> current;

        private int index;

        private BPlusTreeNode<T> startNode;

        internal BplusTreeEnumerator(BPlusTree<T> tree, bool asc = true)
        {
            this.asc = asc;

            startNode = asc ? tree.BottomLeftNode : tree.BottomRightNode;
            current = startNode;

            index = asc ? -1 : current.KeyCount;
        }

        public bool MoveNext()
        {
            if (current == null) return false;

            if (asc)
            {
                if (index + 1 < current.KeyCount)
                {
                    index++;
                    return true;
                }
            }
            else
            {
                if (index - 1 >= 0)
                {
                    index--;
                    return true;
                }
            }

            current = asc ? current.Next : current.Prev;

            var canMove = current != null && current.KeyCount > 0;
            if (canMove) index = asc ? 0 : current.KeyCount - 1;

            return canMove;
        }

        public void Reset()
        {
            current = startNode;
            index = asc ? -1 : current.KeyCount;
        }

        object IEnumerator.Current => Current;

        public T Current => current.Keys[index];

        public void Dispose()
        {
            current = null;
            startNode = null;
        }
    }
}