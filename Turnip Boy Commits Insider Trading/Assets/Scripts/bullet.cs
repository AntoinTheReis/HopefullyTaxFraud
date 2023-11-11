using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{

    public float moveSpeed;

    private void FixedUpdate()
    {
        transform.Translate(moveSpeed * new Vector3(1,0,0));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

}
