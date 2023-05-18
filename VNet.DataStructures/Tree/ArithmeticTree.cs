using System.Collections;
using System.Globalization;

namespace VNet.DataStructures.Tree
{
    public class ArithmeticTree : IBinaryTree<string>
    {
        private ArithmeticTreeNode<string> _root;
        private int _size;

        public ArithmeticTree()
        {

        }

        public ArithmeticTree(ArithmeticTreeNode<string> pRoot, int pSize)
        {
            _root = pRoot;
            _size = pSize;
        }

        public IBinaryTree<string> Clone()
        {
            IBinaryTree<string> clone = new ArithmeticTree(this._root.Clone(null), this._size);
            return clone;
        }

        public double SolveTree()
        {
            return SolveLevel(_root.Clone(null));
        }

        private double SolveLevel(ArithmeticTreeNode<string> current)
        {
            double answer, answerLeft, answerRight;

            while (!double.TryParse(current.LeftNode.Data, NumberStyles.Any, CultureInfo.InvariantCulture, out answerLeft) ||
                   !double.TryParse(current.RightNode.Data, NumberStyles.Any, CultureInfo.InvariantCulture, out answerRight))
            {
                if (!double.TryParse(current.LeftNode.Data, NumberStyles.Any, CultureInfo.InvariantCulture, out answerLeft))
                    current = current.LeftNode;
                if (!double.TryParse(current.RightNode.Data, NumberStyles.Any, CultureInfo.InvariantCulture, out answerRight))
                    current = current.RightNode;
            }

            answer = GetCalulation(Double.Parse(current.LeftNode.Data.Replace('.', ',')), current.Data.ToString(), Double.Parse(current.RightNode.Data.Replace('.', ',')));

            current.Data = answer.ToString();
            current.RightNode = null;
            current.LeftNode = null;

            if (current.ParentNode == null)
                return answer;

            return SolveLevel(current.ParentNode);
        }

        private double GetCalulation(double nb1, string op, double nb2)
        {
            double answer = 0.0;

            switch (op)
            {
                case "+":
                    answer = nb1 + nb2;
                    break;
                case "-":
                    answer = nb1 - nb2;
                    break;
                case "*":
                    answer = nb1 * nb2;
                    break;
                case "/":
                    answer = nb1 / nb2;
                    break;
                case "^":
                    answer = Math.Pow(nb1, nb2);
                    break;
                default:
                    throw new Exception("Invalid operator");
            }

            return answer;
        }


        #region Implementation of IBinaryTree<T>
        public ArithmeticTreeNode<string> GetElement(int index)
        {
            return null;
        }

        public ArithmeticTreeNode<string> GetRoot()
        {
            return _root;
        }

        public bool IsEmpty()
        {
            return false;
        }

        public int Size()
        {
            return 0;
        }

        public int Height()
        {
            return 0;
        }

        public int NumberOfLeaves()
        {
            return 0;
        }
        #endregion


        #region Implementation of IEnumerable
        public IEnumerator<ArithmeticTreeNode<string>> GetEnumerator()
        {
            return new ArithmeticTreeInOrderTraversal<string>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public ArithmeticTreeInOrderTraversal<string> GetInOrderTraversalEnumerator()
        {
            return new ArithmeticTreeInOrderTraversal<string>(this);
        }
        #endregion
    }
}