using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Priority_Queue;

public static class Search
{
    public delegate bool SearchAlgorithm(GraphNode source, GraphNode destination, ref List<GraphNode> path, int maxSteps);

    static public bool BuildPath(SearchAlgorithm searchAlgorithm, GraphNode source, GraphNode destination, ref List<GraphNode> path, int steps = int.MaxValue)
    {
        if (source == null || destination == null) return false;

        // reset graph nodes
        GraphNode.ResetNodes();

        // search for path from source to destination nodes        
        bool found = searchAlgorithm(source, destination, ref path, steps);

        return found;
    }

    public static void CreatePathFromParents(GraphNode node, ref List<GraphNode> path)
    {
        // while node not null
        while (node != null)
        {
            // add node to list path
            path.Add(node);
            // set node to node parent
            node = node.parent;
        }

        // reverse path
        path.Reverse();
    }

    public static bool DFS(GraphNode source, GraphNode destination, ref List<GraphNode> path, int maxSteps)
    {
        bool found = false;

        var nodes = new Stack<GraphNode>();
        nodes.Push(source);

        int steps = 0;
        while (!found && nodes.Count > 0 && steps++ < maxSteps)
        {
            var node = nodes.Peek();
            node.visited = true;

            bool forward = false;

            foreach(var neighbor in node.neighbors)
            {
                if (!neighbor.visited)
                {
                    nodes.Push(neighbor);
                    forward = true;

                    if (neighbor == destination)
                    {
                        found = true;
                    }

                    break;
                }
            }

            if (!forward)
            {
                nodes.Pop();
            }
        }

        path = new List<GraphNode>(nodes);
        path.Reverse();

        return found;
    }

    public static bool BFS(GraphNode source, GraphNode destination, ref List<GraphNode> path, int maxSteps)
    {
        bool found = false;

        // create queue of graph nodes
        var nodes = new LinkedList<GraphNode>();

        // set source node visited to true
        source.visited = true;
        // enqueue source node
        nodes.AddLast(source);

        // set the current number of steps
        int steps = 0;
        while (!found && nodes.Count > 0 && steps++ < maxSteps)
        {
            // dequeue node
            var node = nodes.First();
            nodes.RemoveFirst();
            // iterate through neighbors of node
            foreach (var neighbor in node.neighbors)
            {
                // check if neighbor has not been visited
                if (!neighbor.visited)
                {
                    // set neighbor visited to true
                    neighbor.visited = true;
                    // set neighbor parent to node
                    neighbor.parent = node;
                    // enqueue neighbor
                    nodes.AddLast(neighbor);
        
                }
                // check if neighbor is the destination node
                if (neighbor.Equals(destination))
		        {
                    // set found to true
                    found = true;
                    break;
                }
            }
        }

        if (found)
        {
            // create path from destination to source using node parents
            path = new List<GraphNode>();
            CreatePathFromParents(destination, ref path);
        }
        else
        {
            // did not find destination, convert nodes queue to path
            path = nodes.ToList();
        }

        return found;

    }
}