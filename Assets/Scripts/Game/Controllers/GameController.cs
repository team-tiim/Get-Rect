using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public GameObject explosion;
    public GameObject basicBulletPrefab;
    public GameObject handBulletPrefab;

    public Text timerText;
    public Text healthText;
    public Text armorText;
    public int levelTime = 60; // seconds
    public int timeoutTime = 10;
    private float startTime;
    private GameObject playerObject;
    private GameObject[] platforms;
    private bool isTimeout;

    private PlayerBehaviour player;

    // Use this for initialization
    void Start () {
        timerText = GameObject.Find("Timer").GetComponent<Text>();
        healthText = GameObject.Find("Health").GetComponent<Text>();
        armorText = GameObject.Find("Armor").GetComponent<Text>();
        playerObject = GameObject.FindGameObjectWithTag("Player");
        player = playerObject.GetComponent<PlayerBehaviour>();
        platforms = GameObject.FindGameObjectsWithTag("Platform");
        startTime = Time.time;
        updatePlayerClosestPlatform();
    }
	
	// Update is called once per frame
	void Update () {
        float timeLeft = levelTime - (Time.time - startTime);
        updatePlayerClosestPlatform();
        updateTimer(timeLeft);
        updateHealth();
        updateArmor();
        if (timeLeft < timeoutTime && !isTimeout)
        {
            setTimeoutAnimation();
        }
	}

    public void doExplosion(Vector3 location, string animation = "explosion")
    {
        GameObject expl = Instantiate(explosion);
        expl.transform.position = location;
        expl.GetComponent<Animator>().Play(animation);
        Destroy(expl, expl.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
    }

    private void updateTimer(float timeLeft)
    {
        float minutes = Mathf.Floor(timeLeft / 60);
        float seconds = (timeLeft % 60);
        timerText.text = string.Format("{0}:{1}", minutes.ToString("0"), seconds.ToString("00"));
    }

    private void updateHealth()
    {
		if (player.hp < 0) {
			SceneManager.LoadScene ("menu");
		}
		healthText.text = player.hp.ToString();
    }

    private void updateArmor()
    {
        if (player.armor != null)
        {
            armorText.text = player.armor.Value.ToString();
        }
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
        player.GetComponent<PlayerBehaviour>().closestPlatform = closest;
    }

    private void setTimeoutAnimation()
    {
        Animator anim = player.GetComponent<Animator>();
        anim.runtimeAnimatorController = Resources.Load("Animations/player_timeout") as RuntimeAnimatorController;
        isTimeout = true;
    }
}
