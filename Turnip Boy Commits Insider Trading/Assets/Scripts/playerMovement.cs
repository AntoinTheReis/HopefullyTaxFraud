using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{

    // Misc. variable declarations
    private int hozDirection;
    private int vertDirection;
    private float velocity;
    private bool isMoving;
    [Header("True public")]
    public float acceleration;
    public float friction;
    public float maxVelocity;

    // Update is called once per frame
    void Update()
    {
        // Registering player inputs
        if (Input.GetKey("left"))
        {
            hozDirection = -1;
            isMoving = true;
        }
        else if (Input.GetKey("right"))
        {
            hozDirection = 1;
            isMoving = true;
        }
        if (Input.GetKey("up"))
        {
            vertDirection = 1;
            isMoving = true;
        }
        else if (Input.GetKey("down"))
        {
            vertDirection= -1;
            isMoving = true;
        }

        // Increase acceleration unless velocity == maxVelocity
        if (isMoving)
        {
            velocity += acceleration;
            
            if (velocity > maxVelocity)
            {
                velocity = maxVelocity;
            }
        }
        // Slow player to a stop
        else if (!isMoving && velocity > 0)
        {
            velocity -= friction;

            if (velocity < 0) 
            { 
                velocity = 0;
            }
        }

        // Moving the player
        if (velocity > 0)
        {
            transform.position += new Vector3(hozDirection * velocity * Time.deltaTime, vertDirection * velocity * Time.deltaTime, 0);
        }
        // Reset isMoving
        isMoving = false;
    }

    // For print debugging
    void LateUpdate()
    {
        print (velocity);
    }
}
