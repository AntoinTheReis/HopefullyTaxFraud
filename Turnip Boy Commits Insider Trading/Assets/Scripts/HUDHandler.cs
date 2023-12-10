using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDHandler : MonoBehaviour
{

    [Header("HUD Elements to enable/disable")]
    public GameObject Heart_1;
    public GameObject Heart_2;
    public GameObject Heart_3;

    // Static function to enable HUD elements
    public void EnableHUD()
    {
        // HEALTH_UI
        Heart_1.GetComponent<RawImage>().enabled = true;
        Heart_2.GetComponent<RawImage>().enabled = true;
        Heart_3.GetComponent<RawImage>().enabled = true;
        // Insert similar statements for other HUD elements when they have been implemented
    }

    // Static function to disable HUD elements
    public void DisableHUD()
    {
        // HEALTH_UI
        Heart_1.GetComponent<RawImage>().enabled = false;
        Heart_2.GetComponent<RawImage>().enabled = false;
        Heart_3.GetComponent<RawImage>().enabled = false;
        // Insert similar statements for other HUD elements when they have been implemented
    }
}
