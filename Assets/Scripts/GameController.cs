using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public GameObject explosion;

    public Text timerText;
    public float levelTime = 60; // seconds
    private float startTime;
    private GameObject player;
    private GameObject[] platforms;

    // Use this for initialization
    void Start () {
        timerText = GameObject.FindGameObjectWithTag("Timer").GetComponent<Text>() ;
        player = GameObject.FindGameObjectWithTag("Player");
        platforms = GameObject.FindGameObjectsWithTag("Platform");
        startTime = Time.time;
        updatePlayerClosestPlatform();
    }
	
	// Update is called once per frame
	void Update () {
        updatePlayerClosestPlatform();

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

    public void doExplosion(Vector3 location)
    {
        GameObject expl = Instantiate(explosion);
        expl.transform.position = location;
        Destroy(expl, expl.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
    }

    private void updatePlayerClosestPlatform()
    {
        GameObject closest = platforms[0];
        var bestDist = Vector3.Distance(player.transform.position, platforms[0].transform.position);
        foreach (GameObject platform in platforms)
        {
            var curDist = Vector3.Distance(player.transform.position, platform.transform.position);
            if (curDist <= bestDist) {
                bestDist = curDist;
                closest = platform;
            }
        }
        player.GetComponent<PlayerController>().closestPlatform = closest;
    }
}
