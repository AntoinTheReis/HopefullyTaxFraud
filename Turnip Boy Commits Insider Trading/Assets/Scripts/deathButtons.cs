using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class deathButtons : MonoBehaviour
{

    private playeVenemy playerHp; 

    // Start is called before the first frame update
    void Start()
    {
        playerHp = GameObject.FindGameObjectWithTag("Player").GetComponent<playeVenemy>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Retry()
    {
        playerHp.resetHp();
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
