using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb : MonoBehaviour
{

    private bool watered;
    //private bool watered2;
    public bool launched;

    public GameObject bombZone;
    public float bombVelocity;
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "water")
        {
            if (!watered)
            {
                watered = true;
                Debug.Log("watered1");
            }
            else if(!launched)
            {
                //watered2 = true;
                Invoke("Explode", 3);
                Debug.Log("watered2");
            }
        }
        if(collision.tag == "sword")
        {
            if(watered && !launched)
            {
                launched = true;
                if(gameObject.transform.position.y < player.position.y)
                {
                    if(player.position.y - gameObject.transform.position.y > Mathf.Abs(gameObject.transform.position.x - player.position.x))
                    {
                        GetComponent<Rigidbody2D>().AddForce(Vector2.down * bombVelocity);
                    }
                    else if(gameObject.transform.position.x - player.position.x > 0)
                    {
                        GetComponent<Rigidbody2D>().AddForce(Vector2.right * bombVelocity);
                    }
                    else
                    {
                        GetComponent<Rigidbody2D>().AddForce(Vector2.left * bombVelocity);
                    }
                }
                else
                {
                    if (gameObject.transform.position.y - player.position.y > Mathf.Abs(gameObject.transform.position.x - player.position.x))
                    {
                        GetComponent<Rigidbody2D>().AddForce(Vector2.up * bombVelocity);
                    }
                    else if (gameObject.transform.position.x - player.position.x > 0)
                    {
                        GetComponent<Rigidbody2D>().AddForce(Vector2.right * bombVelocity);
                    }
                    else
                    {
                        GetComponent<Rigidbody2D>().AddForce(Vector2.left * bombVelocity);
                    }
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (launched)
        {
            Explode();
        }
    }

    private void Explode()
    {
        Instantiate(bombZone, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
