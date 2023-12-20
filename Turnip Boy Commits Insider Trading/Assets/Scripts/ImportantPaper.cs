using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImportantPaper : MonoBehaviour
{

    // Necessary variable
    private bool playerClose;
    private bool paperGiven;
    public Paper Paper;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && playerClose && !paperGiven)
        {
            Paper.activatePaper("IMPORTANT\r\nDepartment of Taxation");
            paperGiven = true;
        }
    }

    // When player gets close to self
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            playerClose = true;
        }
    }

    // When player walks away from self
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            playerClose = false;
        }
    }
}
