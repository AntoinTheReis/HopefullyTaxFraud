using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorManager : MonoBehaviour
{

    public static DoorManager instance;
    public bool[] areOpen = new bool[9];

    // AWAKE function
    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        intializeAllStatus();
    }

    // Update function
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Level_4" && GameObject.FindWithTag("Enemy") == null)
        {
            GameObject Key = GameObject.FindWithTag("Key1");
            Key.GetComponent<SpriteRenderer>().enabled = true;
            Key.GetComponent<Transform>().position = new Vector3(0f, 0f, 0f);
        }
    }

    // Function to SET whether or not a door is open
    private void setStatus(bool status, int doorNumber) 
    {
        areOpen[doorNumber] = status;
    }

    // Function to GET whether or not a door is open
    private bool getStatus(int doorNumber)
    {
        return areOpen[doorNumber];
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
