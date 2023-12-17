using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public static bool keyGone1; // Level 3 Key
    public static bool keyGone2; // Level 5 Key

    private void Start()
    {
        if ((this.tag == "Key1" && keyGone1 == true) ||
            (this.tag == "Key2" && keyGone2 == true))
        {
            this.GetComponent<SpriteRenderer>().enabled = false;
            this.GetComponent<PolygonCollider2D>().enabled = false;
        }
    }
}
