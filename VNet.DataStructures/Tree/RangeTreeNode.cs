namespace VNet.DataStructures.Tree
{
    internal class RangeTreeNode<T> : IComparable where T : IComparable
    {
        internal RangeTreeNode(T value)
        {
            Values = new List<T>(new[] { value });
            Tree = new OneDimentionalRangeTree<T>();
        }

        internal T Value => Values[0];

        internal List<T> Values { get; set; }

        internal OneDimentionalRangeTree<T> Tree { get; set; }

        public int CompareTo(object obj)
        {
            return Value.CompareTo(((RangeTreeNode<T>)obj).Value);
        }
    }
}