using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorManager : MonoBehaviour
{

    public GameObject Z_Icon_Prefab;
    public static GameObject Z_Icon;
    public static bool startTalking = false;
    public static DoorManager instance = null;
    public static bool[] areOpen = new bool[9];

    // AWAKE function
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            initializeAllStatus();
            Z_Icon = Instantiate(Z_Icon_Prefab, gameObject.transform);
            Z_Icon.GetComponent<SpriteRenderer>().enabled = false;
            Z_Icon.GetComponent<SpriteRenderer>().sortingOrder = 100;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Function to set intialize statuses of all doors
    public static void initializeAllStatus()
    {
        areOpen[0] = false; // Room 1
        areOpen[1] = true; // Room 1
        areOpen[2] = true; // Room 2
        areOpen[3] = false; // Room 2
        areOpen[4] = true; // Room 3
        areOpen[5] = false; // Room 3
        areOpen[6] = true; // Room 4
        areOpen[7] = false; // Room 4
        areOpen[8] = true; // Room 5
    }

    // Function to destroy Z_Icon object and flip related bool because player has successfully learned how to talk to NPCs
    public static void talkingHasCommenced()
    {
        Destroy(Z_Icon);
        startTalking = true;
    }
}
