using VNet.DataStructures.Graph.Algorithms.Coloring;

namespace VNet.DataStructures.Graph.Algorithms.Clique
{
    public interface IStandardGraphCliqueAlgorithm<TNode, TValue> : IGraphColoringAlgorithm<TNode, TValue>
        where TNode : notnull, INode<TValue>
                                                             where TValue : notnull
    {
        public void Traverse(TNode starTNode, Action<TNode> preVisit, Action<TNode> postVisit);
    }
}