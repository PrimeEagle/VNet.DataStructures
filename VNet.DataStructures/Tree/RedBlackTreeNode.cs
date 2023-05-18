﻿namespace VNet.DataStructures.Tree
{
    internal class RedBlackTreeNode<T> : BinarySearchTreeNodeBase<T> where T : IComparable
    {
        internal RedBlackTreeNode(RedBlackTreeNode<T> parent, T value)
        {
            Parent = parent;
            Value = value;
            NodeColor = RedBlackTreeNodeColor.Red;
        }

        internal new RedBlackTreeNode<T> Parent
        {
            get => (RedBlackTreeNode<T>)base.Parent;
            set => base.Parent = value;
        }

        internal new RedBlackTreeNode<T> Left
        {
            get => (RedBlackTreeNode<T>)base.Left;
            set => base.Left = value;
        }

        internal new RedBlackTreeNode<T> Right
        {
            get => (RedBlackTreeNode<T>)base.Right;
            set => base.Right = value;
        }

        internal RedBlackTreeNodeColor NodeColor { get; set; }

        internal RedBlackTreeNode<T> Sibling => Parent.Left == this ? Parent.Right : Parent.Left;
    }
}