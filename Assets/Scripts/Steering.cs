using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steering : MonoBehaviour
{
    [Range(0, 45)] public float wanderDisplacement = 5;
    [Range(0, 5)] public float wanderRadius = 3;
    [Range(0, 5)] public float wanderDistance = 1;

    float wanderAngle = 0;

    public Vector3 Seek(AutonomousAgent agent, GameObject target)
    {
        Vector3 force = CalculateSteering(agent, target.transform.position - agent.transform.position);
        return force;
    }
    
    public Vector3 Flee(AutonomousAgent agent, GameObject target)
    {
        Vector3 force = CalculateSteering(agent, agent.transform.position - target.transform.position);
        return force;
    }

    public Vector3 Wander(AutonomousAgent agent)
    {
        wanderAngle += Random.Range(-wanderDisplacement, wanderDisplacement);

        Quaternion rotation = Quaternion.AngleAxis(wanderAngle, Vector3.up);
        Vector3 point = rotation * (Vector3.forward * wanderRadius);
        Vector3 forward = agent.transform.forward * wanderDistance;

        Vector3 force = CalculateSteering(agent, forward + point);
        return force;
    }

    public Vector3 Cohesion(AutonomousAgent agent, GameObject[] neighbors)
    {
        Vector3 centerOfNeighbors = Vector3.zero;
        foreach (GameObject neighbor in neighbors)
        {
            centerOfNeighbors += neighbor.transform.position;
        }

        centerOfNeighbors /= neighbors.Length;

        Vector3 force = CalculateSteering(agent, centerOfNeighbors - agent.transform.position);
        return force;
    }

    public Vector3 Seperation(AutonomousAgent agent, GameObject[] neighbors, float radius)
    {
        Vector3 separation = Vector3.zero;
        foreach (GameObject neighbor in neighbors)
        {
            Vector3 direction = (agent.transform.position - neighbor.transform.position);
            if (direction.magnitude < radius)
            {
                separation += direction / direction.sqrMagnitude;
            }
        }

        Vector3 force = CalculateSteering(agent, separation);
        return force;
    }

    public Vector3 Alignment(AutonomousAgent agent, GameObject[] neighbors)
    {
        Vector3 averageVelocity = Vector3.zero;
        foreach (GameObject neighbor in neighbors)
        {
            averageVelocity += neighbor.GetComponent<AutonomousAgent>().velocity;
        }
        averageVelocity /= neighbors.Length;

        Vector3 force = CalculateSteering(agent, averageVelocity);
        return force;
    }

    public Vector3 CalculateSteering(AutonomousAgent agent, Vector3 vector)
    {
        Vector3 direction = vector.normalized;
        Vector3 desired = direction * agent.maxSpeed;
        Vector3 steer = desired - agent.velocity;
        Vector3 force = Vector3.ClampMagnitude(steer, agent.maxForce);

        return force;
    }
}
