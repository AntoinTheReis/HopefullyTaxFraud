using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{

    private bool isDestroyed = false;
    public float moveSpeed;

    // START function
    void Start()
    {
        this.GetComponent<AudioSource>().Play();
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
}
