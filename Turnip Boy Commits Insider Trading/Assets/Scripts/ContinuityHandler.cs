using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinuityHandler : MonoBehaviour
{
    private static bool R2H1 = false;
    private static bool R2H2 = false;
    private static bool R2H3 = false;
    private static bool R2H4 = false;
    private static bool R4H1 = false;
    private static bool paper = false;

    // AWAKE function
    void Awake()
    {
        if (SceneManager.GetActiveScene().name == "Room 2")
        {
            if (R2H1)
            {
                watermelonHole melon = GameObject.FindGameObjectWithTag("R2H1").GetComponent<watermelonHole>();
                melon.internalFill();
            }
            if (R2H2)
            {
                watermelonHole melon = GameObject.FindGameObjectWithTag("R2H2").GetComponent<watermelonHole>();
                melon.internalFill();
            }
            if (R2H3)
            {
                watermelonHole melon = GameObject.FindGameObjectWithTag("R2H3").GetComponent<watermelonHole>();
                melon.internalFill();
            }
            if (R2H4)
            {
                watermelonHole melon = GameObject.FindGameObjectWithTag("R2H4").GetComponent<watermelonHole>();
                melon.internalFill();
            }
        }
        else if (SceneManager.GetActiveScene().name == "Room 4")
        {
            if (R4H1)
            {
                watermelonHole melon = GameObject.FindGameObjectWithTag("R4H1").GetComponent<watermelonHole>();
                melon.internalFill();
            }
            if (paper)
            {
                GameObject paperOBJ = GameObject.FindGameObjectWithTag("Paper");
                GameObject paperParticles = GameObject.FindGameObjectWithTag("PaperParticles");
                Destroy(paperParticles);
                Destroy(paperOBJ);
            }
        }
    }

    // Function for objects to report that they've been changed (either filled by a watermelon or ripped in half)
    public void reportingSystem(string ID)
    {
        if (ID == "R2H1") { R2H1 = true; }
        else if (ID == "R2H2") { R2H2 = true; }
        else if (ID == "R2H3") { R2H3 = true; }
        else if (ID == "R2H4") { R2H4 = true; }
        else if (ID == "R4H1") { R4H1 = true; }
        else if (ID == "Paper") { paper = true; }
    }

    // Function to reset all continuity booleans
    public static void resetBools()
    {
        R2H1 = false;
        R2H2 = false;
        R2H3 = false;
        R2H4 = false;
        R4H1 = false;
        paper = false;
    }
}
