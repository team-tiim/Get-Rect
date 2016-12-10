using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public float levelTime = 15; // seconds
    private float startTime;
    private GameObject player;
    
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
        startTime = Time.time;
    }
	
	// Update is called once per frame
	void Update () {        
        if(Time.time - startTime > levelTime)
        {
            string[] asd= new string[3];
            Animator anim = this.gameObject.GetComponent<Animator>();
            Debug.Log("sweatting");
            //TODO change idle to sweating animation

        }
	}
}
