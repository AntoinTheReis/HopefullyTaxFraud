using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heartLeaf : MonoBehaviour
{

    private bool isDestroyed = false;
    public GameObject heart;
    public ParticleSystem leavesParticle;

    // Update is called once per frame
    void Update()
    {
        if (isDestroyed)
        {
            if (!this.GetComponent<AudioSource>().isPlaying)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag  == "bombZone" || collision.tag == "sword")
        {
            Debug.Log("sword detected");
            Destroyed();
        }
    }

    public void Destroyed()
    {
        Instantiate(leavesParticle, gameObject.transform.position, Quaternion.identity);
        float randomFloat = Random.Range(0f, 1f);
        if(randomFloat > 0.5f)
        {
            Instantiate(heart, gameObject.transform.position, Quaternion.identity);
        }
        this.GetComponent<SpriteRenderer>().enabled = false;
        this.GetComponent<AudioSource>().Play();
        isDestroyed = true;
    }
}
