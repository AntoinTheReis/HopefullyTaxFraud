using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb : MonoBehaviour
{

    private bool watered;
    private bool watered2;
    private bool launched;

    public GameObject bombZone;

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
        if(collision.tag == "water")
        {
            if (!watered)
            {
                watered = true;
                Debug.Log("watered1");
            }
            else if(!launched)
            {
                watered2 = true;
                Invoke("Explode", 3);
                Debug.Log("watered2");
            }
        }
        if(collision.tag == "sword")
        {
            if(watered)
            {
                launched = true;
            }
        }
    }

    private void Explode()
    {
        Instantiate(bombZone, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
