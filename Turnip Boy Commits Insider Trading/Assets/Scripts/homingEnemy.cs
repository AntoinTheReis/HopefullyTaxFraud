using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class homingEnemy : MonoBehaviour
{

    private GameObject player;
    private bool attacking;
    private SpriteRenderer spriteRenderer;

    public ParticleSystem circle;
    public ParticleSystem skull;

    public float push;
    public float closeness;
    [Header("Movement floats")]
    public float maxVel;
    public float vel;
    public float accel;
    [Header("Dialogue_UI & Paper Objects")]
    public GameObject OBJ_Dialogue_UI;
    public GameObject OBJ_Ripping_UI;
    private int gp;
    public Animator animator;
    [Header("MF, don't touch this.")]
    public bool dying;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gp = 3;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!attacking && Vector3.Distance(gameObject.transform.position, player.transform.position) <= closeness)
        {
            attacking = true;
        }
        if (vel < maxVel && attacking && (!OBJ_Dialogue_UI.activeSelf || !OBJ_Ripping_UI.activeSelf))
        {
            vel += accel;
        }

        if(gameObject.transform.position.x < player.transform.position.x)
        {
            spriteRenderer.flipX= true;
        }
        else
        {
            spriteRenderer.flipX= false;
        }
    }

    private void FixedUpdate()
    {
        if(attacking && !dying)
        {
            if (!OBJ_Dialogue_UI.activeSelf || !OBJ_Ripping_UI.activeSelf)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, vel);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.tag == "sword" || collision.tag == "bombZone") && !dying)
        {
            Vector3 dir = collision.transform.position - transform.position;
            dir = -dir.normalized;
            GetComponent<Rigidbody2D>().AddForce(dir * push);
            this.GetComponent<AudioSource>().Play();
            dying = true;
            animator.SetTrigger("Dead");
            Invoke("Dying", 2);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "bullet" && !dying)
        {
            vel = 0;
            Vector3 dir = collision.transform.position - transform.position;
            dir = -dir.normalized;
            GetComponent<Rigidbody2D>().AddForce(dir * push * 0.1f);
            this.GetComponent<AudioSource>().Play();
            gp--;
            if (gp == 0)
            {
                //GetComponent<Rigidbody2D>().AddForce(dir * push * 0.9f);
                dying = true;
                animator.SetTrigger("Dead");
                Invoke("Dying", 2);
            }
            if (!attacking)
            {
                attacking = true;
            }
        }
    }

    private void Dying()
    {
        Quaternion rotation = Quaternion.Euler(-90, 0, 0);
        Instantiate(circle, gameObject.transform.position, Quaternion.identity);
        Instantiate(skull, gameObject.transform.position, rotation);
        Destroy(gameObject);
    }

}