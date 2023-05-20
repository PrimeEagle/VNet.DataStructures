﻿using VNet.DataStructures.Graph;

namespace VNet.DataStructures.Algorithms.Pathfinding
{
    // Kruskal's algorithm is a minimum-spanning-tree algorithm which finds an edge of the least possible weight that connects any two trees in the forest.
    // It is a greedy algorithm in graph theory as it finds a minimum spanning tree for a connected weighted graph adding increasing cost arcs at each step.
    // This means it finds a subset of the edges that forms a tree that includes every vertex, where the total weight of all the edges in the tree is minimized.
    // If the graph is not connected, then it finds a minimum spanning forest (a minimum spanning tree for each connected component).
    public class KruskalsPathfinding : IPathfindingAlgorithm
    {
        private int FindSubset(IList<Subset> subsets, int i)
        {
            if (subsets[i].Parent != i)
            {
                var temp = subsets[i];
                
                temp.Parent = FindSubset(subsets, subsets[i].Parent);
                subsets[i] = temp;
            }

            return subsets[i].Parent;
        }

        private void Union(IList<Subset> subsets, int x, int y)
        {
            int xRoot = FindSubset(subsets, x);
            int yRoot = FindSubset(subsets, y);

            if (subsets[xRoot].Rank < subsets[yRoot].Rank)
            {
                var temp = subsets[xRoot];
                temp.Parent = yRoot;
                subsets[xRoot] = temp;
            }
            else if (subsets[xRoot].Rank > subsets[yRoot].Rank)
            {
                var temp = subsets[yRoot];
                temp.Parent = xRoot;
                subsets[yRoot] = temp;
            }
            else
            {
                var temp = subsets[yRoot];
                temp.Parent = xRoot;
                subsets[yRoot] = temp;

                temp = subsets[xRoot];
                ++temp.Rank;
                subsets[xRoot] = temp;
            }
        }

        public int[] Find(Graph.Graph graph, int source = 0)
        {
            int verticesCount = graph.VertexCount;
            List<Edge> result = new List<Edge>();
            int i = 0;
            int e = 0;

            graph.Edges = graph.Edges.OrderBy(e => e.Weight).ToList();

            var subsets = new List<Subset>();

            for (int v = 0; v < verticesCount; ++v)
            {
                subsets.Add(new Subset());

                subsets[v].Parent = v;
                subsets[v].Rank = 0;
            }

            while (e < verticesCount - 1)
            {
                Edge nextEdge = graph.Edges[i++];
                int x = FindSubset(subsets, nextEdge.Source);
                int y = FindSubset(subsets, nextEdge.Destination);

                if (x != y)
                {
                    result.Add(nextEdge);
                    e++;
                    Union(subsets, x, y);
                }
            }

            return result.Select(r => r.Weight).ToArray();
        }
    }
}