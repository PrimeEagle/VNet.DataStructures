using System.Collections;

namespace VNet.DataStructures.Graph.Algorithms.Optimization
{
    public class OptimizationSolution<TSolution> : IOptimizationSolution<TSolution>, IEnumerable<TSolution> where TSolution : notnull
    {
        public double Cost { get; set; }
        public List<TSolution> Solutions { get; init; }

        public int Count => Solutions.Count;

        public TSolution this[int index]
        {
            get => Solutions[index];
            set => Solutions[index] = value;
        }

        public OptimizationSolution()
        {
            Solutions = new List<TSolution>();
        }

        public OptimizationSolution(TSolution solution)
        {
            Solutions = new List<TSolution> {solution};
        }

        public OptimizationSolution(IOptimizationSolution<TSolution> solution)
        {
            Solutions = new List<TSolution>(solution.Solutions);
        }

        public OptimizationSolution(IEnumerable<TSolution> solutions)
        {
            Solutions = new List<TSolution>(solutions);
        }

        public IEnumerator<TSolution> GetEnumerator()
        {
            return Solutions.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Solutions.GetEnumerator();
        }
    }
}