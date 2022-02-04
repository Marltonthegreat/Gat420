using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointAgent : SearchAgent
{
    [SerializeField] WaypointNode initialNode;

    void Start()
    {
        //targetNode = (initialNode != null) ? initialNode : WaypointNode.GetRandomWaypoint();
        targetNode = (initialNode != null) ? initialNode : Node.GetRandomNode<WaypointNode>();
    }
}
