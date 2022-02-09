using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentCamera : MonoBehaviour
{
    Camera followCamera;

    void Start()
    {
        followCamera = Camera.main;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
		{
            Agent[] agents = Agent.GetAgents<Agent>();
            if (agents.Length == 0) return;

            followCamera.transform.parent = agents[Random.Range(0, agents.Length)].transform;
            followCamera.transform.localPosition = new Vector3(1, 1, -2.5f);
            followCamera.transform.localRotation = Quaternion.AngleAxis(10, Vector3.right);
		}
    }
}
