using VNet.Mathematics.Geometry;

namespace VNet.DataStructures.Tree
{
    internal class QuadTreeNode<T>
    {
        internal bool IsDeleted;
        internal QuadTreeNode<T> Nw, Ne, Se, Sw;
        internal Point Point;
        internal T Value;

        internal QuadTreeNode(Point point, T value)
        {
            Point = point;
            Value = value;
        }
    }
}
