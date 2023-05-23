namespace VNet.DataStructures.Graph
{
    //public interface INode
    //{
    //    public object Value { get; init; }
    //}
    public interface INode<T> where T : notnull
    {
        public new T Value { get; init; }
    }
}