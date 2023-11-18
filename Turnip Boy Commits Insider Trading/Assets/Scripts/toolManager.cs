using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toolManager : MonoBehaviour
{

    public float counter = 0;
    public canManager canManager;
    public swordManager swordManager;
    public gunManager gunManager;

    public Sprite canSprite;
    public Sprite gunSprite;
    public Sprite swordSprite;
    public SpriteRenderer backItemRenderer;

    private bool usingTool = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && !usingTool)
        {
            counter--;
            if(counter<0)
            {
                counter = 2;
            }
            if(counter == 0)
            {
                backItemRenderer.sprite = canSprite;
                canManager.enabled = true;
                gunManager.enabled = false;
                swordManager.enabled = false;
            }
            else if(counter == 1)
            {
                backItemRenderer.sprite = swordSprite;
                canManager.enabled = false;
                gunManager.enabled = false;
                swordManager.enabled = true;
            }
            else
            {
                backItemRenderer.sprite = gunSprite;
                canManager.enabled = false;
                gunManager.enabled = true;
                swordManager.enabled = false;
            }
        }
        if(Input.GetKeyDown(KeyCode.S) && !usingTool)
        {
            counter++;
            if(counter > 2)
            {
                counter = 0;
            }
            if (counter == 0)
            {
                backItemRenderer.sprite = canSprite;
                canManager.enabled = true;
                gunManager.enabled = false;
                swordManager.enabled = false;
            }
            else if (counter == 1)
            {
                backItemRenderer.sprite = swordSprite;
                canManager.enabled = false;
                gunManager.enabled = false;
                swordManager.enabled = true;
            }
            else
            {
                backItemRenderer.sprite = gunSprite;
                canManager.enabled = false;
                gunManager.enabled = true;
                swordManager.enabled = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            usingTool = true;
            backItemRenderer.enabled = false;
            Invoke("chillForSec", 1);
        }
    }

    private void chillForSec()
    {
        usingTool = false;
        backItemRenderer.enabled = true;
    }

}
