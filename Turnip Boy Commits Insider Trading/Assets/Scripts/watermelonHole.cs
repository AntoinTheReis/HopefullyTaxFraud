using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class watermelonHole : MonoBehaviour
{

    private bool filled;
    private ContinuityHandler CH;
    public GameObject outside;
    public Sprite covered;

    // START function
    void Start()
    {
        CH = GameObject.FindGameObjectWithTag("Player").GetComponent<ContinuityHandler>();
    }

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
            CH.reportingSystem(this.tag);
        }
    }

    // Function to fill watermelon holes internally
    public void internalFill()
    {
        GetComponent<SpriteRenderer>().color = Color.green;
        GetComponent<Collider2D>().enabled = false;
        filled = true;
        Destroy(outside);
        GetComponent<SpriteRenderer>().sprite = covered;
    }

    // filled getter
    public bool getFilled() // (what an amazing function name lmao)
    {
        return filled;
    }
}
