namespace VNet.DataStructures.Graph
{
    public class GraphValidationArgs : IGraphValidationArgs
    {
        public bool MustBeStandardGraph { get; set; }
        public bool MustBeHyperGraph { get; set; }
        public bool MustBeLineGraph { get; set; }
        public bool MustBeMultiOrParallelGraph { get; set; }
        public bool MustBeDirected { get; set; }
        public bool MustBeWeighted { get; set; }
        public bool MustHaveNegativeWeights { get; set; }
        public bool CannotBeStandardGraph { get; set; }
        public bool CannotBeHyperGraph { get; set; }
        public bool CannotBeLineGraph { get; set; }
        public bool CannotBeMultiOrParallelGraph { get; set; }
        public bool CannotBeDirected { get; set; }
        public bool CannotBeWeighted { get; set; }
        public bool CannotHaveNegativeWeights { get; set; }
    }
}