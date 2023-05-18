using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace VNet.DataStructures.Tree
{
    public class kdTree<T> : IEnumerable<T[]> where T : IComparable
    {
        private readonly int dimensions;
        private kdTreeNode<T> root;

        public kdTree(int dimensions)
        {
            this.dimensions = dimensions;
            if (dimensions <= 0) throw new ArgumentOutOfRangeException("Dimension should be greater than 0.");
        }

        public int Count { get; private set; }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T[]> GetEnumerator()
        {
            return new kdTreeEnumerator<T>(root);
        }

        public void Insert(T[] point)
        {
            if (root == null)
            {
                root = new kdTreeNode<T>(dimensions, null);
                root.Points = new T[dimensions];
                CopyPoints(root.Points, point);
                Count++;
                return;
            }

            Insert(root, point, 0);
            Count++;
        }

        private void Insert(kdTreeNode<T> currentNode, T[] point, int depth)
        {
            var currentDimension = depth % dimensions;

            if (point[currentDimension].CompareTo(currentNode.Points[currentDimension]) < 0)
            {
                if (currentNode.Left == null)
                {
                    currentNode.Left = new kdTreeNode<T>(dimensions, currentNode);
                    currentNode.Left.Points = new T[dimensions];
                    CopyPoints(currentNode.Left.Points, point);
                    return;
                }

                Insert(currentNode.Left, point, depth + 1);
            }
            else if (point[currentDimension].CompareTo(currentNode.Points[currentDimension]) >= 0)
            {
                if (currentNode.Right == null)
                {
                    currentNode.Right = new kdTreeNode<T>(dimensions, currentNode);
                    currentNode.Right.Points = new T[dimensions];
                    CopyPoints(currentNode.Right.Points, point);
                    return;
                }

                Insert(currentNode.Right, point, depth + 1);
            }
        }

        public void Delete(T[] point)
        {
            if (root == null) throw new NullReferenceException("root is null");

            Delete(root, point, 0);
            Count--;
        }

        private void Delete(kdTreeNode<T> currentNode, T[] point, int depth)
        {
            if (currentNode == null) throw new ArgumentOutOfRangeException("Given deletion point do not exist in this kd tree.");

            var currentDimension = depth % dimensions;

            if (DoMatch(currentNode.Points, point))
            {
                HandleDeleteCases(currentNode, point, depth);
                return;
            }

            if (point[currentDimension].CompareTo(currentNode.Points[currentDimension]) < 0)
                Delete(currentNode.Left, point, depth + 1);
            else if (point[currentDimension].CompareTo(currentNode.Points[currentDimension]) >= 0)
                Delete(currentNode.Right, point, depth + 1);
        }

        private void HandleDeleteCases(kdTreeNode<T> currentNode, T[] point, int depth)
        {
            if (currentNode.IsLeaf)
            {
                if (currentNode == root)
                {
                    root = null;
                }
                else
                {
                    if (currentNode.IsLeftChild)
                        currentNode.Parent.Left = null;
                    else
                        currentNode.Parent.Right = null;

                    return;
                }
            }

            if (currentNode.Right != null)
            {
                var minNode = FindMin(currentNode.Right, depth % dimensions, depth + 1);
                CopyPoints(currentNode.Points, minNode.Points);

                Delete(currentNode.Right, minNode.Points, depth + 1);
            }
            else if (currentNode.Left != null)
            {
                var minNode = FindMin(currentNode.Left, depth % dimensions, depth + 1);
                CopyPoints(currentNode.Points, minNode.Points);

                Delete(currentNode.Left, minNode.Points, depth + 1);

                currentNode.Right = currentNode.Left;
                currentNode.Left = null;
            }
        }

        private void CopyPoints(T[] points1, T[] points2)
        {
            for (var i = 0; i < points1.Length; i++) points1[i] = points2[i];
        }

        private kdTreeNode<T> FindMin(kdTreeNode<T> node, int searchdimension, int depth)
        {
            var currentDimension = depth % dimensions;

            if (currentDimension == searchdimension)
            {
                if (node.Left == null) return node;

                return FindMin(node.Left, searchdimension, depth + 1);
            }

            kdTreeNode<T> leftMin = null;
            if (node.Left != null) leftMin = FindMin(node.Left, searchdimension, depth + 1);

            kdTreeNode<T> rightMin = null;
            if (node.Right != null) rightMin = FindMin(node.Right, searchdimension, depth + 1);

            return Min(node, leftMin, rightMin, searchdimension);
        }

        private kdTreeNode<T> Min(kdTreeNode<T> node,
            kdTreeNode<T> leftMin, kdTreeNode<T> rightMin,
            int searchdimension)
        {
            var min = node;

            if (leftMin != null && min.Points[searchdimension]
                    .CompareTo(leftMin.Points[searchdimension]) > 0)
                min = leftMin;

            if (rightMin != null && min.Points[searchdimension]
                    .CompareTo(rightMin.Points[searchdimension]) > 0)
                min = rightMin;

            return min;
        }

        private bool DoMatch(T[] a, T[] b)
        {
            for (var i = 0; i < a.Length; i++)
                if (a[i].CompareTo(b[i]) != 0)
                    return false;

            return true;
        }

        public T[] NearestNeighbour(IDistanceCalculator<T> distanceCalculator, T[] point)
        {
            if (root == null) throw new NullReferenceException("root is null.");

            return FindNearestNeighbour(root, point, 0, distanceCalculator).Points;
        }

        private kdTreeNode<T> FindNearestNeighbour(kdTreeNode<T> currentNode,
            T[] searchPoint, int depth,
            IDistanceCalculator<T> distanceCalculator)
        {
            var currentDimension = depth % dimensions;
            kdTreeNode<T> currentBest = null;

            var compareResult = searchPoint[currentDimension]
                .CompareTo(currentNode.Points[currentDimension]);

            if (compareResult < 0)
            {
                if (currentNode.Left != null)
                    currentBest = FindNearestNeighbour(currentNode.Left,
                        searchPoint, depth + 1, distanceCalculator);
                else
                    currentBest = currentNode;
                if (currentNode.Right != null &&
                    (distanceCalculator.Compare(currentNode.Points[currentDimension], searchPoint[currentDimension],
                         searchPoint, currentBest.Points) < 0
                     || currentNode.Right.Points[currentDimension]
                         .CompareTo(currentNode.Points[currentDimension]) == 0))
                {
                    var rightBest = FindNearestNeighbour(currentNode.Right,
                        searchPoint, depth + 1,
                        distanceCalculator);

                    currentBest = GetClosestToPoint(distanceCalculator, currentBest, rightBest, searchPoint);
                }

                currentBest = GetClosestToPoint(distanceCalculator, currentBest, currentNode, searchPoint);
            }
            else if (compareResult >= 0)
            {
                if (currentNode.Right != null)
                    currentBest = FindNearestNeighbour(currentNode.Right,
                        searchPoint, depth + 1, distanceCalculator);
                else
                    currentBest = currentNode;

                if (currentNode.Left != null
                    && (distanceCalculator.Compare(currentNode.Points[currentDimension], searchPoint[currentDimension],
                        searchPoint, currentBest.Points) < 0 || compareResult == 0))
                {
                    var leftBest = FindNearestNeighbour(currentNode.Left,
                        searchPoint, depth + 1,
                        distanceCalculator);

                    currentBest = GetClosestToPoint(distanceCalculator, currentBest, leftBest, searchPoint);
                }

                currentBest = GetClosestToPoint(distanceCalculator, currentBest, currentNode, searchPoint);
            }


            return currentBest;
        }

        private kdTreeNode<T> GetClosestToPoint(IDistanceCalculator<T> distanceCalculator,
            kdTreeNode<T> currentBest, kdTreeNode<T> currentNode, T[] point)
        {
            if (distanceCalculator.Compare(currentBest.Points,
                    currentNode.Points, point) < 0)
                return currentBest;

            return currentNode;
        }

        public List<T[]> RangeSearch(T[] start, T[] end)
        {
            var result = RangeSearch(new List<T[]>(), root,
                start, end, 0);

            return result;
        }

        private List<T[]> RangeSearch(List<T[]> result,
            kdTreeNode<T> currentNode,
            T[] start, T[] end, int depth)
        {
            if (currentNode == null) return result;

            var currentDimension = depth % dimensions;

            if (currentNode.IsLeaf)
            {
                if (InRange(currentNode, start, end)) result.Add(currentNode.Points);
            }
            else
            {
                if (start[currentDimension].CompareTo(currentNode.Points[currentDimension]) < 0)
                    RangeSearch(result, currentNode.Left, start, end, depth + 1);
                if (end[currentDimension].CompareTo(currentNode.Points[currentDimension]) > 0)
                    RangeSearch(result, currentNode.Right, start, end, depth + 1);
                if (InRange(currentNode, start, end)) result.Add(currentNode.Points);
            }

            return result;
        }

        private bool InRange(kdTreeNode<T> node, T[] start, T[] end)
        {
            for (var i = 0; i < node.Points.Length; i++)
                if (!(start[i].CompareTo(node.Points[i]) <= 0
                      && end[i].CompareTo(node.Points[i]) >= 0))
                    return false;

            return true;
        }
    }
}