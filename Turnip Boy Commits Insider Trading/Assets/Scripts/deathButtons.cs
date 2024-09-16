using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class deathButtons : MonoBehaviour
{

    private playeVenemy playerHp;

    private GameObject selector;
    [Header("Interactables")]
    public GameObject retry;
    public GameObject quit;

    // Start is called before the first frame update
    void Start()
    {
        playerHp = GameObject.FindGameObjectWithTag("Player").GetComponent<playeVenemy>();
        this.GetComponent<HUDHandler>().Heart_1 = GameObject.FindGameObjectWithTag("Heart1");
        this.GetComponent<HUDHandler>().Heart_2 = GameObject.FindGameObjectWithTag("Heart2");
        this.GetComponent<HUDHandler>().Heart_3 = GameObject.FindGameObjectWithTag("Heart3");
        this.GetComponent<HUDHandler>().itemSwap = GameObject.FindGameObjectWithTag("ItemSwap");
        this.GetComponent<HUDHandler>().tripDash = GameObject.FindGameObjectWithTag("TripDash");
        this.GetComponent<HUDHandler>().useItem = GameObject.FindGameObjectWithTag("UseItem");
        this.GetComponent<HUDHandler>().DisableHUD();
        selector = retry;
    }

    // Update is called once per frame
    void Update()
    {
        // Change what the pieces of text look like depending on which piece of text is currently selected
        appearanceUpdate();

        // Switching text selections and...
        if (Input.GetKeyDown("up") && selector != retry)
        {
            selector = retry;
        }
        else if (Input.GetKeyDown("down") && selector != quit)
        {
            selector = quit;
        }
            // ... Actually picking one of them
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            if (selector == retry)
            {
                Retry();
            }
            else
            {
                Quit();
            }
        }
    }

    public void Retry()
    {
        playerHp.resetHp();
        Key.keyGone1 = false;
        Key.keyGone2 = false;
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }

    // Function to update the color and size of the UI options
    private void appearanceUpdate()
    {
        // RETRY is selected
        if (selector == retry)
        {
            retry.GetComponent<Text>().color = new Color(1f, 0.67f, 0f, 1f);
            retry.GetComponent<Text>().fontSize = 86;
            quit.GetComponent<Text>().color = new Color(0.86f, 0.58f, 0f, 1f);
            quit.GetComponent<Text>().fontSize = 70;
        }
        // QUIT is selected
        else
        {
            quit.GetComponent<Text>().color = new Color(1f, 0.67f, 0f, 1f);
            quit.GetComponent<Text>().fontSize = 86;
            retry.GetComponent<Text>().color = new Color(0.86f, 0.58f, 0f, 1f);
            retry.GetComponent<Text>().fontSize = 70;
        }
    }
}
