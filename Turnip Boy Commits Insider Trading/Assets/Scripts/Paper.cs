using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Paper : MonoBehaviour
{

    // TODO: Gotta make basically everything an opacity lerp at some point.

    private bool activated = false;
    private bool reading = false;
    private bool ripT_readF = true;
    private bool coroutineOn;
    [Header("Necessary Objects")]
    public playerMovement OBJ_Player;
    public GameObject OBJ_Ripping_UI;
    public GameObject OBJ_Readable;
    public GameObject OBJ_Readable_BG;
    public GameObject OBJ_Rip_Up;
    public GameObject OBJ_Read;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // If self has been activated, handle controls
        if (activated && !reading && !coroutineOn)
        {
            // Rip Up becomes OPAQUE; Read becomes TRANSLUCENT
            if (Input.GetKeyDown("up") && !ripT_readF)
            {
                StartCoroutine(lerpOpacity(OBJ_Rip_Up, 1.0f, 0.8f));
                StartCoroutine(lerpOpacity(OBJ_Read, 0.5f, 0.8f));
                ripT_readF = true;
            }
            // Rip Up becomes TRANSLUCENT; Read becomes OPAQUE
            else if (Input.GetKeyDown("down") &&  ripT_readF)
            {
                StartCoroutine(lerpOpacity(OBJ_Read, 1.0f, 0.8f));
                StartCoroutine(lerpOpacity(OBJ_Rip_Up, 0.5f, 0.8f));
                ripT_readF = false;
            }
            // Selecting Rip Up
            else if (Input.GetKeyDown(KeyCode.Z) && ripT_readF)
            {
                // After we get the paper ripping animation and other art assets,
                // we'll be able to actually make this look the way it does in the game.
                // But until then...
                OBJ_Player.set_higherUI(false);
                OBJ_Ripping_UI.SetActive(false);
                activated = false;
            }
            else if (Input.GetKeyDown(KeyCode.Z) && !ripT_readF)
            {
                OBJ_Readable.SetActive(true);
                OBJ_Readable_BG.SetActive(true);
                reading = true;
            }
        }
        
        // Exiting out of the reading UI
        else if (reading && Input.GetKeyDown(KeyCode.X))
        {
            OBJ_Readable.SetActive(false);
            OBJ_Readable_BG.SetActive(false);
            reading = false;
        }
    }

    // Function to activate self
    public void activatePaper(string desiredText)
    {
        OBJ_Player.set_higherUI(true);
        OBJ_Ripping_UI.SetActive(true);
        OBJ_Readable.SetActive(false);
        OBJ_Readable_BG.SetActive(false);
        activated = true;
        OBJ_Readable.GetComponent<Text>().text = desiredText;
    }

    // Coroutine for lerpping opacity (NOTE: Might make the totalTime the same for all opacity lerps if I determine that they're all the same
    // length in the game.)
    private IEnumerator lerpOpacity(GameObject UI_element, float opacity, float totalTime)
    {
        coroutineOn = true;
        float Timer = 0f;
        RawImage elementImage = UI_element.GetComponent<RawImage>();
        Color OGcolor = elementImage.color;
        Color finalColor = new Color(OGcolor.r, OGcolor.g, OGcolor.b, opacity);
        while (elementImage.color != finalColor)
        {
            elementImage.color = Color.Lerp(OGcolor, finalColor, Timer);
            Timer = (Timer + Time.deltaTime) / totalTime;
            yield return null;
        }
        coroutineOn = false;
    }
}
