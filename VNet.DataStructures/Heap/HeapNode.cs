namespace VNet.DataStructures.Heap
{
    public class HeapNode<TKey, TValue> where TKey : notnull where TValue : notnull
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }
        public HeapNode<TKey, TValue> Parent { get; set; }
        public List<HeapNode<TKey,TValue>> Children { get; set; }
        public bool Mark;

        public void AddChild(HeapNode<TKey, TValue> child)
        {
            child.Parent = this;
            Children = new List<HeapNode<TKey, TValue>>
            {
                child
            };
        }

        public override string ToString()
        {
            return $"({Key},{Value})";
        }
    }
}