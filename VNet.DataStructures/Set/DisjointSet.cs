using System.Collections;

namespace VNet.DataStructures.Set
{
    public class DisjointSet<T> : IEnumerable<T>
    {
        private readonly Dictionary<T, DisjointSetNode<T>> set = new();

        public int Count { get; private set; }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return set.Values.Select(x => x.Data).GetEnumerator();
        }

        public void MakeSet(T member)
        {
            if (set.ContainsKey(member)) throw new Exception("A set with given member already exists.");

            var newSet = new DisjointSetNode<T>
            {
                Data = member,
                Rank = 0
            };

            newSet.Parent = newSet;
            set.Add(member, newSet);

            Count++;
        }

        public T FindSet(T member)
        {
            if (!set.ContainsKey(member)) throw new Exception("No such set with given member.");

            return FindSet(set[member]).Data;
        }

        private DisjointSetNode<T> FindSet(DisjointSetNode<T> node)
        {
            var parent = node.Parent;

            if (node != parent)
            {
                node.Parent = FindSet(node.Parent);
                return node.Parent;
            }

            return parent;
        }

        public void Union(T memberA, T memberB)
        {
            var rootA = FindSet(memberA);
            var rootB = FindSet(memberB);

            if (rootA.Equals(rootB)) return;

            var nodeA = set[rootA];
            var nodeB = set[rootB];

            if (nodeA.Rank == nodeB.Rank)
            {
                nodeB.Parent = nodeA;
                nodeA.Rank++;
            }
            else
            {
                if (nodeA.Rank < nodeB.Rank)
                    nodeA.Parent = nodeB;
                else
                    nodeB.Parent = nodeA;
            }
        }
    }
}