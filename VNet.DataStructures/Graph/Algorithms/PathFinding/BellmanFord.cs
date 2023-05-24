﻿namespace VNet.DataStructures.Graph.Algorithms.PathFinding;
// Bellman–Ford algorithm, also known as Bellman–Ford–Moore algorithm, is an algorithm that computes shortest paths from a single source vertex to all
// of the other vertices in a weighted digraph. It is slower than Dijkstra's algorithm for the same problem, but more versatile, as it is capable of
// handling graphs in which some of the edge weights are negative numbers..

public class BellmanFord
{
    public int[] Find(Basic.Graph graph, int source = 0)
    {
        var vertices = graph.VertexCount;
        var distances = new int[vertices];
        for (var i = 0; i < vertices; ++i) distances[i] = int.MaxValue;
        distances[0] = 0;

        for (var i = 1; i < vertices; ++i)
            foreach (var edge in graph.Edges)
            {
                var u = edge.Source;
                var v = edge.Destination;
                var weight = edge.Weight;
                if (distances[u] != int.MaxValue && distances[u] + weight < distances[v]) distances[v] = distances[u] + weight;
            }

        // Check for negative-weight cycles
        var hasNegativeWeightCycle = false;
        foreach (var edge in graph.Edges)
        {
            var u = edge.Source;
            var v = edge.Destination;
            var weight = edge.Weight;
            if (distances[u] != int.MaxValue && distances[u] + weight < distances[v])
            {
                hasNegativeWeightCycle = true;
                break;
            }
        }

        if (hasNegativeWeightCycle)
        {
            Console.WriteLine("Graph contains a negative-weight cycle");
            return null;
        }

        return distances;
    }
}