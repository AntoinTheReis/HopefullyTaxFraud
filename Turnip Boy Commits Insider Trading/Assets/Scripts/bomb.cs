using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb : MonoBehaviour
{

    private bool watered;
    private bool watered2;
    public bool launched;

    public GameObject bombZone;
    public float bombVelocity;
    private Transform player;

    public Animator animator;

    public float shakeIntensity = 1f;
    public float shakeDuration = 0.2f;
    private CameraShakeManager shakeManager;

    public ParticleSystem circle;
    public ParticleSystem mushroom;
    public ParticleSystem sparkles;

    // Start is called before the first frame update
    void Start()
    {
        shakeManager = GameObject.FindGameObjectWithTag("VCM").GetComponent<CameraShakeManager>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "water")
        {
            if (!watered)
            {
                Instantiate(sparkles, gameObject.transform.position, Quaternion.identity);
                watered = true;
                Debug.Log("watered1");
                animator.SetBool("Watered", true);
                gameObject.GetComponent<Collider2D>().includeLayers = LayerMask.GetMask("characters");
                gameObject.GetComponent<Collider2D>().excludeLayers = 0;
            }
            else if(!launched)
            {
                watered2 = true;
                Invoke("Explode", 3);
                Debug.Log("watered2");
                animator.SetBool("StationaryExplosion", true);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (launched)
        {
            Explode();
        }
        if(watered && collision.gameObject.tag == "bullet")
        {
            Explode();
        }
        if(collision.gameObject.tag == "Player")
        {
            if (watered && !launched && !watered2)
            {
                GetComponent<Rigidbody2D>().constraints &= ~RigidbodyConstraints2D.FreezePositionY;
                GetComponent<Rigidbody2D>().constraints &= ~RigidbodyConstraints2D.FreezePositionX;
                launched = true;
                if (gameObject.transform.position.y < player.position.y)
                {
                    if (player.position.y - gameObject.transform.position.y > Mathf.Abs(gameObject.transform.position.x - player.position.x))
                    {
                        GetComponent<Rigidbody2D>().AddForce(Vector2.down * bombVelocity);
                        animator.SetBool("LaunchedRight", true);
                    }
                    else if (gameObject.transform.position.x - player.position.x > 0)
                    {
                        GetComponent<Rigidbody2D>().AddForce(Vector2.right * bombVelocity);
                        animator.SetBool("LaunchedRight", true);
                    }
                    else
                    {
                        GetComponent<Rigidbody2D>().AddForce(Vector2.left * bombVelocity);
                        animator.SetBool("LaunchedLeft", true);
                    }
                }
                else
                {
                    if (gameObject.transform.position.y - player.position.y > Mathf.Abs(gameObject.transform.position.x - player.position.x))
                    {
                        GetComponent<Rigidbody2D>().AddForce(Vector2.up * bombVelocity);
                        animator.SetBool("LaunchedRight", true);
                    }
                    else if (gameObject.transform.position.x - player.position.x > 0)
                    {
                        GetComponent<Rigidbody2D>().AddForce(Vector2.right * bombVelocity);
                        animator.SetBool("LaunchedRight", true);
                    }
                    else
                    {
                        GetComponent<Rigidbody2D>().AddForce(Vector2.left * bombVelocity);
                        animator.SetBool("LaunchedLeft", true);
                    }
                }
            }
        }
    }

    private void Explode()
    {
        Quaternion rotation = Quaternion.Euler(-90, 0, 0);
        shakeManager.ShakeCamera(shakeDuration, shakeIntensity);
        Instantiate(bombZone, gameObject.transform.position, Quaternion.identity);
        Instantiate(circle, gameObject.transform.position, Quaternion.identity);
        Instantiate(mushroom, gameObject.transform.position, rotation);
        Destroy(gameObject);
    }

}
