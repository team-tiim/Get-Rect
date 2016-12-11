using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    public GameObject buttons;
    public GameObject backButton;
    public GameObject credits;
    public GameObject playerInsult;

    void Start()
    {
        buttons = GameObject.Find("Buttons");
        backButton = GameObject.Find("BackButton");
        credits = GameObject.Find("Credits");
        playerInsult = GameObject.Find("PlayerInsult");
        credits.SetActive(false);
        backButton.SetActive(false);
        //playerInsult.SetActive(false);
    }

    public void startNewGame()
    {
        Debug.Log("startnew");
        SceneManager.LoadScene("scene1");
    }

    public void rollCredits()
    {
        Debug.Log("rollcredits");
        buttons.SetActive(false);
        backButton.SetActive(true);
        credits.SetActive(true);
    }

    public void mockPlayer()
    {
        Application.Quit();
        //Debug.Log("mockplayer");
        //playerInsult.GetComponent<Text>().text = "I bet your mom is a quitter too";
       // playerInsult.SetActive(true);
    }

    public void back()
    {
        Debug.Log("back");
        buttons.SetActive(true);
        backButton.SetActive(false);
        credits.SetActive(false);
    }
}
