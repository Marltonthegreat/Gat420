using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchAgent : Agent
{
    private static bool pause = false;

    public GraphNode sourceNode { get; set; }
    public GraphNode destinationNode { get; set; }
    public Node targetNode { get; set; }

    List<GraphNode> path = new List<GraphNode>();

    public void Start()
    {
        sourceNode = Node.GetRandomNode<GraphNode>();
        destinationNode = Node.GetRandomNode<GraphNode>();

        targetNode = sourceNode;

        GeneratePath();
    }

    private void Update()
    {
        //Debug.DrawLine(transform.position, sourceNode.transform.position, Color.green);
        //Debug.DrawLine(transform.position, destinationNode.transform.position, Color.red);
        //Debug.DrawLine(transform.position + Vector3.up, targetNode.transform.position + Vector3.up, Color.yellow);

        if (Input.GetKeyDown(KeyCode.Space)) pause = !pause;
        if (targetNode != null && !pause) 
        {
            movement.MoveTowards(targetNode.transform.position);
        }
        else if(movement.velocity.sqrMagnitude != 0)
        {
            movement.velocity = Vector3.zero;
        }
    }

    public GraphNode GetNextNode(GraphNode graphNode)
    {
        if (path.Count == 0) return null;

        int index = path.FindIndex(node => node == graphNode);
        if (index == path.Count - 1)
        {
            sourceNode = destinationNode;
            do
            {
                destinationNode = Node.GetRandomNode<GraphNode>();
            } while (sourceNode == destinationNode);

            GeneratePath();
            index = 0;
        }

        GraphNode nextNode = path[index + 1];
        return nextNode;
    }

    private void GeneratePath()
    {
        GraphNode.ResetNodes();
        Search.BuildPath(Search.AStar, sourceNode, destinationNode, ref path);
    }
}
