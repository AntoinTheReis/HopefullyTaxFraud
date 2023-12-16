using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyGet : MonoBehaviour
{
    [HideInInspector]
    public bool isKey = false;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.tag == "Key1" || col.tag == "Key2") && gameObject.name == "Radish Boy")
        {
            isKey = true;
            col.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            col.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
            
            if (col.tag == "Key1")
            {
                Key.keyGone1 = true;
            }
            else if (col.tag == "Key2")
            {
                Key.keyGone2 = true;
            }
        }
    }
}
