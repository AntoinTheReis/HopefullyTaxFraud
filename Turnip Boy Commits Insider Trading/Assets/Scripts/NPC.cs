using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{

    private bool playerClose;
    private bool UI_On;
    private bool interactHit;
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
            OBJ_Dialogue_UI.SetActive(true);
            this.GetComponent<HUDHandler>().DisableHUD();
            OBJ_Dialogue_Text.GetComponent<Text>().text = myDialogue;
            OBJ_Name_Text.GetComponent<Text>().text = myName;
            uiSprite.texture = characterSprite.texture;
            UI_On = true;
            OBJ_Player.GetComponent<playerMovement>().set_higherUI(UI_On);
            interactHit = true;
        }

        // When player is actively talking to self
        if (UI_On && Input.GetKeyDown(KeyCode.Z) && !interactHit)
        {
            OBJ_Dialogue_UI.SetActive(false);
            this.GetComponent<HUDHandler>().EnableHUD();
            UI_On = false;
            OBJ_Player.GetComponent<playerMovement>().set_higherUI(UI_On);
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

    // When sprites and animations are implemented for the UI elements, this method will serve to execute those animations
    private void Untitled(bool boolean)
    {
        // TODO: Execute UI animations
    }
}
