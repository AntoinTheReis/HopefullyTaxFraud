using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Paper : MonoBehaviour
{

    private bool activated = false;
    private bool reading = false;
    private bool ripT_readF = true;
    private string text_for_readable;
    [Header("Necessary Objects")]
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
        if (activated && !reading)
        {
            if (Input.GetKeyDown("up"))
            {

            }
        }
    }

    // Function to activate self
    public void activatePaper(string desiredText)
    {
        OBJ_Ripping_UI.SetActive(true);
        OBJ_Readable.SetActive(false);
        OBJ_Readable_BG.SetActive(false);
        OBJ_Read.GetComponent<RawImage>().color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        activated = true;
        text_for_readable = desiredText;
    }
}
