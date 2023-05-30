namespace VNet.DataStructures.Graph
{
    public interface INode<T> where T : notnull
    {
        public new T Value { get; init; }
    }
}