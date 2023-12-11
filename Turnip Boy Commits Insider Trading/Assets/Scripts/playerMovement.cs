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
    private bool higherUI = false;
    private float velocity;
    private bool[] boolArray = { false, false, false, false }; // Left, Right, Up, Down
    public bool usingItem;
    private float timeStill;
    [Header("Dash")]
    public float dashPush;
    public float dashTime;
    private bool inNpcRange = false;
    private bool dashing = false;
    [Header("True public")]
    public float acceleration;
    public float friction;
    public float maxVelocity;
    public float bombPush;
    [Header("Hat & BackItem Controller")]
    public SpriteRenderer backFaceRight;
    public SpriteRenderer backFaceLeft;
    public bool facingRight;
    public Animator animator;

    private float sleepTimer;

    // Update is called once per frame
    void FixedUpdate()
    {
        // Registering player input for movement
        if (!usingItem && !higherUI && !dashing)
        {
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
        }

        // Moving the player
        if (isMoving)
        {
            if (dashing)
            {
                animator.SetBool("Dashing", true);
            }
            else
            {
                animator.SetBool("Dashing", false);
                animator.SetBool("Walking", true);
            }
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
        else
        {
            if (dashing)
            {
                animator.SetBool("Dashing", true);
            }
            else
            {
                animator.SetBool("Dashing", false);
                animator.SetBool("Walking", false);
            }
            
        }

        resetVars();
    }

    private void Update()
    {
        if (!((Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.Z) || Input.GetKey("left") || Input.GetKey("right") || Input.GetKey("down") || Input.GetKey("up"))))
        {
            sleepTimer += Time.deltaTime;
            if (sleepTimer > 15)
            {
                animator.SetBool("Asleep", true);
            }
        }
        else
        {
            sleepTimer = 0;
            animator.SetBool("Asleep", false);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            usingItem = true;
            Invoke("itemDone", 1);
        }
        if (Input.GetKeyDown(KeyCode.Z) && !dashing)
        {
            if (!inNpcRange)
            {
                if (Input.GetKey("right"))
                {
                    if (Input.GetKey("up"))
                    {
                        Debug.Log("dash up right");
                        GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 1) * dashPush);
                        dashing = true;
                        animator.SetBool("Dashing", true);
                        Invoke("Dashing", dashTime);
                    }
                    else if (!Input.GetKey("up") && !Input.GetKey("down"))
                    {
                        Debug.Log("dash right");
                        GetComponent<Rigidbody2D>().AddForce(Vector2.right * dashPush);
                        dashing = true;
                        animator.SetBool("Dashing", true);
                        Invoke("Dashing", dashTime);
                    }
                    else if (Input.GetKey("down"))
                    {
                        Debug.Log("dash down right");
                        GetComponent<Rigidbody2D>().AddForce(new Vector2(1, -1) * dashPush);
                        dashing = true;
                        animator.SetBool("Dashing", true);
                        Invoke("Dashing", dashTime);
                    }
                }
                else if (!Input.GetKey("left") && !Input.GetKey("right"))
                {
                    if (Input.GetKey("up"))
                    {
                        Debug.Log("dash up");
                        GetComponent<Rigidbody2D>().AddForce(Vector2.up * dashPush);
                        dashing = true;
                        animator.SetBool("Dashing", true);
                        Invoke("Dashing", dashTime);
                    }
                    if (Input.GetKey("down"))
                    {
                        Debug.Log("dash down");
                        GetComponent<Rigidbody2D>().AddForce(Vector2.down * dashPush);
                        dashing = true;
                        animator.SetBool("Dashing", true);
                        Invoke("Dashing", dashTime);
                    }
                }
                else
                {
                    if (Input.GetKey("up"))
                    {
                        Debug.Log("dash left up");
                        GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, 1) * dashPush);
                        dashing = true;
                        animator.SetBool("Dashing", true);
                        Invoke("Dashing", dashTime);
                    }
                    else if (!Input.GetKey("up") && !Input.GetKey("down"))
                    {
                        Debug.Log("dash left");
                        GetComponent<Rigidbody2D>().AddForce(Vector2.left * dashPush);
                        dashing = true;
                        animator.SetBool("Dashing", true);
                        Invoke("Dashing", dashTime);
                    }
                    else if(Input.GetKey("down"))
                    {
                        Debug.Log("dash left down");
                        GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, -1) * dashPush);
                        dashing = true;
                        animator.SetBool("Dashing", true);
                        Invoke("Dashing", dashTime);
                    }
                }
            }
        }
    }

    // Accelerating
    private void accelerating()
    {
        velocity += acceleration;

        if (velocity > maxVelocity)
        {
            velocity = maxVelocity;
        }

        if (hozDirection > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
            facingRight = true;
            backFaceLeft.enabled = false;
            backFaceRight.enabled = true;
        }
        else if (hozDirection < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            facingRight = false;
            backFaceLeft.enabled = true;
            backFaceRight.enabled = false;
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
        for (int i = 0; i < 4; i++)
        {
            boolArray[i] = false;
        }
        vertDirection = 0;
        hozDirection = 0;
    }

    // For print debugging
    void LateUpdate()
    {
        //print(velocity);
    }

    // When player gets hit by a bomb
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "bombZone")
        {
            Debug.Log("hit by bomb");
            Vector3 dir = collision.transform.position - transform.position;
            dir = -dir.normalized;
            GetComponent<Rigidbody2D>().AddForce(dir * bombPush);
        }
        if (collision.tag == "npcRange")
        {
            inNpcRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "npcRange")
        {
            inNpcRange = false;
        }
    }

    // Flipping the usingItem boolean variable a second after the player uses an item
    private void itemDone()
    {
        usingItem = false;
    }

    // Setting the higherUI boolean variable to true or false depending on whether or not UI is on screen
    public void set_higherUI(bool UI_On)
    {
        higherUI = UI_On;
    }

    private void Dashing()
    {
        dashing = false;
        Debug.Log("Dashed");
    }
}
