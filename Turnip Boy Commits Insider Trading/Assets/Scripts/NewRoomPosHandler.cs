using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewRoomPosHandler : MonoBehaviour
{

    [Header("Necessary Objects")]
    public GameObject watermelonPrefab;
    [HideInInspector]
    public static string prevRoom;

    // AWAKE function
    void Awake()
    {
        // Entering Room 1 after dying / Beginning of game
        if (SceneManager.GetActiveScene().name == "Room 1" && prevRoom == null)
        {
            this.transform.position = new Vector3(-15.12612f, 2.976284f, 0f);
            this.GetComponent<playerMovement>().rightFacing();
            ToolDirectionRIGHT();
        }
        // Entering Room 1 backward
        else if (SceneManager.GetActiveScene().name == "Room 1" && prevRoom == "Room 2")
        {
            this.transform.position = new Vector3(9.24f, -9.99f, 0f);
            ToolDirectionDeterminer();
        }
        // Entering Room 2 forward
        else if (SceneManager.GetActiveScene().name == "Room 2" && prevRoom == "Room 1")
        {
            this.transform.position = new Vector3(-3.78f, -0.83f, 0f);
            ToolDirectionDeterminer();
        }
        // Entering Room 2 backward
        else if (SceneManager.GetActiveScene().name == "Room 2" && prevRoom == "Room 3")
        {
            this.transform.position = new Vector3(-17.67f, 1.28f, 0f);
            this.GetComponent<playerMovement>().rightFacing();
            ToolDirectionRIGHT();
        }
        // Entering Room 3 forward
        else if (SceneManager.GetActiveScene().name == "Room 3" && prevRoom == "Room 2")
        {
            this.transform.position = new Vector3(23.3f, 0.52f, 0f);
            this.GetComponent<playerMovement>().leftFacing();
            ToolDirectionLEFT();
        }
        // Entering Room 3 backward
        else if (SceneManager.GetActiveScene().name == "Room 3" && prevRoom == "Room 4")
        {
            this.transform.position = new Vector3(0.55f, 0.52f, 0f);
            this.GetComponent<playerMovement>().rightFacing();
            ToolDirectionRIGHT();
        }
        // Entering Room 4 forward
        else if (SceneManager.GetActiveScene().name == "Room 4" && prevRoom == "Room 3")
        {
            this.transform.position = new Vector3(4.01f, 3.1f, 0f);
            this.GetComponent<playerMovement>().leftFacing();
            ToolDirectionLEFT();
        }
        // Entering Room 4 backward
        else if (SceneManager.GetActiveScene().name == "Room 4" && prevRoom == "Room 5")
        {
            this.transform.position = new Vector3(4.43f, -7.31f, 0f);
            this.GetComponent<playerMovement>().leftFacing();
            ToolDirectionLEFT();
        }
        // Entering Room 5 forward
        else if (SceneManager.GetActiveScene().name == "Room 5" && prevRoom == "Room 4")
        {
            this.transform.position = new Vector3(2.76f, 3.1f, 0f);
            this.GetComponent<playerMovement>().rightFacing();
            ToolDirectionRIGHT();
        }
    }

    // Function to determine and set direction of tools
    private void ToolDirectionDeterminer()
    {
        if (this.GetComponent<playerMovement>().facingRight)
        {
            ToolDirectionRIGHT();
        }
        else
        {
            ToolDirectionLEFT();
        }
    }

    // Function to set the direction tools are facing to be LEFT
    private void ToolDirectionLEFT()
    {
        this.GetComponent<canManager>().setFacingDirection(canManager.direction.Left);
        this.GetComponent<swordManager>().setFacingDirection(swordManager.direction.Left);
        this.GetComponent<gunManager>().setFacingDirection(gunManager.direction.Left);
    }
    // Function to set the direction tools are facing to be RIGHT
    private void ToolDirectionRIGHT()
    {
        this.GetComponent<canManager>().setFacingDirection(canManager.direction.Right);
        this.GetComponent<swordManager>().setFacingDirection(swordManager.direction.Right);
        this.GetComponent<gunManager>().setFacingDirection(gunManager.direction.Right);
    }
}
