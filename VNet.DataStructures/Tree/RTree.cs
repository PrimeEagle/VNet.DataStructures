using System.Collections;
using VNet.Mathematics.Geometry;

namespace VNet.DataStructures.Tree
{
    public class RTree : IEnumerable<Polygon>
    {
        private readonly int maxKeysPerNode;
        private readonly int minKeysPerNode;
        private readonly Dictionary<Polygon, RTreeNode> leafMappings = new();

        internal RTreeNode Root;

        public RTree(int maxKeysPerNode)
        {
            if (maxKeysPerNode < 3) throw new ArgumentOutOfRangeException("Max keys per node should be at least 3.");

            this.maxKeysPerNode = maxKeysPerNode;
            minKeysPerNode = maxKeysPerNode / 2;
        }

        public int Count { get; private set; }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<Polygon> GetEnumerator()
        {
            return leafMappings.Select(x => x.Key).GetEnumerator();
        }

        public void Insert(Polygon newPolygon)
        {
            var newNode = new RTreeNode(maxKeysPerNode, null)
            {
                MbRectangle = newPolygon.GetContainingRectangle()
            };

            leafMappings.Add(newPolygon, newNode);
            InsertToLeaf(newNode);
            Count++;
        }

        private void InsertToLeaf(RTreeNode newNode)
        {
            if (Root == null)
            {
                Root = new RTreeNode(maxKeysPerNode, null);
                Root.AddChild(newNode);
                return;
            }

            var leafToInsert = FindInsertionLeaf(Root, newNode);
            InsertAndSplit(leafToInsert, newNode);
        }

        private void InsertInternalNode(RTreeNode internalNode)
        {
            InsertInternalNode(Root, internalNode);
        }

        private void InsertInternalNode(RTreeNode currentNode, RTreeNode internalNode)
        {
            if (currentNode.Height == internalNode.Height + 1)
                InsertAndSplit(currentNode, internalNode);
            else
                InsertInternalNode(currentNode.GetMinimumEnlargementAreaMbr(internalNode.MbRectangle), internalNode);
        }

        private RTreeNode FindInsertionLeaf(RTreeNode node, RTreeNode newNode)
        {
            if (node.IsLeaf) return node;

            return FindInsertionLeaf(node.GetMinimumEnlargementAreaMbr(newNode.MbRectangle), newNode);
        }

        private void InsertAndSplit(RTreeNode node, RTreeNode newValue)
        {
            //newValue have room to fit in this node
            if (node.KeyCount < maxKeysPerNode)
            {
                node.AddChild(newValue);
                ExpandAncestorMbRs(node);
                return;
            }

            var e = new List<RTreeNode>(new[] { newValue });
            e.AddRange(node.Children);

            var distantPairs = GetDistantPairs(e);

            var e1 = new RTreeNode(maxKeysPerNode, null);
            var e2 = new RTreeNode(maxKeysPerNode, null);

            e1.AddChild(distantPairs.Item1);
            e2.AddChild(distantPairs.Item2);

            e = e.Where(x => x != distantPairs.Item1 && x != distantPairs.Item2)
                .ToList();

            while (e.Count > 0)
            {
                var current = e[e.Count - 1];

                var leftEnlargementArea = e1.MbRectangle.GetEnlargementArea(current.MbRectangle);
                var rightEnlargementArea = e2.MbRectangle.GetEnlargementArea(current.MbRectangle);

                if (leftEnlargementArea == rightEnlargementArea)
                {
                    var leftArea = e1.MbRectangle.Area();
                    var rightArea = e2.MbRectangle.Area();

                    if (leftArea == rightArea)
                    {
                        if (e1.KeyCount < e2.KeyCount)
                            e1.AddChild(current);
                        else
                            e2.AddChild(current);
                    }
                    else if (leftArea < rightArea)
                    {
                        e1.AddChild(current);
                    }
                    else
                    {
                        e2.AddChild(current);
                    }
                }
                else if (leftEnlargementArea < rightEnlargementArea)
                {
                    e1.AddChild(current);
                }
                else
                {
                    e2.AddChild(current);
                }

                e.RemoveAt(e.Count - 1);

                var remaining = e.Count;

                /*if during the assignment of entries, there remain λ entries to be assigned
                and the one node contains minKeysPerNode − λ entries then
                assign all the remaining entries to this node without considering
                the aforementioned criteria
                so that the node will contain at least minKeysPerNode entries */
                if (e1.KeyCount == minKeysPerNode - remaining)
                {
                    foreach (var entry in e) e1.AddChild(entry);
                    e.Clear();
                }
                else if (e2.KeyCount == minKeysPerNode - remaining)
                {
                    foreach (var entry in e) e2.AddChild(entry);
                    e.Clear();
                }
            }

            var parent = node.Parent;
            if (parent != null)
            {
                parent.SetChild(node.Index, e1);
                InsertAndSplit(parent, e2);
            }
            else
            {
                Root = new RTreeNode(maxKeysPerNode, null);
                Root.AddChild(e1);
                Root.AddChild(e2);
            }
        }

