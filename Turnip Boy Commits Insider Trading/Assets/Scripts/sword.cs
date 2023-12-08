using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sword : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SelfDestruct(1));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SelfDestruct(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        Destroy(gameObject);
    }
}
