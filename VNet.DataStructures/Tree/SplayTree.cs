﻿using System.Collections;

namespace VNet.DataStructures.Tree
{
    public class SplayTree<T> : IEnumerable<T> where T : IComparable
    {
        public SplayTree()
        {
        }

        public SplayTree(IEnumerable<T> sortedCollection) : this()
        {
            Util.ValidateSortedCollection(sortedCollection);
            var nodes = sortedCollection.Select(x => new SplayTreeNode<T>(null, x)).ToArray();
            Root = (SplayTreeNode<T>)Util.ToBst(nodes);
            Util.AssignCount(Root);
        }

        internal SplayTreeNode<T> Root { get; set; }
        public int Count => Root == null ? 0 : Root.Count;

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new BinarySearchTreeEnumerator<T>(Root);
        }

        public bool HasItem(T value)
        {
            if (Root == null) return false;

            return Find(Root, value) != null;
        }

        internal int GetHeight()
        {
            return GetHeight(Root);
        }

        private int GetHeight(SplayTreeNode<T> node)
        {
            if (node == null) return -1;

            return Math.Max(GetHeight(node.Left), GetHeight(node.Right)) + 1;
        }

        public void Insert(T value)
        {
            if (Root == null)
            {
                Root = new SplayTreeNode<T>(null, value);
                return;
            }

            var newNode = Insert(Root, value);
            Splay(newNode);
        }

        private SplayTreeNode<T> Insert(SplayTreeNode<T> currentNode, T newNodeValue)
        {
            while (true)
            {
                var compareResult = currentNode.Value.CompareTo(newNodeValue);

                if (compareResult < 0)
                {
                    if (currentNode.Right == null)
                    {
                        currentNode.Right = new SplayTreeNode<T>(currentNode, newNodeValue);
                        return currentNode.Right;
                    }

                    currentNode = currentNode.Right;
                }
                else if (compareResult > 0)
                {
                    if (currentNode.Left == null)
                    {
                        currentNode.Left = new SplayTreeNode<T>(currentNode, newNodeValue);
                        return currentNode.Left;
                    }

                    currentNode = currentNode.Left;
                }
                else
                {
                    throw new Exception("Item exists");
                }
            }
        }

        public int IndexOf(T item)
        {
            return Root.Position(item);
        }

        public T ElementAt(int index)
        {
            if (index < 0 || index >= Count) throw new ArgumentNullException("index");

            return Root.KthSmallest(index).Value;
        }

        public void Delete(T value)
        {
            if (Root == null) throw new Exception("Empty SplayTree");

            Delete(Root, value);
        }

        public T RemoveAt(int index)
        {
            if (index < 0 || index >= Count) throw new ArgumentOutOfRangeException("index");

            var nodeToDelete = Root.KthSmallest(index) as SplayTreeNode<T>;

            Delete(nodeToDelete, nodeToDelete.Value);

            return nodeToDelete.Value;
        }

        private void Delete(SplayTreeNode<T> node, T value)
        {
            while (true)
            {
                var compareResult = node.Value.CompareTo(value);

                if (compareResult < 0)
                {
                    node = node.Right ?? throw new ArgumentNullException("Item does not exist.");
                    continue;
                }

                if (compareResult > 0)
                {
                    node = node.Left ?? throw new ArgumentNullException("Item does not exist.");
                    continue;
                }

                var parent = node.Parent;

                if (node.IsLeaf)
                {
                    DeleteLeaf(node);
                }
                else
                {
                    if (node.Left != null && node.Right == null)
                    {
                        DeleteLeftNode(node);
                    }
                    else if (node.Right != null && node.Left == null)
                    {
                        DeleteRightNode(node);
                    }
                    else
                    {
                        var maxLeftNode = FindMax(node.Left);

                        node.Value = maxLeftNode.Value;

                        Delete(node.Left, maxLeftNode.Value);
                    }
                }

                if (parent != null) Splay(parent);

                break;
            }
        }

        private void DeleteLeaf(SplayTreeNode<T> node)
        {
            if (node.Parent == null)
                Root = null;
            else if (node.IsLeftChild)
                node.Parent.Left = null;
            else
                node.Parent.Right = null;
        }

        private void DeleteRightNode(SplayTreeNode<T> node)
        {
            if (node.Parent == null)
            {
                Root.Right.Parent = null;
                Root = Root.Right;
                return;
            }

            if (node.IsLeftChild)
                node.Parent.Left = node.Right;
            else
                node.Parent.Right = node.Right;

            node.Right.Parent = node.Parent;
        }

        private void DeleteLeftNode(SplayTreeNode<T> node)
        {
            if (node.Parent == null)
            {
                Root.Left.Parent = null;
                Root = Root.Left;
                return;
            }

            if (node.IsLeftChild)
                node.Parent.Left = node.Left;
            else
                node.Parent.Right = node.Left;

            node.Left.Parent = node.Parent;
        }

