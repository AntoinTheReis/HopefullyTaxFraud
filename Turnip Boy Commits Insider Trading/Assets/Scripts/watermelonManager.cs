using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class watermelonManager : MonoBehaviour
{
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
        Debug.Log("triggered");
        if (collision.tag == "water")
        {
            gameObject.transform.localScale += new Vector3(1, 1, 0);
            Debug.Log("triggered water");
        }
    }

}
