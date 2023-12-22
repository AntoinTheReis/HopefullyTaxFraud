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
    public GameObject itemSwap;
    public GameObject tripDash;
    public GameObject useItem;

    // Static function to enable HUD elements
    public void EnableHUD()
    {
        // HEALTH_UI
        Heart_1.GetComponent<RawImage>().enabled = true;
        Heart_2.GetComponent<RawImage>().enabled = true;
        Heart_3.GetComponent<RawImage>().enabled = true;
        itemSwap.GetComponent<RawImage>().enabled = true;
        tripDash.GetComponent<RawImage>().enabled = true;
        useItem.GetComponent<RawImage>().enabled = true;
    }

    // Static function to disable HUD elements
    public void DisableHUD()
    {
        // HEALTH_UI
        Heart_1.GetComponent<RawImage>().enabled = false;
        Heart_2.GetComponent<RawImage>().enabled = false;
        Heart_3.GetComponent<RawImage>().enabled = false;
        itemSwap.GetComponent<RawImage>().enabled = false;
        tripDash.GetComponent<RawImage>().enabled = false;
        useItem.GetComponent<RawImage>().enabled = false;
    }
}
