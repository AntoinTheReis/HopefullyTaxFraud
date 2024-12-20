using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{

    private bool playerClose;
    private bool UI_On;
    private bool interactHit;
    private string prevTool;
    private GameObject OBJ_Player;
    [Header("Necessary Objects")] // For dragging and dropping into the inspector,
                                  // remember to drag and drop the Dialogue_UI object that's IN THE SCENE,
                                  // as opposed to the one in the prefab folder.
    public GameObject OBJ_Dialogue_UI;
    public GameObject OBJ_Dialogue_Text;
    public GameObject OBJ_Name_Text;
    public RawImage uiSprite;
    [Header("NPC's Dialogue & Name")]
    public string myDialogue;
    public string myName;
    public Sprite characterSprite;
    public string NPC_Type;

    // Start is called before the first frame update
    void Start()
    {
        OBJ_Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // When player interacts with self
        if (playerClose && Input.GetKeyDown(KeyCode.Z) && !UI_On)
        {
            this.GetComponent<AudioSource>().Play();
            OBJ_Dialogue_UI.SetActive(true);
            this.GetComponent<HUDHandler>().DisableHUD();
            OBJ_Dialogue_Text.GetComponent<Text>().text = myDialogue;
            OBJ_Name_Text.GetComponent<Text>().text = myName;
            uiSprite.texture = characterSprite.texture;
            UI_On = true;
            OBJ_Player.GetComponent<playerMovement>().set_higherUI(UI_On);
            interactHit = true;
            OBJ_Player.GetComponent<toolManager>().enabled = false;
            if (OBJ_Player.GetComponent<swordManager>().enabled)
            {
                prevTool = "sword";
                OBJ_Player.GetComponent<swordManager>().enabled = false;
            }
            else if (OBJ_Player.GetComponent<gunManager>().enabled)
            {
                prevTool = "gun";
                OBJ_Player.GetComponent<gunManager>().enabled = false;
            }
            else if (OBJ_Player.GetComponent<canManager>().enabled)
            {
                prevTool = "can";
                OBJ_Player.GetComponent<canManager>().enabled = false;
            }
            if (!DoorManager.startTalking)
            {
                DoorManager.talkingHasCommenced();
            }
        }

        // When player is actively talking to self
        if (UI_On && Input.GetKeyDown(KeyCode.Z) && !interactHit)
        {
            this.GetComponent<AudioSource>().Play();
            OBJ_Dialogue_UI.SetActive(false);
            this.GetComponent<HUDHandler>().EnableHUD();
            UI_On = false;
            OBJ_Player.GetComponent<playerMovement>().set_higherUI(UI_On);
            OBJ_Player.GetComponent<toolManager>().enabled = true;
            switch (prevTool)
            {
                case "sword":
                    OBJ_Player.GetComponent<swordManager>().enabled = true;
                    break;
                case "gun":
                    OBJ_Player.GetComponent<gunManager>().enabled = true;
                    break;
                case "can":
                    OBJ_Player.GetComponent<canManager>().enabled = true;
                    break;
            }
        }

        interactHit = false;
    }

    // When player gets close to self
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            playerClose = true;
            if (!DoorManager.startTalking)
            {
                float dist_from_head = (gameObject.GetComponent<SpriteRenderer>().size.y / 2) + 1.1f;
                DoorManager.Z_Icon.transform.position = new Vector3(gameObject.transform.position.x, 
                                                                    gameObject.transform.position.y + dist_from_head, 
                                                                    gameObject.transform.position.z);
                DoorManager.Z_Icon.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }

    // When player is not close
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            playerClose = false;
            if (!DoorManager.startTalking)
            {
                DoorManager.Z_Icon.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }
}
