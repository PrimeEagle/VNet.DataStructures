namespace VNet.DataStructures.Graph.Algorithms.Optimization
{
    public interface IOptimizationSolution<TSolution> : IEnumerable<TSolution> where TSolution : notnull
    {
        public double Cost { get; set; }
        public List<TSolution> Solutions { get; init; }

        public int Count => Solutions.Count;

        public new IEnumerator<TSolution> GetEnumerator();
    }
}