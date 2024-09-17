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
        StartCoroutine(stall(collision));
    }

    // Function to stall OnTriggerEnter2D for exactly 2 frames
    IEnumerator stall(Collider2D collision)
    {
        for (int i = 0; i < 2; i++)
        {
            yield return null;
        }

        // OnTriggerEnter2D Proper
        GameObject watermelon = collision.gameObject;

        if (collision.tag == "watermelon" && !filled && watermelon.GetComponent<watermelonCube>().getBP() > 0)
        {
            GetComponent<SpriteRenderer>().color = Color.green;
            GetComponent<Collider2D>().enabled = false;
            Destroy(collision.gameObject);
            filled = true;
            this.GetComponent<AudioSource>().Play();
            Destroy(outside);
            GetComponent<SpriteRenderer>().sprite = covered;
        }
    }
}
