using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointAgent : SearchAgent
{
    // Start is called before the first frame update
    void Start()
    {
        targetNode = (initialNode != null) ? initialNode : WaypointNode.GetRandomWaypoint();
    }
}
