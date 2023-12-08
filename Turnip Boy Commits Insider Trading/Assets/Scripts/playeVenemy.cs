using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playeVenemy : MonoBehaviour
{

    public float hp = 3;
    public float shakeIntensity = 0.4f;
    public float shakeDuration = 0.15f;
    private playerMovement playerMovement;
    private CameraShakeManager shakeManager;

    // Start is called before the first frame update
    void Start()
    {
        shakeManager = GameObject.FindGameObjectWithTag("VCM").GetComponent<CameraShakeManager>();
        playerMovement = GetComponent<playerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hp > 3)
        {
            hp = 3;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            shakeManager.ShakeCamera(shakeDuration, shakeIntensity);
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
