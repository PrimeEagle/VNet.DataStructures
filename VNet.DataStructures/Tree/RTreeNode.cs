using VNet.Mathematics.Geometry;

namespace VNet.DataStructures.Tree
{
    internal class RTreeNode
    {
        internal int Height;
        internal int Index;
        internal int KeyCount;

        internal RTreeNode(int maxKeysPerNode, RTreeNode parent)
        {
            Parent = parent;
            Children = new RTreeNode[maxKeysPerNode];
        }

        internal MbRectangle MbRectangle { get; set; }

        internal RTreeNode Parent { get; set; }
        internal RTreeNode[] Children { get; set; }

        internal bool IsLeaf => MbRectangle.Polygon != null
                                || Children[0].MbRectangle.Polygon != null;

        internal void AddChild(RTreeNode child)
        {
            SetChild(KeyCount, child);
            KeyCount++;
        }

        internal void SetChild(int index, RTreeNode child)
        {
            Children[index] = child;
            Children[index].Parent = this;
            Children[index].Index = index;

            if (MbRectangle == null)
                MbRectangle = new MbRectangle(child.MbRectangle);
            else
                MbRectangle.Merge(child.MbRectangle);

            Height = child.Height + 1;
        }

        internal RTreeNode GetMinimumEnlargementAreaMbr(MbRectangle newPolygon)
        {
            return Children[Children.Take(KeyCount)
                .Select((node, index) => new { node, index })
                .OrderBy(x => x.node.MbRectangle.GetEnlargementArea(newPolygon))
                .ThenBy(x => x.node.MbRectangle.Area())
                .First().index];
        }
    }
}