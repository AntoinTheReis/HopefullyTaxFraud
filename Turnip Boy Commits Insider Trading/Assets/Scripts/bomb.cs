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
            }
            else
            {
                watered2 = true;
            }
        }
    }

}
