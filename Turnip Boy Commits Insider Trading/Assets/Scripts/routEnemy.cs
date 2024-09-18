using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class routEnemy : MonoBehaviour
{

    public Transform target1;
    public Transform target2;
    private Transform currentTarget;
    private bool even = false;
    private bool iframesActive;
    public float push;

    public ParticleSystem circle;
    public ParticleSystem skull;

    [Header("Movement floats")]
    public float maxVel;
    public float vel;
    public float accel;
    private int gp;
    private SpriteRenderer spriteRender;

    [Header("MF, don't touch this.")]
    public bool dying;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = target1.position;
        currentTarget = target2;
        gp = 3;
        spriteRender = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!dying)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, vel);
        }
    }

    private void Update()
    {
        if(vel < maxVel)
        {
            vel += accel;
        }
        if(transform.position == currentTarget.position)
        {
            vel = 0f;
            if(!even)
            {
                spriteRender.flipX = true;
                even = true;
                currentTarget = target1;
            }
            else
            {
                spriteRender.flipX = false;
                even = false;
                currentTarget = target2;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "sword" && !dying && !iframesActive)
        {
            vel = 0;
            Vector3 dir = collision.transform.position - transform.position;
            dir = -dir.normalized;
            GetComponent<Rigidbody2D>().AddForce(dir * push * 0.5f);
            this.GetComponent<AudioSource>().Play();
            gp--;
            if (gp == 0)
            {
                gameObject.GetComponent<Animator>().SetTrigger("Dead");
                dying = true;
                Invoke("Dying", 2);
            }

            StartCoroutine(iFrames());
        }
        else if (collision.tag == "bombZone" && !dying)
        {
            Vector3 dir = collision.transform.position - transform.position;
            dir = -dir.normalized;
            GetComponent<Rigidbody2D>().AddForce(dir * push);
            this.GetComponent<AudioSource>().Play();
            gameObject.GetComponent<Animator>().SetTrigger("Dead");
            dying = true;
            Invoke("Dying", 2);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "bullet" && !dying)
        {
            Vector3 dir = collision.transform.position - transform.position;
            dir = -dir.normalized;
            GetComponent<Rigidbody2D>().AddForce(dir * push);
            this.GetComponent<AudioSource>().Play();
            gameObject.GetComponent<Animator>().SetTrigger("Dead");
            dying = true;
            Invoke("Dying", 2);
        }
    }

    private void Dying()
    {
        Quaternion rotation = Quaternion.Euler(-90, 0, 0);
        Instantiate(circle, gameObject.transform.position, Quaternion.identity);
        Instantiate(skull, gameObject.transform.position, rotation);
        Destroy(gameObject);
    }

    // iFrames function
    IEnumerator iFrames()
    {
        iframesActive = true;
        yield return new WaitForSecondsRealtime(1f);
        iframesActive = false;
    }
}
