using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sword : MonoBehaviour
{

    swordManager SwordManager;

    // Start is called before the first frame update
    void Start()
    {
        SwordManager = GetComponentInParent<swordManager>();
        StartCoroutine(SelfDestruct(SwordManager.shootingWaitingTime));
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
