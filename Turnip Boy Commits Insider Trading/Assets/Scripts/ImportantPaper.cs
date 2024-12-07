using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImportantPaper : MonoBehaviour
{

    // Necessary variable
    private bool playerClose;
    private bool paperGiven;
    private string prevTool;
    private GameObject player;
    private GameObject Icon_Z;
    [Header("Necessary Objects")]
    public GameObject Icon_Z_Prefab;
    public Paper Paper;
    public GameObject sparkleParticle;

    // START function
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Paper.enabled = false;
        Icon_Z = Instantiate(Icon_Z_Prefab, gameObject.transform);
        Icon_Z.GetComponent<SpriteRenderer>().enabled = false;
        Icon_Z.GetComponent<SpriteRenderer>().sortingOrder = 100;
        Icon_Z.GetComponent<SpriteRenderer>().sortingLayerName = "Radish Boy";
        float dist_from_head = (gameObject.GetComponent<SpriteRenderer>().size.y / 2) + 1.6f;
        Icon_Z.transform.localScale = new Vector3(0.06f, 0.06f, 0.06f);
        Icon_Z.transform.position = new Vector3(gameObject.transform.position.x + 0.12f,
                                                gameObject.transform.position.y + dist_from_head,
                                                gameObject.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && playerClose && !paperGiven)
        {
            this.GetComponent<AudioSource>().Play();
            Paper.enabled = true;
            Paper.activatePaper("IMPORTANT\r\nDepartment of Taxation");
            paperGiven = true;
            player.GetComponent<ContinuityHandler>().reportingSystem(this.tag);
            player.GetComponent<toolManager>().enabled = false;
            if (player.GetComponent<swordManager>().enabled) 
            {
                prevTool = "sword";
                player.GetComponent<swordManager>().enabled = false; 
            }
            else if (player.GetComponent<gunManager>().enabled)
            {
                prevTool = "gun";
                player.GetComponent<gunManager>().enabled = false; 
            }
            else if (player.GetComponent<canManager>().enabled)
            {
                prevTool = "can";
                player.GetComponent<canManager>().enabled = false; 
            }
            player.GetComponent<playerMovement>().setPaperON(true);
            Destroy(Icon_Z);
        }
    }

    // When player gets close to self
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            playerClose = true;
            Icon_Z.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    // When player walks away from self
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            playerClose = false;
            if (Icon_Z != null)
            {
                Icon_Z.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }

    // Method to destroy self and associated particle system after paper has been ripped
    public void FinishTheRip()
    {
        Destroy(sparkleParticle);
        player.GetComponent<toolManager>().enabled = true;
        switch (prevTool)
        {
            case "sword":
                player.GetComponent<swordManager>().enabled = true;
                break;
            case "gun":
                player.GetComponent<gunManager>().enabled = true;
                break;
            case "can":
                player.GetComponent<canManager>().enabled = true;
                break;
        }
        player.GetComponent<playerMovement>().setPaperON(false);
        Destroy(gameObject);
    }
}
