using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class consumableHeart : MonoBehaviour
{

    public float push;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 dir = GameObject.FindGameObjectWithTag("Player").transform.position - transform.position;
        dir = -dir.normalized;
        GetComponent<Rigidbody2D>().AddForce(dir * push);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<playeVenemy>().hp++;
            Destroy(gameObject);
        }
    }

}
