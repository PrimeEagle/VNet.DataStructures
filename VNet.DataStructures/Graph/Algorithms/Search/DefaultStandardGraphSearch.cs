using VNet.System.Extensions;

// ReSharper disable ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
#pragma warning disable CS8601
#pragma warning disable CS8602

namespace VNet.DataStructures.Graph.Algorithms.Search
{
    public class DefaultStandardGraphSearch<TNode, TEdge, TValue> : IGraphSearchAlgorithm<TNode, TEdge, TValue>
                                                                    where TNode : notnull, INode<TValue>
                                                                    where TEdge : notnull, IStandardEdge<TNode, TValue>
                                                                    where TValue : notnull, IComparable<TValue>
    {
        public IEnumerable<TNode> Search(IGraphSearchAlgorithmArgs<TNode, TEdge, TValue> args)
        {
            args.Graph.Validate(new GraphValidationArgs() { MustBeStandardGraph = true });
            if (!IsValid(args)) throw new ArgumentNullException(nameof(args), "Search arguments are not configured properly.");

            return (List<TNode>)FindNodes(args);
        }

        private static bool IsValid(IGraphSearchAlgorithmArgs<TNode, TEdge, TValue> args)
        {
            var result = args.TraversalAlgorithm is not null;
            if(result) result &= args.TraversalArgs is not null;
            if (result) result &= args.TraversalArgs.StartNode is not null;
            if (result) result &= args.StartNode is not null;
            if (result) result &= (args.NodeToFind is not null || args.ValueToFind is not null);

            return result;
        }

        private static IEnumerable<TNode> FindNodes(IGraphSearchAlgorithmArgs<TNode, TEdge, TValue> args)
        {
            var results = new List<TNode>();
            args.TraversalArgs.StartNode = args.StartNode;
            args.TraversalArgs.EndNode = args.NodeToFind;

            args.TraversalArgs.OnVisitNode = node =>
            {
                if (args.NodeToFind is not null)
                {
                    // node search
                    if (node.Equals(args.NodeToFind)) results.Add(node);
                }

                if (args.ValueToFind is null)
                {
                    args.TraversalArgs.ShouldStop = node => results.Count > 0;
                }
                else
                {
                    if (args.ValueToFindHasWildcards && typeof(TValue) == typeof(string))
                    {
                        // value search
                        if ((args.ValueToFind.ToString() ?? string.Empty).EqualsWildcard(node.Value.ToString() ?? string.Empty)) results.Add(node);
                    }
                    else
                    {
                        // wildcard string search
                        if (node.Value.CompareTo(args.ValueToFind) == 0) results.Add(node);
                    }
                }
            };


            return results.Distinct();
        }
    }
}