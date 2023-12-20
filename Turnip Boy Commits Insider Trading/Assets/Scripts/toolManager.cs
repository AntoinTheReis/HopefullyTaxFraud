using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toolManager : MonoBehaviour
{

    public float counter = 0;
    public canManager canManager;
    public swordManager swordManager;
    public gunManager gunManager;
    public playerMovement playerCode;

    public Sprite canSprite;
    public Sprite gunSprite;
    public Sprite swordSprite;
    public SpriteRenderer backItemRendererRight;
    public SpriteRenderer backItemRendererLeft;

    public bool usingTool = false;

    private bool up;
    private float holdingUp = 0;
    private bool left;
    private float holdingLeft = 0;
    private bool down;
    private float holdingDown = 0;
    private bool right;
    private float holdingRight = 0;

    public bool facingRight;

    public enum direction
    {
        Up,
        Left,
        Down,
        Right,
        UpLeft,
        DownLeft,
        UpRight,
        DownRight
    }

    public direction facingDirection;

    // Start is called before the first frame update
    void Start()
    {
        facingDirection = direction.Right;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.A) && !usingTool && !playerCode.dead && !Input.GetKeyDown(KeyCode.X))
        {
            counter--;
            if (counter < 0)
            {
                counter = 2;
            }
            if (counter == 0)
            {
                backItemRendererRight.sprite = canSprite;
                backItemRendererLeft.sprite = canSprite;
                canManager.enabled = true;
                gunManager.enabled = false;
                swordManager.enabled = false;
            }
            else if (counter == 1)
            {
                backItemRendererRight.sprite = swordSprite;
                backItemRendererLeft.sprite = swordSprite;
                canManager.enabled = false;
                gunManager.enabled = false;
                swordManager.enabled = true;
            }
            else
            {
                backItemRendererRight.sprite = gunSprite;
                backItemRendererLeft.sprite = gunSprite;
                canManager.enabled = false;
                gunManager.enabled = true;
                swordManager.enabled = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.S) && !usingTool && !playerCode.dead && !Input.GetKeyDown(KeyCode.X))
        {
            counter++;
            if (counter > 2)
            {
                counter = 0;
            }
            if (counter == 0)
            {
                backItemRendererRight.sprite = canSprite;
                backItemRendererLeft.sprite = canSprite;
                canManager.enabled = true;
                gunManager.enabled = false;
                swordManager.enabled = false;
            }
            else if (counter == 1)
            {
                backItemRendererRight.sprite = swordSprite;
                backItemRendererLeft.sprite = swordSprite;
                canManager.enabled = false;
                gunManager.enabled = false;
                swordManager.enabled = true;
            }
            else
            {
                backItemRendererRight.sprite = gunSprite;
                backItemRendererLeft.sprite = gunSprite;
                canManager.enabled = false;
                gunManager.enabled = true;
                swordManager.enabled = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.X) && !playerCode.getDashing() && !usingTool/*&&
            (!canManager.getAbleToShoot() ||
            !swordManager.getAbleToShoot() ||
            !gunManager.getAbleToShoot())*/)
        {
            usingTool = true;
            backItemRendererRight.enabled = false;
            backItemRendererLeft.enabled = false;
            Invoke("chillForSec", 1f);
        }
        /*if (usingTool)
        {
            backItemRendererRight.enabled = false;
            backItemRendererLeft.enabled = false;
        }
        if (!usingTool && facingRight)
        {
            backItemRendererRight.enabled = true;
            backItemRendererLeft.enabled = false;
        }
        else if(!usingTool)
        {
            backItemRendererLeft.enabled = true;
            backItemRendererRight.enabled = false;
        }
        */
    }

    private void UpdateDirection()
    {
        if (Input.GetKey("left"))
        {
            left = true;
            holdingLeft += 0.01f;
        }
        else
        {
            left = false;
            holdingLeft = 0;
        }
        if (Input.GetKey("down"))
        {
            down = true;
            holdingDown += 0.01f;
        }
        else
        {
            down = false;
            holdingDown = 0;
        }
        if (Input.GetKey("up"))
        {
            up = true;
            holdingUp += 0.01f;
        }
        else
        {
            up = false;
            holdingUp = 0;
        }
        if (Input.GetKey("right"))
        {
            right = true;
            holdingRight += 0.01f;
        }
        else
        {
            right = false;
            holdingRight = 0;
        }

        if (up && holdingUp > holdingDown)
        {
            if (left && !right)
            {
                facingDirection = direction.UpLeft;
                return;
            }
            else if (!left && right)
            {
                facingDirection = direction.UpRight;
                return;
            }
            else
            {
                facingDirection = direction.Up;
                return;
            }
        }
        else if (down && holdingDown > holdingUp)
        {
            if (left && !right)
            {
                facingDirection = direction.DownLeft;
                return;
            }
            else if (!left && right)
            {
                facingDirection = direction.DownRight;
                return;
            }
            else
            {
                facingDirection = direction.Down;
                return;
            }
        }
        else if (right && holdingRight > holdingLeft)
        {
            if (up && !down)
            {
                facingDirection = direction.UpRight;
                return;
            }
            else if (!up && down)
            {
                facingDirection = direction.DownRight;
                return;
            }
            else
            {
                facingDirection = direction.Right;
                return;
            }
        }
        else if (left && holdingRight < holdingLeft)
        {
            if (up && !down)
            {
                facingDirection = direction.UpLeft;
                return;
            }
            else if (!up && down)
            {
                facingDirection = direction.DownLeft;
                return;
            }
            else
            {
                facingDirection = direction.Left;
                return;
            }
        }
    }

    private void chillForSec()
    {
        usingTool = false;
        if (facingRight)
        {
            backItemRendererRight.enabled = true;
        }
        else
        {
            backItemRendererLeft.enabled = true;
        }

        /* if (canManager.getAbleToShoot() &&
            swordManager.getAbleToShoot() &&
            gunManager.getAbleToShoot())
        {
            facingRight = GameObject.FindGameObjectWithTag("Player").GetComponent<playerMovement>().facingRight;
           
            if (facingRight)
            {
                backItemRendererRight.enabled = true;
            }
            else
            {
                backItemRendererLeft.enabled = true;
            }
        }
        else
        {
            Invoke("chillForSec", 0.5f);
        }
        */
    }

    // usingTool getter
    public bool getUsingTool()
    {
        return usingTool;
    }
}
