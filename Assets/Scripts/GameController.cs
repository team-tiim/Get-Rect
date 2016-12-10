using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public Text timerText;
    public float levelTime = 60; // seconds
    private float startTime;
    private GameObject player;
    
	// Use this for initialization
	void Start () {
        timerText = GameObject.FindGameObjectWithTag("Timer").GetComponent<Text>() ;
        player = GameObject.FindGameObjectWithTag("Player");
        startTime = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
        float timeLeft = levelTime - (Time.time - startTime);
        float minutes = Mathf.Floor(timeLeft / 60);
        float seconds = (timeLeft % 60);
        timerText.text = string.Format("{0}:{1}", minutes.ToString("0"), seconds.ToString("00"));

        if (timeLeft < 10)
        {
            Animator anim = this.gameObject.GetComponent<Animator>();
            Debug.Log("sweatting");
            //TODO change idle to sweating animation

        }
	}
}