        public T FindMax()
        {
            return FindMax(Root).Value;
        }

        private SplayTreeNode<T> FindMax(SplayTreeNode<T> node)
        {
            while (true)
            {
                if (node.Right == null) return node;
                node = node.Right;
            }
        }

        public T FindMin()
        {
            return FindMin(Root).Value;
        }

        private SplayTreeNode<T> FindMin(SplayTreeNode<T> node)
        {
            while (true)
            {
                if (node.Left == null) return node;
                node = node.Left;
            }
        }

        private SplayTreeNode<T> Find(SplayTreeNode<T> parent, T value)
        {
            while (true)
            {
                if (parent == null) return null;

                if (parent.Value.CompareTo(value) == 0) return parent;

                var left = Find(parent.Left, value);

                if (left != null) return left;
                parent = parent.Right;
            }
        }

        private void Splay(SplayTreeNode<T> x)
        {
            x.UpdateCounts();

            while (x.Parent != null)
            {
                if (x.Parent.Parent == null)
                {
                    x = x.IsLeftChild ? RightRotate(x.Parent) : LeftRotate(x.Parent);
                }
                else if (x.IsLeftChild && x.Parent.IsLeftChild)

                {
                    RightRotate(x.Parent.Parent);
                    x = RightRotate(x.Parent);
                }
                else if (x.IsRightChild && x.Parent.IsRightChild)
                {
                    LeftRotate(x.Parent.Parent);
                    x = LeftRotate(x.Parent);
                }
                else if (x.IsLeftChild && x.Parent.IsRightChild)
                {
                    RightRotate(x.Parent);
                    x = LeftRotate(x.Parent);
                }
                else
                {
                    LeftRotate(x.Parent);
                    x = RightRotate(x.Parent);
                }

                x.UpdateCounts();
            }
        }

        private SplayTreeNode<T> RightRotate(SplayTreeNode<T> currentRoot)
        {
            var prevRoot = currentRoot;
            var leftRightChild = prevRoot.Left.Right;

            var newRoot = currentRoot.Left;

            //make left child as root
            prevRoot.Left.Parent = prevRoot.Parent;

            if (prevRoot.Parent != null)
            {
                if (prevRoot.Parent.Left == prevRoot)
                    prevRoot.Parent.Left = prevRoot.Left;
                else
                    prevRoot.Parent.Right = prevRoot.Left;
            }

            //move prev root as right child of current root
            newRoot.Right = prevRoot;
            prevRoot.Parent = newRoot;

            //move right child of left child of prev root to left child of right child of new root
            newRoot.Right.Left = leftRightChild;
            if (newRoot.Right.Left != null) newRoot.Right.Left.Parent = newRoot.Right;

            newRoot.Left.UpdateCounts();
            newRoot.Right.UpdateCounts();
            newRoot.UpdateCounts();

            if (prevRoot == Root) Root = newRoot;

            return newRoot;
        }

        private SplayTreeNode<T> LeftRotate(SplayTreeNode<T> currentRoot)
        {
            var prevRoot = currentRoot;
            var rightLeftChild = prevRoot.Right.Left;

            var newRoot = currentRoot.Right;

            //make right child as root
            prevRoot.Right.Parent = prevRoot.Parent;

            if (prevRoot.Parent != null)
            {
                if (prevRoot.Parent.Left == prevRoot)
                    prevRoot.Parent.Left = prevRoot.Right;
                else
                    prevRoot.Parent.Right = prevRoot.Right;
            }

            //move prev root as left child of current root
            newRoot.Left = prevRoot;
            prevRoot.Parent = newRoot;

            //move left child of right child of prev root to right child of left child of new root
            newRoot.Left.Right = rightLeftChild;
            if (newRoot.Left.Right != null) newRoot.Left.Right.Parent = newRoot.Left;

            newRoot.Left.UpdateCounts();
            newRoot.Right.UpdateCounts();
            newRoot.UpdateCounts();

            if (prevRoot == Root) Root = newRoot;

            return newRoot;
        }

        private BinarySearchTreeNodeBase<T> Find(T value)
        {
            return Root.Find(value).Item1;
        }

        public T NextLower(T value)
        {
            var node = Find(value);
            if (node == null) return default;

            var next = node.NextLower();
            return next != null ? next.Value : default;
        }

        public T NextHigher(T value)
        {
            var node = Find(value);
            if (node == null) return default;

            var next = node.NextHigher();
            return next != null ? next.Value : default;
        }

        public IEnumerable<T> AsEnumerableDesc()
        {
            return GetEnumeratorDesc().AsEnumerable();
        }

        public IEnumerator<T> GetEnumeratorDesc()
        {
            return new BinarySearchTreeEnumerator<T>(Root, false);
        }
    }
}