using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchAgent : Agent
{
    [SerializeField] protected Node initialNode;
    public Node targetNode { get; set; }
    private static bool pause = false;

    public void Start()
    {
        targetNode = initialNode;
    }

    private void Update()
    {
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
}
