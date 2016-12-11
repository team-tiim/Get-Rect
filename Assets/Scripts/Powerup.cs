using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour {

    public enum PowerupType { WEAPON, HEALTH, TIME };

    public PowerupType type;
    public int value;
    public Weapon weapon;

    public GameObject spawner;

	// Use this for initialization
	void Start () {
        this.spawner.GetComponent<DudeSpawner>().enabled = false;
        createRandom();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag != "Player")
        {
            return;
        }
        switch (this.type)
        {
            case PowerupType.WEAPON:
                //TODO change weapon
                break;
            case PowerupType.HEALTH:
                Debug.Log(other.GetComponent<CharacterBehaviourBase>().hp);
                other.GetComponent<CharacterBehaviourBase>().hp += this.value;
                Debug.Log(other.GetComponent<CharacterBehaviourBase>().hp);
                break;
            case PowerupType.TIME:
                Debug.Log(GameObject.Find("GameControllers").GetComponent<GameController>().levelTime);
                GameObject.Find("GameControllers").GetComponent<GameController>().levelTime += this.value;
                Debug.Log(GameObject.Find("GameControllers").GetComponent<GameController>().levelTime);
                break;
        }

        GameObject.Destroy(this.gameObject);
        this.spawner.GetComponent<DudeSpawner>().enabled = true;
    }

    private void createRandom()
    {
        switch (this.type)
        {
            case PowerupType.WEAPON:
                //TODO create random weapon
                break;
            case PowerupType.HEALTH:
                this.value = Random.Range(5, 10);
                break;
            case PowerupType.TIME:
                this.value = Random.Range(10, 30);
                break;
        }
    }
}
