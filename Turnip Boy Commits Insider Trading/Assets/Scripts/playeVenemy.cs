using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playeVenemy : MonoBehaviour
{

    private playerMovement playerMovement;
    private GameObject Heart_1;
    private GameObject Heart_2;
    private GameObject Heart_3;
    [Header("HP & Health_UI Object")]
    public float hp = 3;
    public GameObject Health_UI;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<playerMovement>();
        Heart_1 = this.gameObject.transform.GetChild(0).gameObject;
        Heart_2 = this.gameObject.transform.GetChild(1).gameObject;
        Heart_3 = this.gameObject.transform.GetChild(2).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (hp > 3)
        {
            hp = 3;
        }

        // Update the health UI
        HealthUIUpdate();
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

    // TODO: Dying and respawning
    private void Dead()
    {
        Debug.Log("dead");
    }

    // Series of methods for the health UI to reflect current health value
        // Main health UI changing function
    private void HealthUIUpdate()
    {
        // Reference to sprite objects
        Health_UI sprites = Health_UI.GetComponent<Health_UI>();

        // Sprite assignments
        Heart_1.GetComponent<SpriteRenderer>().sprite = StateSetter(sprites, 1);
        Heart_2.GetComponent<SpriteRenderer>().sprite = StateSetter(sprites, 2);
        Heart_3.GetComponent<SpriteRenderer>().sprite = StateSetter(sprites, 3);

    }
        // Function to determine state heart is in (full, half, or empty) and assign that sprite to the corresponding heart object
    private Sprite StateSetter(Health_UI spr, int num)
    {
        Sprite returnSpr = Sprite.Create(Texture2D.redTexture, new Rect(0.0f, 0.0f, 0.0f, 0.0f), new Vector2(0.0f, 0.0f));

        // Determining heart state and returning it to the HealthUIUpdate function
        if (hp >= num)
        {
            returnSpr = spr.fullHeart;
        }
        else if (hp == num - 0.5f)
        {
            returnSpr = spr.halfHeart;
        }
        else if (hp < num - 0.5f)
        {
            returnSpr = spr.emptyHeart;
        }

        return returnSpr;
    }
}
