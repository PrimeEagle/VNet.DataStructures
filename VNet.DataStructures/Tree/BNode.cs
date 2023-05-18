namespace VNet.DataStructures.Tree
{
    internal abstract class BNodeBase<T> where T : IComparable
    {
        internal int Index;
        internal int KeyCount;

        internal BNodeBase(int maxKeysPerNode)
        {
            Keys = new T[maxKeysPerNode];
        }

        internal T[] Keys { get; set; }
        internal abstract BNodeBase<T> GetParent();
        internal abstract BNodeBase<T>[] GetChildren();

        internal int GetMedianIndex()
        {
            return KeyCount / 2 + 1;
        }
    }
}
