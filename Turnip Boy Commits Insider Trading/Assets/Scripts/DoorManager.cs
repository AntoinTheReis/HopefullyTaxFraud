using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorManager : MonoBehaviour
{

    public static DoorManager instance = null;
    public bool[] areOpen = new bool[9];

    // AWAKE function
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            intializeAllStatus();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Function to set intialize statuses of all doors
    private void intializeAllStatus()
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
}
