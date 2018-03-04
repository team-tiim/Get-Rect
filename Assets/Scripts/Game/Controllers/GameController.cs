using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public GameObject basicBulletPrefab;
    public GameObject handBulletPrefab;

    public int levelTime = 60; // seconds
    public int timeoutTime = 10;
    private float startTime;
    private GameObject playerObject;
    private GameObject[] platforms;
    private bool isTimeout;

    private PlayerBehaviour player;
    private BossBehaviourBase boss;

    // Use this for initialization
    void Start () {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        player = playerObject.GetComponent<PlayerBehaviour>();
        boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossBehaviourBase>();

        platforms = GameObject.FindGameObjectsWithTag("Platform");
        startTime = Time.time;
        UpdatePlayerClosestPlatform();
    }
	
	// Update is called once per frame
	void Update () {
        UpdatePlayerClosestPlatform();
        UpdateHealth();
        UpdateTimer();

    }

    private void UpdateTimer()
    {
        float timeLeft = levelTime - (Time.time - startTime);
        if(boss != null && timeLeft  > 0)
        {
            boss.angerLevel = boss.maxAnger - (int)timeLeft / 10;
        }
        if (timeLeft < timeoutTime && !isTimeout)
        {
            setTimeoutAnimation();
        }
    }

    private void UpdateHealth()
    {
		if (player.hp < 0) {
            StartCoroutine(LoadMenu(4.0f));
		}
    }

    private IEnumerator LoadMenu(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene ("menu");
    }

    private void UpdatePlayerClosestPlatform()
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
        //anim.runtimeAnimatorController = Resources.Load("Animations/player_timeout") as RuntimeAnimatorController;
        isTimeout = true;
    }
}
