using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class watermelonHole : MonoBehaviour
{

    private bool filled;
    public GameObject outside;

    public Sprite covered;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "watermelon" && !filled)
        {
            GetComponent<SpriteRenderer>().color = Color.green;
            GetComponent<Collider2D>().enabled= false;
            Destroy(collision.gameObject);
            filled = true;
            this.GetComponent<AudioSource>().Play();
            Destroy(outside);
            GetComponent<SpriteRenderer>().sprite= covered;
        }
    }

}
