using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heartLeaf : MonoBehaviour
{

    public GameObject heart;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        float randomFloat = Random.Range(0f, 1f);
        if(randomFloat > 0.5f)
        {
            Instantiate(heart, gameObject.transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

}
