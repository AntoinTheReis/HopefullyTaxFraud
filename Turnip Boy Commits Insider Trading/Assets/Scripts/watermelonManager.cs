using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class watermelonManager : MonoBehaviour
{

    private bool watered;
    private int bp;

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
            gameObject.transform.localScale += new Vector3(1, 1, 0);
            //Debug.Log("triggered water");
            //gameObject.GetComponent<Rigidbody2D>().constraints;
            gameObject.GetComponent<Rigidbody2D>().constraints &= ~RigidbodyConstraints2D.FreezePositionY;
            gameObject.GetComponent<Rigidbody2D>().constraints &= ~RigidbodyConstraints2D.FreezePositionX;
            watered = true;
            gameObject.GetComponent<Collider2D>().includeLayers = LayerMask.GetMask("characters");
            gameObject.GetComponent<Collider2D>().excludeLayers = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "bomb" && watered)
        {
            if(collision.collider.GetComponent<bomb>().launched == true)
            {
                Exploded();
            }
        }
        if(collision.gameObject.tag == "bullet" && watered)
        {
            bp--;
            if(bp == 0)
            {
                Exploded();
            }
        }
    }

    private void Exploded()
    {
        Destroy(gameObject);
    }

}
