using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Paper : MonoBehaviour
{

    private bool activated = false;
    private bool reading = false;
    private bool ripT_readF = true;
    private bool coroutineOn;
    [Header("Actual Paper Object")]
    public GameObject realPaper;
    [Header("Necessary Objects")]
    public AudioClip paperRippingSFX;
    public AudioClip UI_Sound;
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
                this.GetComponent<AudioSource>().PlayOneShot(UI_Sound, 0.25F);
                Color OG_RU_Color = OBJ_Rip_Up.GetComponent<RawImage>().color;
                OBJ_Rip_Up.GetComponent<RawImage>().color = new Color(OG_RU_Color.r, OG_RU_Color.g, OG_RU_Color.b, 1.0f);
                Color OG_RUT_Color = OBJ_Rip_Up_Text.GetComponent<Text>().color;
                OBJ_Rip_Up_Text.GetComponent<Text>().color = new Color(OG_RUT_Color.r, OG_RUT_Color.g, OG_RUT_Color.b, 1.0f);
                Color OG_R_Color = OBJ_Read.GetComponent<RawImage>().color;
                OBJ_Read.GetComponent<RawImage>().color = new Color(OG_R_Color.r, OG_R_Color.g, OG_R_Color.b, 0.5f);
                Color OG_RT_Color = OBJ_Read_Text.GetComponent<Text>().color;
                OBJ_Read_Text.GetComponent<Text>().color = new Color(OG_RT_Color.r, OG_RT_Color.g, OG_RT_Color.b, 0.5f);
                ripT_readF = true;
            }
            // Rip Up becomes TRANSLUCENT; Read becomes OPAQUE
            else if (Input.GetKeyDown("down") &&  ripT_readF)
            {
                this.GetComponent<AudioSource>().PlayOneShot(UI_Sound, 0.25F);
                Color OG_RU_Color = OBJ_Rip_Up.GetComponent<RawImage>().color;
                OBJ_Rip_Up.GetComponent<RawImage>().color = new Color(OG_RU_Color.r, OG_RU_Color.g, OG_RU_Color.b, 0.5f);
                Color OG_RUT_Color = OBJ_Rip_Up_Text.GetComponent<Text>().color;
                OBJ_Rip_Up_Text.GetComponent<Text>().color = new Color(OG_RUT_Color.r, OG_RUT_Color.g, OG_RUT_Color.b, 0.5f);
                Color OG_R_Color = OBJ_Read.GetComponent<RawImage>().color;
                OBJ_Read.GetComponent<RawImage>().color = new Color(OG_R_Color.r, OG_R_Color.g, OG_R_Color.b, 1.0f);
                Color OG_RT_Color = OBJ_Read_Text.GetComponent<Text>().color;
                OBJ_Read_Text.GetComponent<Text>().color = new Color(OG_RT_Color.r, OG_RT_Color.g, OG_RT_Color.b, 1.0f);
                ripT_readF = false;
            }
            // Selecting Rip Up
            else if (Input.GetKeyDown(KeyCode.Z) && ripT_readF)
            {
                OBJ_paperImage.GetComponent<RawImage>().enabled = false;
                this.GetComponent<AudioSource>().PlayOneShot(paperRippingSFX, 0.8F);
                StartCoroutine(lerpTransform(OBJ_leftHalf, -150.0f, -100.0f, 1.5f));
                StartCoroutine(lerpTransform(OBJ_rightHalf, 150.0f, -100.0f, 1.5f));
                StartCoroutine(lerpOpacity(OBJ_leftHalf, 0f, 1.5f, "Box"));
                StartCoroutine(lerpOpacity(OBJ_rightHalf, 0f, 1.5f, "Box"));
                StartCoroutine(lerpOpacity(OBJ_Rip_Up_Text, 0f, 1.5f, "Text"));
                StartCoroutine(lerpOpacity(OBJ_Read_Text, 0f, 1.5f, "Text"));
                StartCoroutine(lerpOpacity(OBJ_Rip_Up, 0f, 1.5f, "Box"));
                StartCoroutine(lerpOpacity(OBJ_Read, 0f, 1.5f, "Box"));
                StartCoroutine(lerpOpacity(OBJ_Background, 0f, 1.5f, "Box"));
            }
            else if (Input.GetKeyDown(KeyCode.Z) && !ripT_readF)
            {
                this.GetComponent<AudioSource>().PlayOneShot(UI_Sound, 0.25F);
                OBJ_Readable.SetActive(true);
                OBJ_Readable_BG.SetActive(true);
                reading = true;
            }
        }
        
        // Exiting out of the reading UI
        else if (reading && Input.GetKeyDown(KeyCode.X))
        {
            this.GetComponent<AudioSource>().PlayOneShot(UI_Sound, 0.25F);
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
        OBJ_Readable.GetComponent<Text>().text = desiredText;
        StartCoroutine(inputDelay());
    }

    // Coroutine for delaying input when the paper gets activated
    private IEnumerator inputDelay()
    {
        yield return new WaitForSeconds(1);
        activated = true;
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
                Timer = (Timer + Time.deltaTime); // / totalTime;
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
                Timer = (Timer + Time.deltaTime); // / totalTime;
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
        Vector3 OGposition = halfPage.GetComponent<Transform>().position;
        Vector3 finalPosition = new Vector3(OGposition.x + x_distance, OGposition.y + y_distance / 2, OGposition.z);
        while (halfPage.GetComponent<Transform>().position != finalPosition)
        {
            halfPage.GetComponent<Transform>().position = Vector3.Lerp(OGposition, finalPosition, Timer);
            Timer = (Timer + Time.deltaTime); // / totalTime;
            yield return null;
        }
        coroutineOn = false;
        OBJ_Player.set_higherUI(false);
        this.GetComponent<HUDHandler>().EnableHUD();
        OBJ_Ripping_UI.SetActive(false);
        activated = false;
        realPaper.GetComponent<ImportantPaper>().FinishTheRip();
    }
}
