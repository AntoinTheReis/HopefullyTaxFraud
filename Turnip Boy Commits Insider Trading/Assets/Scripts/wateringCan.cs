using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wateringCan : MonoBehaviour
{
    canManager SwordManager;

    // Start is called before the first frame update
    void Start()
    {
        SwordManager = GetComponentInParent<canManager>();
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
