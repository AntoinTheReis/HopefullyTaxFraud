using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{

    [Header("Sprite references")]
    public Sprite starterSprite;
    public Sprite lockedDoor;
    public Sprite breakableDoor;
    public Sprite UnbreakableDoor;
    [Header("Necessary variables")]
    public string associatedSceneName;
    public int doorNumber;

    // Depending on status, set sprite (START function)
    void Start()
    {
        if (DoorManager.instance.areOpen[doorNumber] == false)
        {
            this.GetComponent<SpriteRenderer>().sprite = starterSprite;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().sprite = UnbreakableDoor;
        }

        if (this.tag == "specialDoor" && DoorManager.instance.areOpen[doorNumber] == false)
        {
            this.GetComponent<BoxCollider2D>().edgeRadius = 2f;
        }
    }

    // When player gets close to self
    private void OnTriggerEnter2D(Collider2D col)
    {

        Sprite activeSprite = this.GetComponent<SpriteRenderer>().sprite;

        if (col.tag == "Player")
        {
            if (DoorManager.instance.areOpen[doorNumber])
            {
                NewRoomPosHandler.prevRoom = SceneManager.GetActiveScene().name;
                if (associatedSceneName != "End")
                {
                    SceneManager.LoadScene(sceneName: associatedSceneName);
                }
                else
                {
                    Application.Quit();
                }
            }
            else
            {
                Checks(activeSprite);
            }
        }
    }

    // Collision detection for bombZone interaction
    private void OnTriggerStay2D(Collider2D col)
    {
        if (starterSprite == breakableDoor)
        {
            print(col.tag);
            print(DoorManager.instance.areOpen[doorNumber]);
        }

        if (col.tag == "bombZone" && !DoorManager.instance.areOpen[doorNumber] && 
            starterSprite == breakableDoor)
        {

            print("BOMBS AWAY!");

            doorUnlock();
            this.GetComponent<BoxCollider2D>().edgeRadius = 0.8f;
        }
    }

    // Check if player has key 1 or 2
    private void Checks(Sprite localActiveSprite)
    {
        if (localActiveSprite == lockedDoor) 
        {
            if ((SceneManager.GetActiveScene().name == "Room 2" && Key.keyGone1) ||
                (SceneManager.GetActiveScene().name == "Room 1" && Key.keyGone2))
            {
                doorUnlock();
            }
            else if (SceneManager.GetActiveScene().name == "Room 3" && GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                doorUnlock();
            }
        }
    }

    // Unlocking door
    private void doorUnlock()
    {
        this.GetComponent<SpriteRenderer>().sprite = UnbreakableDoor;
        DoorManager.instance.areOpen[doorNumber] = true;
    }
}
