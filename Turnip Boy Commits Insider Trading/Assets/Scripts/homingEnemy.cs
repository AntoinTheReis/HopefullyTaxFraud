using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class homingEnemy : MonoBehaviour
{

    private GameObject player;
    private bool attacking;

    public float push;
    public float closeness;
    [Header("Movement floats")]
    public float maxVel;
    public float vel;
    public float accel;
    [Header("Dialogue_UI Object")]
    public GameObject OBJ_Dialogue_UI;
    private int gp;
    private bool dying;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gp = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if(!attacking && Vector3.Distance(gameObject.transform.position, player.transform.position) <= closeness)
        {
            attacking = true;
        }
        if (vel < maxVel && attacking && !OBJ_Dialogue_UI.activeSelf)
        {
            vel += accel;
        }
    }

    private void FixedUpdate()
    {
        if(attacking && !dying)
        {
            Debug.Log("Supposed to move");
            if (OBJ_Dialogue_UI.activeSelf == false)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, vel);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.tag == "sword" || collision.tag == "bomb") && !dying)
        {
            Vector3 dir = collision.transform.position - transform.position;
            dir = -dir.normalized;
            GetComponent<Rigidbody2D>().AddForce(dir * push);
            dying = true;
            Invoke("Dying", 2);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "bullet" && !dying)
        {
            vel = 0;
            Vector3 dir = collision.transform.position - transform.position;
            dir = -dir.normalized;
            GetComponent<Rigidbody2D>().AddForce(dir * push * 0.1f);
            gp--;
            if (gp == 0)
            {
                //GetComponent<Rigidbody2D>().AddForce(dir * push * 0.9f);
                dying = true;
                Invoke("Dying", 2);
            }
            if (!attacking)
            {
                attacking = true;
            }
        }
    }

    private void Dying()
    {
        Destroy(gameObject);
    }

}
