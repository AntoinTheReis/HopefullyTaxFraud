using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{

    public static DoorManager instance;
    public bool[] areOpen = new bool[9];

    // AWAKE function
    void Awake()
    {
        instance = this;

        print(instance);

        DontDestroyOnLoad(gameObject);
        intializeAllStatus();
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
        areOpen[0] = false;
        areOpen[1] = true;
        areOpen[2] = true;
        areOpen[3] = false;
        areOpen[4] = true;
        areOpen[5] = false;
        areOpen[6] = true;
        areOpen[7] = false;
        areOpen[8] = false;
    }
}
