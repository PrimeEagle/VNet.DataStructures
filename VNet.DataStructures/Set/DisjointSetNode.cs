namespace VNet.DataStructures.Set
{
    internal class DisjointSetNode<T>
    {
        internal T Data { get; set; }
        internal int Rank { get; set; }

        internal DisjointSetNode<T> Parent { get; set; }
    }
}