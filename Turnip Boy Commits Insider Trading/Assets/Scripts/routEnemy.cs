using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class routEnemy : MonoBehaviour
{

    public Transform target1;
    public Transform target2;
    private Transform currentTarget;
    private bool even = false;
    public float push;

    [Header("Movement floats")]
    public float maxVel;
    public float vel;
    public float accel;



    // Start is called before the first frame update
    void Start()
    {
        transform.position = target1.position;
        currentTarget = target2;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, vel);
    }

    private void Update()
    {
        if(vel < maxVel)
        {
            vel += accel;
        }
        if(transform.position == currentTarget.position)
        {
            vel = 0f;
            if(!even)
            {
                even = true;
                currentTarget = target1;
            }
            else
            {
                even = false;
                currentTarget = target2;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "sword" || collision.tag == "bomb")
        {
            Vector3 dir = collision.transform.position - transform.position;
            dir = -dir.normalized;
            GetComponent<Rigidbody2D>().AddForce(dir * push);
        }
    }

}
