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
    private bool isHozMoving;
    private bool isVertMoving;
    private int prevHozDirection;
    private int prevVertDirection;
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
            prevHozDirection = -1;
            isMoving = true;
            isHozMoving = true;
        }
        else if (Input.GetKey("right"))
        {
            hozDirection = 1;
            prevHozDirection = 1;
            isMoving = true;
            isHozMoving = true;
        }
        if (Input.GetKey("up"))
        {
            vertDirection = 1;
            prevVertDirection = 1;
            isMoving = true;
            isVertMoving = true;
        }
        else if (Input.GetKey("down"))
        {
            vertDirection= -1;
            prevVertDirection = -1;
            isMoving = true;
            isVertMoving = true;
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
            /*
            if (isHozMoving)
            {
                transform.position += new Vector3(hozDirection * velocity * Time.deltaTime, 0, 0);
            }
            else
            {
                transform.position += new Vector3(prevHozDirection * velocity * Time.deltaTime, 0, 0);
            }
            if (isVertMoving)
            {
                transform.position += new Vector3(0, vertDirection * velocity * Time.deltaTime, 0);
            }
            else
            {
                transform.position += new Vector3(0, prevVertDirection * velocity * Time.deltaTime, 0);
            }
            */

            if (isMoving)
            {
                transform.position += new Vector3(hozDirection * velocity * Time.deltaTime, vertDirection * velocity * Time.deltaTime, 0);
            }
            else
            {
                transform.position += new Vector3(prevHozDirection * velocity * Time.deltaTime, prevVertDirection * velocity * Time.deltaTime, 0);
            }
        }
        resetVars();
    }

    // Resetting variables for the Update loop
    private void resetVars()
    {
        isMoving = false;
        isHozMoving = false;
        isVertMoving = false;
    }

    // For print debugging
    void LateUpdate()
    {
        
    }
}
