using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb : MonoBehaviour
{

    private bool watered;
    private bool watered2;
    private bool launched;

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
                Debug.Log("watered2");
            }
        }
    }

}
