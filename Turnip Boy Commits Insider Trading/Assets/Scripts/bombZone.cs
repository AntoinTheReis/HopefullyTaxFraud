using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombZone : MonoBehaviour
{

    private AudioSource MP3player;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("PlayExplodeAudio", 0.05f);
        MP3player = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!MP3player.isPlaying && !this.GetComponent<CircleCollider2D>().enabled)
        {
            selfDestruct();
        }
    }

    private void PlayExplodeAudio()
    {
        this.GetComponent<CircleCollider2D>().enabled = false;
        MP3player.Play();
    }

    private void selfDestruct()
    {
        Destroy(gameObject);
    }

}
