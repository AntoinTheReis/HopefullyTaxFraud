using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toolManager : MonoBehaviour
{

    public float counter = 0;
    public canManager canManager;
    public swordManager swordManager;
    public gunManager gunManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            counter--;
            if(counter<0)
            {
                counter = 2;
            }
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            counter++;
            if(counter > 2)
            {
                counter = 0;
            }
        }
    }
}
