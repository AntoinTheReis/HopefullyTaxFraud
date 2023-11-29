using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playeVenemy : MonoBehaviour
{

    private static int hp = 6;
    private playerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<playerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            hp--;
            Vector3 dir = collision.gameObject.transform.position - transform.position;
            dir = -dir.normalized;
            GetComponent<Rigidbody2D>().AddForce(dir * playerMovement.bombPush);
            if (hp == 0)
            {
                Dead();
            }
        }
    }

    private void Dead()
    {
        Debug.Log("dead");
    }
}
