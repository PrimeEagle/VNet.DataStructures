﻿using System.Collections;

namespace VNet.DataStructures.Tree
{
    internal class BinarySearchTreeEnumerator<T> : IEnumerator<T> where T : IComparable
    {
        private readonly bool asc;

        private readonly BinarySearchTreeNodeBase<T> root;
        private BinarySearchTreeNodeBase<T> current;

        internal BinarySearchTreeEnumerator(BinarySearchTreeNodeBase<T> root, bool asc = true)
        {
            this.root = root;
            this.asc = asc;
        }

        public bool MoveNext()
        {
            if (root == null) return false;

            if (current == null)
            {
                current = asc ? root.FindMin() : root.FindMax();
                return true;
            }

            var next = asc ? current.NextHigher() : current.NextLower();
            if (next != null)
            {
                current = next;
                return true;
            }

            return false;
        }

        public void Reset()
        {
            current = root;
        }

        public T Current => current.Value;

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            current = null;
        }
    }
}
