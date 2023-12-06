using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_paper_script : MonoBehaviour
{

    // Necessary variable
    private bool playerClose;
    private bool paperGiven;
    public Paper Paper;

    // Update is called once per frame
    void Update()
    {
        if (playerClose && !paperGiven)
        {
            Paper.activatePaper("holy smokes");
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
}
