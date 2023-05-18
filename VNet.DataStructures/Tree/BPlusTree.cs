using System;
using System.Collections;
using System.Collections.Generic;

namespace VNet.DataStructures.Tree
{
    public class BPlusTree<T> : IEnumerable<T> where T : IComparable
    {
        private readonly int maxKeysPerNode;
        private readonly int minKeysPerNode;

        internal BPlusTreeNode<T> BottomLeftNode;
        internal BPlusTreeNode<T> BottomRightNode;
        internal BPlusTreeNode<T> Root;

        public BPlusTree(int maxKeysPerNode = 3)
        {
            if (maxKeysPerNode < 3) throw new Exception("Max keys per node should be atleast 3.");

            this.maxKeysPerNode = maxKeysPerNode;
            minKeysPerNode = maxKeysPerNode / 2;
        }

        public int Count { get; private set; }

        public T Max
        {
            get
            {
                if (Root == null) return default;

                var maxNode = BottomRightNode;
                return maxNode.Keys[maxNode.KeyCount - 1];
            }
        }

        public T Min
        {
            get
            {
                if (Root == null) return default;

                var minNode = BottomLeftNode;
                return minNode.Keys[0];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new BplusTreeEnumerator<T>(this);
        }

        public bool HasItem(T value)
        {
            return Find(Root, value) != null;
        }

        private BPlusTreeNode<T> Find(BPlusTreeNode<T> node, T value)
        {
            if (node.IsLeaf)
            {
                for (var i = 0; i < node.KeyCount; i++)
                    if (value.CompareTo(node.Keys[i]) == 0)
                        return node;
            }
            else
            {
                for (var i = 0; i < node.KeyCount; i++)
                {
                    if (value.CompareTo(node.Keys[i]) < 0) return Find(node.Children[i], value);
                    if (node.KeyCount == i + 1) return Find(node.Children[i + 1], value);
                }
            }

            return null;
        }

        public void Insert(T newValue)
        {
            if (Root == null)
            {
                Root = new BPlusTreeNode<T>(maxKeysPerNode, null) { Keys = { [0] = newValue } };
                Root.KeyCount++;
                Count++;
                BottomLeftNode = Root;
                BottomRightNode = Root;
                return;
            }

            var leafToInsert = FindInsertionLeaf(Root, newValue);

            InsertAndSplit(ref leafToInsert, newValue, null, null);
            Count++;
        }

        private BPlusTreeNode<T> FindInsertionLeaf(BPlusTreeNode<T> node, T newValue)
        {
            if (node.IsLeaf) return node;

            for (var i = 0; i < node.KeyCount; i++)
            {
                if (newValue.CompareTo(node.Keys[i]) < 0) return FindInsertionLeaf(node.Children[i], newValue);
                if (node.KeyCount == i + 1) return FindInsertionLeaf(node.Children[i + 1], newValue);
            }

            return node;
        }

        private void InsertAndSplit(ref BPlusTreeNode<T> node, T newValue,
            BPlusTreeNode<T> newValueLeft, BPlusTreeNode<T> newValueRight)
        {
            if (node == null)
            {
                node = new BPlusTreeNode<T>(maxKeysPerNode, null);
                Root = node;
            }

            if (node.KeyCount != maxKeysPerNode)
            {
                InsertToNotFullNode(ref node, newValue, newValueLeft, newValueRight);
                return;
            }

            var left = new BPlusTreeNode<T>(maxKeysPerNode, null);
            var right = new BPlusTreeNode<T>(maxKeysPerNode, null);

            if (node.IsLeaf) ConnectLeaves(node, left, right);

            var currentMedianIndex = node.GetMedianIndex();
            var currentNode = left;
            var currentNodeIndex = 0;
            var newMedian = default(T);
            var newMedianSet = false;
            var newValueInserted = false;
            var insertionCount = 0;

            for (var i = 0; i < node.KeyCount; i++)
            {
                if (!newMedianSet && insertionCount == currentMedianIndex)
                {
                    newMedianSet = true;

                    if (!newValueInserted && newValue.CompareTo(node.Keys[i]) < 0)
                    {
                        newMedian = newValue;
                        newValueInserted = true;

                        if (newValueLeft != null) SetChild(currentNode, currentNode.KeyCount, newValueLeft);

                        currentNode = right;
                        currentNodeIndex = 0;

                        if (newValueRight != null) SetChild(currentNode, 0, newValueRight);

                        i--;
                        insertionCount++;
                        continue;
                    }


                    newMedian = node.Keys[i];
                    currentNode = right;
                    currentNodeIndex = 0;

                    continue;
                }

                if (newValueInserted || node.Keys[i].CompareTo(newValue) < 0)
                {
                    currentNode.Keys[currentNodeIndex] = node.Keys[i];
                    currentNode.KeyCount++;

                    if (currentNode.Children[currentNodeIndex] == null)
                        SetChild(currentNode, currentNodeIndex, node.Children[i]);

                    SetChild(currentNode, currentNodeIndex + 1, node.Children[i + 1]);
                }
                else
                {
                    currentNode.Keys[currentNodeIndex] = newValue;
                    currentNode.KeyCount++;

                    SetChild(currentNode, currentNodeIndex, newValueLeft);
                    SetChild(currentNode, currentNodeIndex + 1, newValueRight);

                    i--;
                    newValueInserted = true;
                }

                currentNodeIndex++;
                insertionCount++;
            }

            if (!newValueInserted)
            {
                currentNode.Keys[currentNodeIndex] = newValue;
                currentNode.KeyCount++;

                SetChild(currentNode, currentNodeIndex, newValueLeft);
                SetChild(currentNode, currentNodeIndex + 1, newValueRight);
            }

            if (node.IsLeaf)
            {
                InsertAt(right.Keys, 0, newMedian);
                right.KeyCount++;
            }

            var parent = node.Parent;
            InsertAndSplit(ref parent, newMedian, left, right);
        }

        private void InsertToNotFullNode(ref BPlusTreeNode<T> node, T newValue,
            BPlusTreeNode<T> newValueLeft, BPlusTreeNode<T> newValueRight)
        {
            var inserted = false;

            if (newValueLeft != null)
            {
                newValueLeft.Parent = node;
                newValueRight.Parent = node;
            }

            for (var i = 0; i < node.KeyCount; i++)
            {
                if (newValue.CompareTo(node.Keys[i]) >= 0) continue;

                InsertAt(node.Keys, i, newValue);
                node.KeyCount++;

                SetChild(node, i, newValueLeft);
                InsertChild(node, i + 1, newValueRight);

                inserted = true;
                break;
            }

            if (inserted) return;

            node.Keys[node.KeyCount] = newValue;
            node.KeyCount++;

            SetChild(node, node.KeyCount - 1, newValueLeft);
            SetChild(node, node.KeyCount, newValueRight);
        }


        private void ConnectLeaves(BPlusTreeNode<T> node, BPlusTreeNode<T> left, BPlusTreeNode<T> right)
        {
            left.Next = right;
            right.Prev = left;

            if (node.Next != null)
            {
                right.Next = node.Next;
                node.Next.Prev = right;
            }
            else
            {
                BottomRightNode = right;
            }

            if (node.Prev != null)
            {
                left.Prev = node.Prev;
                node.Prev.Next = left;
            }
            else
            {
                BottomLeftNode = left;
            }
        }

        public void Delete(T value)
        {
            var node = FindDeletionNode(Root, value);

            if (node == null) throw new Exception("Item do not exist in this tree.");

            for (var i = 0; i < node.KeyCount; i++)
            {
                if (value.CompareTo(node.Keys[i]) != 0) continue;

                RemoveAt(node.Keys, i);
                node.KeyCount--;

                if (node.Parent != null && node != node.Parent.Children[0] && node.KeyCount > 0)
                {
                    var separatorIndex = GetPrevSeparatorIndex(node);
                    node.Parent.Keys[separatorIndex] = node.Keys[0];
                }

                Balance(node, value);

                Count--;
                return;
            }
        }

        private BPlusTreeNode<T> FindMinNode(BPlusTreeNode<T> node)
        {
            while (true)
            {
                if (node.IsLeaf) return node;
                node = node.Children[0];
            }
        }

        private BPlusTreeNode<T> FindMaxNode(BPlusTreeNode<T> node)
        {
            while (true)
            {
                if (node.IsLeaf) return node;
                node = node.Children[node.KeyCount];
            }
        }

        private void Balance(BPlusTreeNode<T> node, T deleteKey)
        {
            if (node == Root) return;

            if (node.KeyCount >= minKeysPerNode)
            {
                UpdateIndex(node, deleteKey, true);
                return;
            }

            var rightSibling = GetRightSibling(node);

            if (rightSibling != null
                && rightSibling.KeyCount > minKeysPerNode)
            {
                LeftRotate(node, rightSibling);
                FindMinNode(node);
                UpdateIndex(node, deleteKey, true);
                return;
            }

            var leftSibling = GetLeftSibling(node);

            if (leftSibling != null
                && leftSibling.KeyCount > minKeysPerNode)
            {
                RightRotate(leftSibling, node);
                UpdateIndex(node, deleteKey, true);
                return;
            }

            if (rightSibling != null)
                Sandwich(node, rightSibling, deleteKey);
            else
                Sandwich(leftSibling, node, deleteKey);
        }

        private void UpdateIndex(BPlusTreeNode<T> node, T deleteKey, bool spiralUp)
        {
            while (true)
            {
                if (node == null) return;

                if (node.IsLeaf || node.Children[0].IsLeaf)
                {
                    node = node.Parent;
                    continue;
                }

                for (var i = 0; i < node.KeyCount; i++)
                    if (node.Keys[i].CompareTo(deleteKey) == 0)
                        node.Keys[i] = FindMinNode(node.Children[i + 1]).Keys[0];

                if (spiralUp)
                {
                    node = node.Parent;
                    continue;
                }

                break;
            }
        }

        private void Sandwich(BPlusTreeNode<T> leftSibling, BPlusTreeNode<T> rightSibling, T deleteKey)
        {
            var separatorIndex = GetNextSeparatorIndex(leftSibling);
            var parent = leftSibling.Parent;

            var newNode = new BPlusTreeNode<T>(maxKeysPerNode, leftSibling.Parent);

            if (leftSibling.IsLeaf) MergeLeaves(newNode, leftSibling, rightSibling);

            var newIndex = 0;
            for (var i = 0; i < leftSibling.KeyCount; i++)
            {
                newNode.Keys[newIndex] = leftSibling.Keys[i];

                if (leftSibling.Children[i] != null) SetChild(newNode, newIndex, leftSibling.Children[i]);

                if (leftSibling.Children[i + 1] != null) SetChild(newNode, newIndex + 1, leftSibling.Children[i + 1]);

                newIndex++;
            }

            if (leftSibling.KeyCount == 0 && leftSibling.Children[0] != null)
                SetChild(newNode, newIndex, leftSibling.Children[0]);

            if (!rightSibling.IsLeaf)
            {
                newNode.Keys[newIndex] = parent.Keys[separatorIndex];
                newIndex++;
            }

            for (var i = 0; i < rightSibling.KeyCount; i++)
            {
                newNode.Keys[newIndex] = rightSibling.Keys[i];

                if (rightSibling.Children[i] != null)
                {
                    SetChild(newNode, newIndex, rightSibling.Children[i]);

                    if (i == 0 && rightSibling.Children[i].IsLeaf
                               && rightSibling.Children[i].Keys[0].CompareTo(newNode.Keys[newIndex - 1]) != 0)
                    {
                        InsertAt(rightSibling.Children[i].Keys, 0, newNode.Keys[newIndex - 1]);
                        rightSibling.Children[i].KeyCount++;
                    }
                }

                if (rightSibling.Children[i + 1] != null) SetChild(newNode, newIndex + 1, rightSibling.Children[i + 1]);

                newIndex++;
            }

            if (rightSibling.KeyCount == 0 && rightSibling.Children[0] != null)
            {
                SetChild(newNode, newIndex, rightSibling.Children[0]);

                if (newNode.Children[newIndex].IsLeaf) newNode.Keys[newIndex - 1] = newNode.Children[newIndex].Keys[0];
            }

            newNode.KeyCount = newIndex;

            SetChild(parent, separatorIndex, newNode);

            RemoveAt(parent.Keys, separatorIndex);
            parent.KeyCount--;

            RemoveChild(parent, separatorIndex + 1);

            if (newNode.IsLeaf && newNode.Parent.Children[0] != newNode)
            {
                separatorIndex = GetPrevSeparatorIndex(newNode);
                newNode.Parent.Keys[separatorIndex] = newNode.Keys[0];
            }

            UpdateIndex(newNode, deleteKey, false);

            if (parent.KeyCount == 0
                && parent == Root)
            {
                Root = newNode;
                Root.Parent = null;

                if (Root.KeyCount == 0) Root = null;
                return;
            }

            if (parent.KeyCount < minKeysPerNode) Balance(parent, deleteKey);

            UpdateIndex(newNode, deleteKey, true);
        }

        private void RightRotate(BPlusTreeNode<T> leftSibling, BPlusTreeNode<T> rightSibling)
        {
            var parentIndex = GetNextSeparatorIndex(leftSibling);

            InsertAt(rightSibling.Keys, 0, rightSibling.Parent.Keys[parentIndex]);
            rightSibling.KeyCount++;

            InsertChild(rightSibling, 0, leftSibling.Children[leftSibling.KeyCount]);

            if (rightSibling.Children[1] != null
                && rightSibling.Children[1].IsLeaf)
                rightSibling.Keys[0] = rightSibling.Children[1].Keys[0];


            rightSibling.Parent.Keys[parentIndex] = leftSibling.Keys[leftSibling.KeyCount - 1];

            RemoveAt(leftSibling.Keys, leftSibling.KeyCount - 1);
            leftSibling.KeyCount--;

            RemoveChild(leftSibling, leftSibling.KeyCount + 1);

            if (rightSibling.IsLeaf) rightSibling.Keys[0] = rightSibling.Parent.Keys[parentIndex];
        }

        private void LeftRotate(BPlusTreeNode<T> leftSibling, BPlusTreeNode<T> rightSibling)
        {
            var parentIndex = GetNextSeparatorIndex(leftSibling);

            leftSibling.Keys[leftSibling.KeyCount] = leftSibling.Parent.Keys[parentIndex];
            leftSibling.KeyCount++;

            SetChild(leftSibling, leftSibling.KeyCount, rightSibling.Children[0]);

            if (leftSibling.Children[leftSibling.KeyCount] != null
                && leftSibling.Children[leftSibling.KeyCount].IsLeaf)
                leftSibling.Keys[leftSibling.KeyCount - 1] = leftSibling.Children[leftSibling.KeyCount].Keys[0];

            leftSibling.Parent.Keys[parentIndex] = rightSibling.Keys[0];

            RemoveAt(rightSibling.Keys, 0);
            rightSibling.KeyCount--;

            RemoveChild(rightSibling, 0);

            if (rightSibling.IsLeaf) rightSibling.Parent.Keys[parentIndex] = rightSibling.Keys[0];

            if (leftSibling.IsLeaf && leftSibling.Parent.Children[0] != leftSibling)
            {
                parentIndex = GetPrevSeparatorIndex(leftSibling);
                leftSibling.Parent.Keys[parentIndex] = leftSibling.Keys[0];
            }
        }

        private void MergeLeaves(BPlusTreeNode<T> newNode, BPlusTreeNode<T> leftSibling, BPlusTreeNode<T> rightSibling)
        {
            if (leftSibling.Prev != null)
            {
                newNode.Prev = leftSibling.Prev;
                leftSibling.Prev.Next = newNode;
            }
            else
            {
                BottomLeftNode = newNode;
            }

            if (rightSibling.Next != null)
            {
                newNode.Next = rightSibling.Next;
                rightSibling.Next.Prev = newNode;
            }
            else
            {
                BottomRightNode = newNode;
            }
        }

        private BPlusTreeNode<T> FindDeletionNode(BPlusTreeNode<T> node, T value)
        {
            if (node.IsLeaf)
            {
                for (var i = 0; i < node.KeyCount; i++)
                    if (value.CompareTo(node.Keys[i]) == 0)
                        return node;
            }
            else
            {
                for (var i = 0; i < node.KeyCount; i++)
                {
                    if (value.CompareTo(node.Keys[i]) < 0) return FindDeletionNode(node.Children[i], value);
                    if (node.KeyCount == i + 1) return FindDeletionNode(node.Children[i + 1], value);
                }
            }

            return null;
        }

        private int GetPrevSeparatorIndex(BPlusTreeNode<T> node)
        {
            var parent = node.Parent;

            if (node.Index == 0) return 0;

            if (node.Index == parent.KeyCount) return node.Index - 1;

            return node.Index - 1;
        }

        private int GetNextSeparatorIndex(BPlusTreeNode<T> node)
        {
            var parent = node.Parent;

            if (node.Index == 0) return 0;

            if (node.Index == parent.KeyCount) return node.Index - 1;

            return node.Index;
        }

        private BPlusTreeNode<T> GetRightSibling(BPlusTreeNode<T> node)
        {
            var parent = node.Parent;
            return node.Index == parent.KeyCount ? null : parent.Children[node.Index + 1];
        }

        private BPlusTreeNode<T> GetLeftSibling(BPlusTreeNode<T> node)
        {
            return node.Index == 0 ? null : node.Parent.Children[node.Index - 1];
        }

        private void SetChild(BPlusTreeNode<T> parent, int childIndex, BPlusTreeNode<T> child)
        {
            parent.Children[childIndex] = child;

            if (child == null) return;

            child.Parent = parent;
            child.Index = childIndex;
        }

        private void InsertChild(BPlusTreeNode<T> parent, int childIndex, BPlusTreeNode<T> child)
        {
            InsertAt(parent.Children, childIndex, child);

            if (child != null) child.Parent = parent;

            for (var i = childIndex; i <= parent.KeyCount; i++)
                if (parent.Children[i] != null)
                    parent.Children[i].Index = i;
        }

        private void RemoveChild(BPlusTreeNode<T> parent, int childIndex)
        {
            RemoveAt(parent.Children, childIndex);

            for (var i = childIndex; i <= parent.KeyCount; i++)
                if (parent.Children[i] != null)
                    parent.Children[i].Index = i;
        }

        private void InsertAt<TS>(TS[] array, int index, TS newValue)
        {
            Array.Copy(array, index, array, index + 1, array.Length - index - 1);
            array[index] = newValue;
        }

        private void RemoveAt<TS>(TS[] array, int index)
        {
            Array.Copy(array, index + 1, array, index, array.Length - index - 1);
        }

        public IEnumerable<T> AsEnumerableDesc()
        {
            return GetEnumeratorDesc().AsEnumerable();
        }

        public IEnumerator<T> GetEnumeratorDesc()
        {
            return new BplusTreeEnumerator<T>(this, false);
        }
    }
}