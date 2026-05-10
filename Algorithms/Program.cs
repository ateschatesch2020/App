using Algorithms;
using System;
using System.Collections.Generic;

class Program
{
    static List<int>[] graph;
    static bool[] visitedDFS;
    static bool[] visitedBFS;

    static void DFS(int node)
    {
        visitedDFS[node] = true;

        Console.WriteLine(node);

        foreach (int neighbor in graph[node])
        {
            if (!visitedDFS[neighbor])
            {
                DFS(neighbor);
            }
        }
    }
    static void BFS(int node)
    {
        Queue<int> values = new Queue<int>();

        values.Enqueue(node);
        visitedBFS[node] = true;

        while(values.Count() > 0)
        {
            node = values.Dequeue();
            Console.WriteLine(node);

            foreach (var item in graph[node])
            {
                if (!visitedBFS[item])
                {
                    visitedBFS[item] = true;
                    values.Enqueue(item);
                }
            }
        }
    }
    static void Main()
    {
        int n = 6;

        graph = new List<int>[n];
        visitedDFS = new bool[n];
        visitedBFS = new bool[n];

        for (int i = 0; i < n; i++)
        {
            graph[i] = new List<int>();
        }

        // Edge ekleme
        graph[0].Add(1);
        graph[0].Add(2);

        graph[1].Add(3);
        graph[1].Add(4);

        graph[2].Add(5);

        DFS(0);
        Console.WriteLine();
        BFS(0);

        Console.WriteLine();


        int[][] intervals1 =
        {
            new[] {1,2},
            new[] {1,3},
            new[] {2,5}
        };

        int[][] intervals2 =
        {
            new[] {1,2},
            new[] {2,5},
            new[] {5,9}
        };

        Console.WriteLine(MinimumMeetingRooms.MinClassrooms(intervals1)); // 2
        Console.WriteLine(MinimumMeetingRooms.MinClassrooms(intervals2)); // 1
        int[][] intervals3 =
        {
            new[]{1,4},
            new[]{1,3},
            new[] {2,6},
            new [] {3,4}
        }
        ;
        Console.WriteLine(MinimumMeetingRooms.MinClassrooms(intervals3));

        string s = "ADOBECODEBANC";
        string t = "ABC";

        Console.WriteLine(MinimumWindowSubstring.MinWindow(s, t));
    }
}