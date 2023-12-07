using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class watermelonLeaf : MonoBehaviour
{

    public GameObject watermelonPrefab;
    public GameObject watermelon;

    // Start is called before the first frame update
    void Start()
    {
        watermelon = Instantiate(watermelonPrefab, gameObject.transform);
    }

    // Update is called once per frame
    void Update()
    {
        if(watermelon == null)
        {
            watermelon = Instantiate(watermelonPrefab, gameObject.transform);
        }
    }
}
