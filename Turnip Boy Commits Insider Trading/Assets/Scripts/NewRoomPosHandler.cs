using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewRoomPosHandler : MonoBehaviour
{

    [HideInInspector]
    public static string prevRoom;

    // AWAKE function
    void Awake()
    {
        // Entering Room 1 backward
        if (SceneManager.GetActiveScene().name == "Room 1" && prevRoom == "Room 2")
        {
            this.transform.position = new Vector3(9.24f, -9.99f, 0f);
        }
        // Entering Room 2 forward
        else if (SceneManager.GetActiveScene().name == "Room 2" && prevRoom == "Room 1")
        {
            this.transform.position = new Vector3(-3.78f, -0.83f, 0f);
        }
        // Entering Room 2 backward
        else if (SceneManager.GetActiveScene().name == "Room 2" && prevRoom == "Room 3")
        {
            this.transform.position = new Vector3(-17.67f, 1.28f, 0f);
            this.GetComponent<playerMovement>().rightFacing();
        }
        // Entering Room 3 forward
        else if (SceneManager.GetActiveScene().name == "Room 3" && prevRoom == "Room 2")
        {
            this.transform.position = new Vector3(0.42f, 0.84f, 0f);
            this.GetComponent<playerMovement>().rightFacing();
        }
        // Entering Room 3 backward
        else if (SceneManager.GetActiveScene().name == "Room 3" && prevRoom == "Room 4")
        {
            this.transform.position = new Vector3(23.62f, 0.84f, 0f);
            this.GetComponent<playerMovement>().leftFacing();
        }
        // Entering Room 4 forward
        else if (SceneManager.GetActiveScene().name == "Room 4" && prevRoom == "Room 3")
        {
            this.transform.position = new Vector3(4.01f, 3.1f, 0f);
            this.GetComponent<playerMovement>().leftFacing();
        }
        // Entering Room 4 backward
        else if (SceneManager.GetActiveScene().name == "Room 4" && prevRoom == "Room 5")
        {
            this.transform.position = new Vector3(4.43f, -7.31f, 0f);
            this.GetComponent<playerMovement>().leftFacing();
        }
        // Entering Room 5 forward
        else if (SceneManager.GetActiveScene().name == "Room 5" && prevRoom == "Room 4")
        {
            this.transform.position = new Vector3(2.76f, 3.1f, 0f);
            this.GetComponent<playerMovement>().rightFacing();
        }
    }
}
