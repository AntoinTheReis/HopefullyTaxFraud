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
    [Header("Necessary Objects")]
    public Paper Paper;
    public GameObject sparkleParticle;

    // START function
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && playerClose && !paperGiven)
        {
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
        }
    }

    // When player gets close to self
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            playerClose = true;
        }
    }

    // When player walks away from self
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            playerClose = false;
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
