using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{

    private bool isDestroyed = false;
    private Vector3 direction;
    public float moveSpeed;

    // START function
    void Start()
    {
        this.GetComponent<AudioSource>().Play();

        // Determining direction
            // LEFT
        if (this.transform.rotation.eulerAngles == new Vector3(0, 180, 0))
        {
            direction = new Vector3(-1, 0, 0);
        }
            // UP
        else if (this.transform.rotation.eulerAngles == new Vector3(0, 180, 90))
        {
            direction = new Vector3(0, 1, 0);
        }
            // RIGHT
        else if (this.transform.rotation.eulerAngles == new Vector3(0, 0, 0))
        {
            direction = new Vector3(1, 0, 0);
        }
            // DOWN
        else if (this.transform.rotation.eulerAngles == new Vector3(0, 0, 270))
        {
            direction = new Vector3(0, -1, 0);
        }
            // DIAGONAL
        else
        {
            direction = new Vector3(0, 0, 1);
        }
    }

    private void FixedUpdate()
    {
        if (!isDestroyed)
        {
            transform.Translate(moveSpeed * new Vector3(1, 0, 0));
        }
    }

    private void Update()
    {
        if (!this.GetComponent<AudioSource>().isPlaying && isDestroyed)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        this.GetComponent<SpriteRenderer>().enabled = false;
        isDestroyed = true;
    }

    // Direction getter
    public Vector3 getDirection()
    {
        return direction;
    }
}
