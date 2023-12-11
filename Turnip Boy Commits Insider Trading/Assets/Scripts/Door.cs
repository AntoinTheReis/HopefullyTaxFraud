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
    }

    // When player gets close to self
    private void OnTriggerEnter2D(Collider2D col)
    {

        Sprite activeSprite = this.GetComponent<SpriteRenderer>().sprite;

        if (col.tag == "Player")
        {
            if (DoorManager.instance.areOpen[doorNumber])
            {
                SceneManager.LoadScene(sceneName: associatedSceneName);
            }
            else
            {
                keyChecks(activeSprite);
            }
        }
        if (col.tag == "bombZone" && !DoorManager.instance.areOpen[doorNumber])
        {
            doorUnlock();
        }
    }

    // Check if player has key 1 or 2
    private void keyChecks(Sprite localActiveSprite)
    {
        if (localActiveSprite == lockedDoor) 
        {
            if ((SceneManager.GetActiveScene().name == "Level_3" && Key.keyGone1) ||
                (SceneManager.GetActiveScene().name == "Level_5" && Key.keyGone2))
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
