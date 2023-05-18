using System.Collections;

namespace VNet.DataStructures.Tree
{
    public class Tree<T> : IEnumerable<T> where T : IComparable
    {
        private TreeNode<T> Root { get; set; }

        public int Count { get; private set; }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new TreeEnumerator<T>(Root);
        }

        public bool HasItem(T value)
        {
            if (Root == null) return false;

            return Find(Root, value) != null;
        }

        public int GetHeight()
        {
            return GetHeight(Root);
        }

        public void Insert(T parent, T child)
        {
            if (Root == null)
            {
                Root = new TreeNode<T>(null, child);
                Count++;
                return;
            }

            var parentNode = Find(parent);

            if (parentNode == null) throw new ArgumentNullException();

            var exists = Find(Root, child) != null;

            if (exists) throw new ArgumentException("value already exists");

            parentNode.Children.InsertFirst(new TreeNode<T>(parentNode, child));
            Count++;
        }

        public void Delete(T value)
        {
            Delete(Root.Value, value);
        }

        public IEnumerable<T> Children(T value)
        {
            return Find(value)?.Children.Select(x => x.Value);
        }

        private TreeNode<T> Find(T value)
        {
            if (Root == null) return null;

            return Find(Root, value);
        }

        private int GetHeight(TreeNode<T> node)
        {
            if (node == null) return -1;

            var currentHeight = -1;

            foreach (var child in node.Children)
            {
                var childHeight = GetHeight(child);

                if (currentHeight < childHeight) currentHeight = childHeight;
            }

            currentHeight++;

            return currentHeight;
        }

        private void Delete(T parentValue, T value)
        {
            var parent = Find(parentValue);

            if (parent == null) throw new Exception("Cannot find parent");

            var itemToRemove = Find(parent, value);

            if (itemToRemove == null) throw new Exception("Cannot find item");


            if (itemToRemove.Parent == null)
            {
                if (itemToRemove.Children.Count() == 0)
                {
                    Root = null;
                }
                else
                {
                    if (itemToRemove.Children.Count() == 1)
                    {
                        Root = itemToRemove.Children.DeleteFirst();
                        Root.Parent = null;
                    }
                    else
                    {
                        throw new Exception("Node have multiple children. Cannot delete node unambiguosly");
                    }
                }
            }
            else
            {
                if (itemToRemove.Children.Count() == 0)
                {
                    itemToRemove.Parent.Children.Delete(itemToRemove);
                }
                else
                {
                    if (itemToRemove.Children.Count() == 1)
                    {
                        var orphan = itemToRemove.Children.DeleteFirst();
                        orphan.Parent = itemToRemove.Parent;

                        itemToRemove.Parent.Children.InsertFirst(orphan);
                        itemToRemove.Parent.Children.Delete(itemToRemove);
                    }
                    else
                    {
                        throw new Exception("Node have multiple children. Cannot delete node unambiguosly");
                    }
                }
            }

            Count--;
        }

        private TreeNode<T> Find(TreeNode<T> parent, T value)
        {
            if (parent.Value.CompareTo(value) == 0) return parent;

            foreach (var child in parent.Children)
            {
                var result = Find(child, value);

                if (result != null) return result;
            }

            return null;
        }
    }
}
