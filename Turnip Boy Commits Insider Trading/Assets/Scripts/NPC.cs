using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{

    private bool playerClose;
    private bool dialogueOn;
    private bool interactHit;
    private GameObject Player;
    [Header("True public")]
    public GameObject Canvas;
    public GameObject Radish_talker;
    public GameObject NPC_talker;
    public GameObject Background;
    public GameObject dialogueBox;
    public GameObject interactSymbol;
    public GameObject nameBox;
    public GameObject npcDialogue;
    public GameObject npcName;
    public string myDialogue;
    public string myName;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // When player interacts with self
        if (playerClose && Input.GetKeyDown(KeyCode.Z) && !dialogueOn)
        {
            dialogueUI_switcher(true);
            npcDialogue.GetComponent<Text>().text = myDialogue;
            npcName.GetComponent<Text>().text = myName;
            dialogueOn = true;
            Player.GetComponent<playerMovement>().set_plDialogueOn(dialogueOn);
            interactHit = true;
        }

        // When player is actively talking to self (bro, this mf won't stop saying shit like "???" Like dawg, say something!)
        if (dialogueOn && Input.GetKeyDown(KeyCode.Z) && !interactHit)
        {
            dialogueUI_switcher(false);
            dialogueOn = false;
            Player.GetComponent<playerMovement>().set_plDialogueOn(dialogueOn);
        }

        interactHit = false;
    }

    // When player gets close to self
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            playerClose = true;
        }
    }

    // When player is not close
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            playerClose = false;
        }
    }

    // Activate or deactivate NPC dialogue UI
    private void dialogueUI_switcher(bool boolean)
    {
        Canvas.SetActive(boolean);
        Radish_talker.SetActive(boolean);
        NPC_talker.SetActive(boolean);
        Background.SetActive(boolean);
        dialogueBox.SetActive(boolean);
        interactSymbol.SetActive(boolean);
        nameBox.SetActive(boolean);
    } 
}
