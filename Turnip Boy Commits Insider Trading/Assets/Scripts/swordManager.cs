using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordManager : MonoBehaviour
{

    public toolManager toolManager;
    public playerMovement playerCode;

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

    private direction facingDirection;

    private bool ableToShoot = true;
    public float shootingWaitingTime = 1f;

    private bool up;
    private float holdingUp = 0;
    private bool left;
    private float holdingLeft = 0;
    private bool down;
    private float holdingDown = 0;
    private bool right;
    private float holdingRight = 0;

    public GameObject sword;
    public GameObject gun;

    [Header("Directions")]
    public Transform dirUp;
    public Transform dirDown;
    public Transform dirLeft;
    public Transform dirRight;
    public Transform dirUpLeft;
    public Transform dirDownLeft;
    public Transform dirUpRight;
    public Transform dirDownRight;

    void Start()
    {
        gun.GetComponent<SpriteRenderer>().enabled = false;
    }
    private void SetActive()
    {
        facingDirection = (direction)(toolManager.facingDirection);
    }

    // Update is called once per frame
    void Update()
    {
        if (ableToShoot)
        {
            UpdateDirection();
        }
        if (Input.GetKeyDown(KeyCode.X) && ableToShoot && !playerCode.getDashing() && !playerCode.dead)
        {
            GameObject tempSword = Instantiate(sword, gun.gameObject.transform);
            tempSword.GetComponent<AudioSource>().Play();

            ableToShoot = false;
            StartCoroutine(ShootCoroutine());
        }

        if (facingDirection == direction.Left)
        {
            gun.transform.position = dirLeft.position;
            gun.transform.rotation = dirLeft.rotation;
        }
        else if (facingDirection == direction.Right)
        {
            gun.transform.position = dirRight.position;
            gun.transform.rotation = dirRight.rotation;
        }
        else if (facingDirection == direction.Up)
        {
            gun.transform.position = dirUp.position;
            gun.transform.rotation = dirUp.rotation;
        }
        else if (facingDirection == direction.Down)
        {
            gun.transform.position = dirDown.position;
            gun.transform.rotation = dirDown.rotation;
        }
        else if (facingDirection == direction.DownLeft)
        {
            gun.transform.position = dirDownLeft.position;
            gun.transform.rotation = dirDownLeft.rotation;
        }
        else if (facingDirection == direction.UpLeft)
        {
            gun.transform.position = dirUpLeft.position;
            gun.transform.rotation = dirUpLeft.rotation;
        }
        else if (facingDirection == direction.DownRight)
        {
            gun.transform.position = dirDownRight.position;
            gun.transform.rotation = dirDownRight.rotation;
        }
        else if (facingDirection == direction.UpRight)
        {
            gun.transform.position = dirUpRight.position;
            gun.transform.rotation = dirUpRight.rotation;
        }

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
    IEnumerator ShootCoroutine()
    {
        yield return new WaitForSecondsRealtime(shootingWaitingTime);
        ableToShoot = true;
    }

    // ableToShoot getter
    public bool getAbleToShoot()
    {
        return ableToShoot;
    }

    // facingDirection setter
    public void setFacingDirection(direction d)
    {
        facingDirection = d;
    }
}
