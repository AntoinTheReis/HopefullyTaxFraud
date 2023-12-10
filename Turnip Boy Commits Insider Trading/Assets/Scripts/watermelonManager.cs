using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class watermelonManager : MonoBehaviour
{

    private bool watered;
    private int bp;
    public float push;
    public Sprite wateredWatermelon;

    public ParticleSystem sparkles;
    public ParticleSystem slices;

    // Start is called before the first frame update
    void Start()
    {
        bp = 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("triggered");
        if (collision.tag == "water" && !watered)
        {
            Instantiate(sparkles, gameObject.transform.position, Quaternion.identity);
            gameObject.transform.localScale += new Vector3(1, 1, 0);
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
        
        if(collision.gameObject.tag == "bullet" && watered)
        {
            Vector3 dir = collision.transform.position - transform.position;
            dir = -dir.normalized;
            GetComponent<Rigidbody2D>().AddForce(dir * push * 0.1f);
            bp--;
            if(bp == 0)
            {
                Exploded();
            }
        }
    }

    private void Exploded()
    {
        Instantiate(slices, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
