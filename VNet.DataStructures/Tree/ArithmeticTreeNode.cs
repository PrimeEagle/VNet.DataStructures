namespace VNet.DataStructures.Tree
{
    public class ArithmeticTreeNode<T>
    {
        public T Data { get; set; }
        public ArithmeticTreeNode()
        {

        }
        public ArithmeticTreeNode(T data)
        {
            Data = data;
        }
        public ArithmeticTreeNode<T> ParentNode { get; set; }
        public ArithmeticTreeNode<T> LeftNode { get; set; }
        public ArithmeticTreeNode<T> RightNode { get; set; }

        public IEnumerable<ArithmeticTreeNode<T>> GetChildren(ArithmeticTreeNode<T> node)
        {
            throw new NotImplementedException();
        }

        public bool IsInternal()
        {
            throw new NotImplementedException();
        }

        public bool IsExternal()
        {
            throw new NotImplementedException();
        }

        public ArithmeticTreeNode<T> Clone(ArithmeticTreeNode<T> parent)
        {
            ArithmeticTreeNode<T> clone = new ArithmeticTreeNode<T>();
            clone.ParentNode = parent;
            clone.Data = this.Data;
            clone.LeftNode = null;
            clone.RightNode = null;

            if (this.LeftNode != null)
                clone.LeftNode = this.LeftNode.Clone(clone);

            if (this.RightNode != null)
                clone.RightNode = this.RightNode.Clone(clone);

            return clone;
        }
    }
}