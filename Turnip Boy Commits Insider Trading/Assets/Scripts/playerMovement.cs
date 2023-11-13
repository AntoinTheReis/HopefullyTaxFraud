using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{

    // Misc. variable declarations
    private int hozDirection;
    private int vertDirection;
    private int prevHozDirection;
    private int prevVertDirection;
    private bool isMoving;
    private float velocity;
    private bool[] boolArray = {false, false, false, false}; // Left, Right, Up, Down
    [Header("True public")]
    public float acceleration;
    public float friction;
    public float maxVelocity;

    // Update is called once per frame
    void Update()
    {
        // Registering player input
        if (Input.GetKey("left"))
        {
            hozDirection = -1;
            prevHozDirection = -1;
            miscInputRegistration(0);
        }
        else if (Input.GetKey("right"))
        {
            hozDirection = 1;
            prevHozDirection = 1;
            miscInputRegistration(1);
        }
        if (Input.GetKey("up"))
        {
            vertDirection = 1;
            prevVertDirection = 1;
            miscInputRegistration(2);
        }
        else if (Input.GetKey("down"))
        {
            vertDirection = -1;
            prevVertDirection = -1;
            miscInputRegistration(3);
        }

        // Moving the player
        if (isMoving)
        {
            // Resetting previous direction variables based on boolean array
            if (!boolArray[0] && !boolArray[1])
            {
                prevHozDirection = 0;
            }
            if (!boolArray[2] && !boolArray[3])
            {
                prevVertDirection = 0;
            }

            transform.position += new Vector3(hozDirection * velocity * Time.deltaTime, vertDirection * velocity * Time.deltaTime, 0);
        }
        // Slowing the player down to a stop
        else if (!isMoving && velocity > 0)
        {
            velocity -= friction;

            if (velocity < 0)
            {
                velocity = 0;
            }

            transform.position += new Vector3(prevHozDirection * velocity * Time.deltaTime, prevVertDirection * velocity * Time.deltaTime, 0);
        }
        resetVars();
    }

    // Accelerating
    private void accelerating()
    {
        velocity += acceleration;

        if (velocity > maxVelocity) 
        { 
            velocity = maxVelocity;
        }
    }

    // Method to consolidate redundancy in the input registration
    private void miscInputRegistration(int index)
    {
        isMoving = true;
        boolArray[index] = true;
        accelerating();
    }

    // Resetting variables in the Update loop
    private void resetVars()
    {
        isMoving = false;
        for (int i = 0; i < 4;  i++)
        {
            boolArray[i] = false;
        }
        vertDirection = 0;
        hozDirection = 0;
    }

    // For print debugging
    void LateUpdate()
    {
        print(velocity);
    }
}