using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class watermelonCube : MonoBehaviour
{

    private bool isDetroyed = false;
    private bool watered;
    private int bp;
    private GameObject player;
    public float push;
    public Sprite wateredWatermelon;

    public ParticleSystem sparkles;
    public ParticleSystem slices;

    // Start is called before the first frame update
    void Start()
    {
        bp = 5;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (isDetroyed)
        {
            if (!this.GetComponent<AudioSource>().isPlaying)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("triggered");
        if (collision.tag == "water" && !watered)
        {
            Instantiate(sparkles, gameObject.transform.position, Quaternion.identity);
            //gameObject.transform.localScale += new Vector3(1, 1, 0);
            //Debug.Log("triggered water");
            //gameObject.GetComponent<Rigidbody2D>().constraints;
            gameObject.GetComponent<Rigidbody2D>().constraints &= ~RigidbodyConstraints2D.FreezePositionY;
            gameObject.GetComponent<Rigidbody2D>().constraints &= ~RigidbodyConstraints2D.FreezePositionX;
            watered = true;
            gameObject.GetComponent<Collider2D>().includeLayers = LayerMask.GetMask("characters");
            gameObject.GetComponent<Collider2D>().excludeLayers = 0;
            gameObject.GetComponent<SpriteRenderer>().sprite = wateredWatermelon;
        }
        if (collision.tag == "bombZone" && watered)
        {
            Exploded();
        }
        if(collision.tag == "sword" && watered)
        {
            Exploded();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        GameObject bullet = GameObject.FindGameObjectWithTag("bullet");

        if (collision.gameObject.tag == "bullet" && watered)
        {
            float dist = Vector3.Distance(player.transform.position, transform.position);
            // TOO CLOSE TO SELF
            if (dist <= 3.0f)
            {
                Exploded();
            }
            // BULLET COMING FROM UP, DOWN, LEFT, RIGHT
            else if (bullet.GetComponent<bullet>().getDirection() != new Vector3(0, 0, 1))
            {
                reduceBP();
                GetComponent<Rigidbody2D>().AddForce(bullet.GetComponent<bullet>().getDirection() * push * 0.1f,
                                                     ForceMode2D.Impulse);
            }
            // BULLET COMING DIAGONALLY
            else
            {
                reduceBP();
                Vector3 dir = collision.transform.position - transform.position;
                dir = -dir.normalized;
                GetComponent<Rigidbody2D>().AddForce(dir * push * 0.1f, ForceMode2D.Impulse);
            }
        }
    }

    // Function to reduce BP
    private void reduceBP()
    {
        bp--;
        if (bp == 0)
        {
            Exploded();
        }

        Debug.Log(bp);

    }

    // Function to explode watermelon
    private void Exploded()
    {
        Instantiate(slices, gameObject.transform.position, Quaternion.identity);
        this.GetComponent<SpriteRenderer>().enabled = false;
        this.GetComponent<AudioSource>().Play();
        isDetroyed = true;
    }

    // BP getter
    public int getBP()
    { 
        return bp;
    }
}
