using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombZone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("selfDestruct", 0.05f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void selfDestruct()
    {
        Destroy(gameObject);
    }

}
