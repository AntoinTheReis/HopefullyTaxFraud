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
        if (this.GetComponent<Rigidbody2D>() != null) { this.GetComponent<Rigidbody2D>().isKinematic = true; }

        if (DoorManager.areOpen[doorNumber] == false)
        {
            this.GetComponent<SpriteRenderer>().sprite = starterSprite;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().sprite = UnbreakableDoor;
        }

        // Changing initial hitbox for breakable door in Room 4
        if (starterSprite == breakableDoor && !DoorManager.areOpen[doorNumber] && 
            SceneManager.GetActiveScene().name == "Room 4")
        {
            this.GetComponent<BoxCollider2D>().edgeRadius = 0.15f;
        }
    }

    // When player gets close to self
    private void OnTriggerEnter2D(Collider2D col)
    {

        Sprite activeSprite = this.GetComponent<SpriteRenderer>().sprite;

        if (col.tag == "Player" && col.gameObject.GetComponent<playeVenemy>().getHP() > 0)
        {
            if (DoorManager.areOpen[doorNumber])
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
        else if (col.tag == "bombZone" && !DoorManager.areOpen[doorNumber] &&
                 starterSprite == breakableDoor)
        {
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
                this.GetComponent<AudioSource>().Play();
            }
            else if (SceneManager.GetActiveScene().name == "Room 3" && GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                doorUnlock();
                this.GetComponent<AudioSource>().Play();
            }
        }
    }

    // Unlocking door
    private void doorUnlock()
    {
        this.GetComponent<SpriteRenderer>().sprite = UnbreakableDoor;
        DoorManager.areOpen[doorNumber] = true;
    }
}
