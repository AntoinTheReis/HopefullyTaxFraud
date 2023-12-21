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
    public GameObject OBJ_paperImage;
    public GameObject OBJ_Readable;
    public GameObject OBJ_Readable_BG;
    public GameObject OBJ_Background;
    public GameObject OBJ_Rip_Up;
    public GameObject OBJ_Rip_Up_Text;
    public GameObject OBJ_Read;
    public GameObject OBJ_Read_Text;
    public GameObject OBJ_leftHalf;
    public GameObject OBJ_rightHalf;

    // Update is called once per frame
    void Update()
    {
        // If self has been activated, handle controls
        if (activated && !reading && !coroutineOn)
        {
            // Rip Up becomes OPAQUE; Read becomes TRANSLUCENT
            if (Input.GetKeyDown("up") && !ripT_readF)
            {
                StartCoroutine(lerpOpacity(OBJ_Rip_Up, 1.0f, 0.8f, "Box"));
                StartCoroutine(lerpOpacity(OBJ_Rip_Up_Text, 1.0f, 0.8f, "Text"));
                StartCoroutine(lerpOpacity(OBJ_Read, 0.5f, 0.8f, "Box"));
                StartCoroutine(lerpOpacity(OBJ_Read_Text, 0.5f, 0.8f, "Text"));
                ripT_readF = true;
            }
            // Rip Up becomes TRANSLUCENT; Read becomes OPAQUE
            else if (Input.GetKeyDown("down") &&  ripT_readF)
            {
                StartCoroutine(lerpTransform(OBJ_leftHalf, -5.0f, -2.5f, 1.5f));
                StartCoroutine(lerpOpacity(OBJ_Read, 1.0f, 0.8f, "Box"));
                StartCoroutine(lerpOpacity(OBJ_Read_Text, 1.0f, 0.8f, "Text"));
                StartCoroutine(lerpOpacity(OBJ_Rip_Up, 0.5f, 0.8f, "Box"));
                StartCoroutine(lerpOpacity(OBJ_Rip_Up_Text, 0.5f, 0.8f, "Text"));
                ripT_readF = false;
            }
            // Selecting Rip Up
            else if (Input.GetKeyDown(KeyCode.Z) && ripT_readF)
            {
                OBJ_paperImage.GetComponent<RawImage>().enabled = false;
                //StartCoroutine(lerpTransform(OBJ_leftHalf, -5.0f, -2.5f, 1.5f));
                //StartCoroutine(lerpTransform(OBJ_rightHalf, 5.0f, -2.5f, 1.5f));
                StartCoroutine(lerpOpacity(OBJ_Rip_Up_Text, 0f, 1.5f, "Text"));
                StartCoroutine(lerpOpacity(OBJ_Read_Text, 0f, 1.5f, "Text"));
                StartCoroutine(lerpOpacity(OBJ_Rip_Up, 0f, 1.5f, "Box"));
                StartCoroutine(lerpOpacity(OBJ_Read, 0f, 1.5f, "Box"));
                StartCoroutine(lerpOpacity(OBJ_Background, 0f, 1.5f, "Box"));
                OBJ_Player.set_higherUI(false);
                this.GetComponent<HUDHandler>().EnableHUD();
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
        this.GetComponent<HUDHandler>().DisableHUD();
        OBJ_Ripping_UI.SetActive(true);
        OBJ_Readable.SetActive(false);
        OBJ_Readable_BG.SetActive(false);
        activated = true;
        OBJ_Readable.GetComponent<Text>().text = desiredText;
    }

    // Coroutine for lerpping opacity (NOTE: Might make the totalTime the same for all opacity lerps if I determine that they're all the same
    // length in the game.)
    private IEnumerator lerpOpacity(GameObject UI_element, float opacity, float totalTime, string type)
    {
        RawImage elementImage;
        Text elementText;
        coroutineOn = true;
        float Timer = 0f;
        if (type == "Text")
        {
            elementText = UI_element.GetComponent<Text>();
            Color OGcolor = elementText.color;
            Color finalColor = new Color(OGcolor.r, OGcolor.g, OGcolor.b, opacity);
            while (elementText.color != finalColor)
            {
                elementText.color = Color.Lerp(OGcolor, finalColor, Timer);
                Timer = (Timer + Time.deltaTime) / totalTime;
                yield return null;
            }
            coroutineOn = false;
        }
        else if (type == "Box")
        {
            elementImage = UI_element.GetComponent<RawImage>();
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

    // Coroutine for ripping animation and closing UI
    private IEnumerator lerpTransform(GameObject halfPage, float x_distance, float y_distance, float totalTime)
    {
        coroutineOn = true;
        float Timer = 0f;
        Vector3 elementImage = halfPage.GetComponent<Transform>().position;
        Vector3 OGposition = elementImage;
        Vector3 finalPosition = new Vector3(OGposition.x + x_distance, OGposition.y + y_distance / 2, OGposition.z);
        while (elementImage != finalPosition)
        {
            elementImage = Vector3.Lerp(OGposition, finalPosition, Timer);
            Timer = (Timer + Time.deltaTime) / totalTime;
            yield return null;
        }
        coroutineOn = false;
    }
}
