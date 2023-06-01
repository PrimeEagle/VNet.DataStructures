namespace VNet.DataStructures.Graph.Algorithms.Optimization;

public class VehicleReschedulingStandardGraphOptimization<TNode, TEdge, TValue> : IGraphOptimizationAlgorithm<TNode, TEdge, TValue, OptimizationSolution<Path<TNode, TValue>>>
    where TNode : notnull, INode<TValue>
    where TEdge : notnull, IStandardEdge<TNode, TValue>
    where TValue : notnull, IComparable<TValue>
{
    public OptimizationSolution<Path<TNode, TValue>> Solve(IGraphOptimizationAlgorithmArgs<TNode, TEdge, TValue> args)
    {
        throw new NotImplementedException("Use the overload Solve(IGraphVehicleReschedulingOptimizationAlgorithmArgs<TNode, TEdge, TValue> args) instead.");
    }

    public OptimizationSolution<Path<TNode, TValue>> Solve(IGraphVehicleReschedulingOptimizationAlgorithmArgs<TNode, TEdge, TValue, OptimizationSolution<Path<TNode, TValue>>> args)
    {
        args.Graph.Validate(new GraphValidationArgs() { MustBeStandardGraph = true });

        var currentSolution = new OptimizationSolution<Path<TNode, TValue>>(args.InitialRoutes);

        while (true)
        {
            var neighboringSolutions = GenerateNeighboringSolutions(currentSolution);

            foreach (var solution in neighboringSolutions) solution.Cost = EvaluateCost(args, solution);

            // Find the best neighboring solution
            var bestNeighbor = neighboringSolutions.MinBy(s => s.Cost);

            if (bestNeighbor == null || bestNeighbor.Cost >= currentSolution.Cost)
                break; // No better solution found, terminate the algorithm

            currentSolution = bestNeighbor;
        }

        return currentSolution;
    }

    private List<OptimizationSolution<Path<TNode, TValue>>> GenerateNeighboringSolutions(IOptimizationSolution<Path<TNode, TValue>> solution)
    {
        var neighboringSolutions = new List<OptimizationSolution<Path<TNode, TValue>>>();

        for (var i = 0; i < solution.Count; i++)
            for (var j = i + 1; j < solution.Count; j++)
            {
                // Swap two nodes in the solution
                var newSolution = new OptimizationSolution<Path<TNode, TValue>>(solution);
                (newSolution[i], newSolution[j]) = (newSolution[j], newSolution[i]);

                neighboringSolutions.Add(newSolution);
            }

        return neighboringSolutions;
    }

    private double EvaluateCost(IGraphVehicleReschedulingOptimizationAlgorithmArgs<TNode, TEdge, TValue, OptimizationSolution<Path<TNode, TValue>>> args, OptimizationSolution<Path<TNode, TValue>> solution)
    {
        var totalCost = 0.0;
        for (var i = 0; i < solution.Count; i++)
        {
            var currentNodeList = solution[i];
            var nextNodeList = i + 1 < solution.Count ? solution[i + 1] : solution[0];

            foreach (var currentNode in currentNodeList.Nodes)
                foreach (var nextNode in nextNodeList.Nodes)
                {
                    var edges = args.Graph[currentNode];
                    var edge = edges.First(e => e.EndNode.Equals(nextNode));

                    var weight = 1.0d;
                    if (args.Graph.IsWeighted) weight = ((IWeightedStandardEdge<TNode, TValue>)edge).Weight;
                    totalCost += weight;
                }
        }

        return totalCost;
    }
}