        private void ExpandAncestorMbRs(RTreeNode node)
        {
            while (node.Parent != null)
            {
                node.Parent.MbRectangle.Merge(node.MbRectangle);
                node.Parent.Height = node.Height + 1;
                node = node.Parent;
            }
        }

        private Tuple<RTreeNode, RTreeNode> GetDistantPairs(List<RTreeNode> allEntries)
        {
            Tuple<RTreeNode, RTreeNode> result = null;

            var maxArea = double.MinValue;
            for (var i = 0; i < allEntries.Count; i++)
                for (var j = i + 1; j < allEntries.Count; j++)
                {
                    var currentArea = allEntries[i].MbRectangle.GetEnlargementArea(allEntries[j].MbRectangle);
                    if (currentArea > maxArea)
                    {
                        result = new Tuple<RTreeNode, RTreeNode>(allEntries[i], allEntries[j]);
                        maxArea = currentArea;
                    }
                }

            return result;
        }

        public bool Exists(Polygon searchPolygon)
        {
            return leafMappings.ContainsKey(searchPolygon);
        }

        public List<Polygon> RangeSearch(Rectangle searchRectangle)
        {
            return RangeSearch(Root, searchRectangle, new List<Polygon>());
        }

        private List<Polygon> RangeSearch(RTreeNode current, Rectangle searchRectangle, List<Polygon> result)
        {
            if (current.IsLeaf)
                foreach (var node in current.Children.Take(current.KeyCount))
                    if (RectangleIntersection.DoIntersect(node.MbRectangle, searchRectangle))
                        result.Add(node.MbRectangle.Polygon);

            foreach (var node in current.Children.Take(current.KeyCount))
                if (RectangleIntersection.DoIntersect(node.MbRectangle, searchRectangle))
                    RangeSearch(node, searchRectangle, result);

            return result;
        }

        public void Delete(Polygon polygon)
        {
            if (Root == null) throw new Exception("Empty tree.");

            if (!Exists(polygon)) throw new Exception("Given polygon do not belong to this tree.");

            var nodeToDelete = leafMappings[polygon];

            DeleteNode(nodeToDelete);
            CondenseTree(nodeToDelete.Parent);

            if (Root.KeyCount == 1 && !Root.IsLeaf)
            {
                Root = Root.Children[0];
                Root.Parent = null;
            }

            leafMappings.Remove(polygon);
            Count--;

            if (Count == 0) Root = null;
        }

        private void DeleteNode(RTreeNode nodeToDelete)
        {
            RemoveAt(nodeToDelete.Parent.Children, nodeToDelete.Index);
            nodeToDelete.Parent.KeyCount--;
            UpdateIndex(nodeToDelete.Parent.Children, nodeToDelete.Parent.KeyCount, nodeToDelete.Index);
        }

        private void RemoveAt(RTreeNode[] array, int index)
        {
            Array.Copy(array, index + 1, array, index, array.Length - index - 1);
        }

        private void UpdateIndex(RTreeNode[] children, int keyCount, int index)
        {
            for (var i = index; i < keyCount; i++) children[i].Index--;
        }

        private void CondenseTree(RTreeNode updatedleaf)
        {
            var current = updatedleaf;
            var toReinsert = new Stack<RTreeNode>();

            while (current != Root)
            {
                var parent = current.Parent;

                if (current.KeyCount < minKeysPerNode)
                {
                    DeleteNode(current);
                    foreach (var node in current.Children.Take(current.KeyCount)) toReinsert.Push(node);
                }
                else
                {
                    ShrinkMbr(current);
                }

                current = parent;
            }

            if (current.KeyCount > 0) ShrinkMbr(current);

            while (toReinsert.Count > 0)
            {
                var node = toReinsert.Pop();

                if (node.Height > 0)
                    InsertInternalNode(node);
                else
                    InsertToLeaf(node);
            }
        }

        private void ShrinkMbr(RTreeNode current)
        {
            current.MbRectangle = new MbRectangle(current.Children[0].MbRectangle);
            foreach (var node in current.Children.Skip(1).Take(current.KeyCount - 1))
                current.MbRectangle.Merge(node.MbRectangle);
        }

        public void Clear()
        {
            Root = null;
            leafMappings.Clear();
            Count = 0;
        }
    }
}