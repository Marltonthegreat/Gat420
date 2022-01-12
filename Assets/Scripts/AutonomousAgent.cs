using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutonomousAgent : Agent
{
    [SerializeField] Perception perception;
    [SerializeField] Steering steering;

    public float maxSpeed;
    public float maxForce;
    public bool seek = true;
    public Vector3 velocity { get; set; } = Vector3.zero;

    private void Update()
    {
        Vector3 acceleration = Vector3.zero;
        Vector3 force;

        GameObject[] gameObjects = perception.GetGameObjects();


        if (gameObjects.Length != 0)
        {
            Debug.DrawLine(transform.position, gameObjects[0].transform.position);


            if (seek)
            {
                force = steering.Seek(this, gameObjects[0]);
            }
            else
            {
                force = steering.Flee(this, gameObjects[0]);
            }

        }
        else
        {
            force = steering.Wander(this);            
        }
        
        acceleration += force;

        velocity += acceleration * Time.deltaTime;
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        transform.position += velocity * Time.deltaTime;

        if (velocity.sqrMagnitude > 0.1f)
        {
            transform.rotation = Quaternion.LookRotation(velocity);
        }

        transform.position = Utilities.Wrap(transform.position, new Vector3(-20, -20, -20), new Vector3(20, 20, 20));
    }
}
