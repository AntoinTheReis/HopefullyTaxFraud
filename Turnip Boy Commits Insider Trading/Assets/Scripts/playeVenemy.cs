using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playeVenemy : MonoBehaviour
{

    public float shakeIntensity = 0.4f;
    public float shakeDuration = 0.15f;
    private playerMovement playerMovement;
    private CameraShakeManager shakeManager;

    [Header("HP & Health_UI Object")]
    public static float hp = 3;
    public GameObject health_UI;
    public GameObject Heart_1;
    public GameObject Heart_2;
    public GameObject Heart_3;

    [Header("Death")]
    private Animator animator;
    public GameObject deathUiPrefab;

    [Header("Audio file")]
    public AudioClip hurtSFX;


    // Start is called before the first frame update
    void Start()
    {
        shakeManager = GameObject.FindGameObjectWithTag("VCM").GetComponent<CameraShakeManager>();
        playerMovement = GetComponent<playerMovement>();
        animator = GetComponent<Animator>();
        HealthUIUpdate();
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
            // Taking damage IF the enemy is a routing enemy AND if they are NOT DYING.
            if (collision.gameObject.GetComponent<routEnemy>() != null)
            {
                takeDamage(collision, "r");
            }
            // Taking damage IF the enemy is a homing enemy AND if they are NOT DYING.
            else if (collision.gameObject.GetComponent<homingEnemy>() != null)
            {
                takeDamage(collision, "h");
            }
        }
        if(collision.gameObject.tag == "heart")
        {
            hp += 0.5f;
            HealthUIUpdate();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "bombZone")
        {
            shakeManager.ShakeCamera(shakeDuration, shakeIntensity);
            hp -= 0.5f;
            this.GetComponent<AudioSource>().clip = hurtSFX;
            this.GetComponent<AudioSource>().Play();
            HealthUIUpdate();
            if (hp == 0)
            {
                Dead();
            }
        }
    }

    private void Dead()
    {
        animator.SetTrigger("Dead");
        playerMovement.dead = true;
        Invoke("Dying", 1);
    }

    private void HealthUIUpdate()
    {
        // Sprite assignments
        Heart_1.GetComponent<RawImage>().texture = StateSetter(1);
        Heart_2.GetComponent<RawImage>().texture = StateSetter(2);
        Heart_3.GetComponent<RawImage>().texture = StateSetter(3);

    }
    // Function to determine state heart is in (full, half, or empty) and assign that sprite to the corresponding heart object
    private Texture StateSetter(int num)
    {
        // Determining heart state and returning it to the HealthUIUpdate function
        if (hp >= num)
        {
            return health_UI.GetComponent<Health_UI>().fullHeart;
        }
        else if (hp == num - 0.5f)
        {
            return health_UI.GetComponent<Health_UI>().halfHeart;
        }
        else if (hp < num - 0.5f)
        {
            return health_UI.GetComponent<Health_UI>().emptyHeart;
        }

        return health_UI.GetComponent<Health_UI>().fullHeart;
    }

    private void Dying()
    {
        Instantiate(deathUiPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }
    public void resetHp()
    {
        hp = 3;
    }

    // Function to TAKE DAMAGE
    private void takeDamage(Collision2D collision, string enemyType)
    {
        if (enemyType == "r")
        {
            if (!collision.gameObject.GetComponent<routEnemy>().dying)
            {
                shakeManager.ShakeCamera(shakeDuration, shakeIntensity);
                hp -= 0.5f;
                this.GetComponent<AudioSource>().clip = hurtSFX;
                this.GetComponent<AudioSource>().Play();
                Vector3 dir = collision.gameObject.transform.position - transform.position;
                dir = -dir.normalized;
                GetComponent<Rigidbody2D>().AddForce(dir * playerMovement.bombPush);
                HealthUIUpdate();
                if (hp == 0)
                {
                    Dead();
                }
            }
        }
        else if (enemyType == "h")
        {
            if (!collision.gameObject.GetComponent<homingEnemy>().dying)
            {
                shakeManager.ShakeCamera(shakeDuration, shakeIntensity);
                hp -= 0.5f;
                this.GetComponent<AudioSource>().clip = hurtSFX;
                this.GetComponent<AudioSource>().Play();
                Vector3 dir = collision.gameObject.transform.position - transform.position;
                dir = -dir.normalized;
                GetComponent<Rigidbody2D>().AddForce(dir * playerMovement.bombPush);
                HealthUIUpdate();
                if (hp == 0)
                {
                    Dead();
                }
            }
        }
    }

    // HP getter
    public float getHP()
    {
        return hp;
    }
}
