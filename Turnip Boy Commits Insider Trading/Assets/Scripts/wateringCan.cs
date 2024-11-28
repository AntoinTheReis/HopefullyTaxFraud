using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wateringCan : MonoBehaviour
{

    private float shootingWaitingTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SelfDestruct(shootingWaitingTime));
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
