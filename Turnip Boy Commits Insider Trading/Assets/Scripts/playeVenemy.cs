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
    public float hp = 3;
    public GameObject health_UI;
    public GameObject Heart_1;
    public GameObject Heart_2;
    public GameObject Heart_3;

    // Start is called before the first frame update
    void Start()
    {
        shakeManager = GameObject.FindGameObjectWithTag("VCM").GetComponent<CameraShakeManager>();
        playerMovement = GetComponent<playerMovement>();
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
            shakeManager.ShakeCamera(shakeDuration, shakeIntensity);
            hp -= 0.5f;
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

    private void Dead()
    {
        Debug.Log("dead");
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
}
