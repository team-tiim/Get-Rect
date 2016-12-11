using Assets.Scripts;
using Assets.Scripts.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour {

    public enum PowerupType { WEAPON, HEALTH, TIME };

    public PowerupType type;
    public int value;
    public DudeSpawner spawner;

	// Use this for initialization
	void Start () {
        this.spawner.isEnabled = false;
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
                addWeapon(other);
                break;
            case PowerupType.HEALTH:
                other.GetComponent<CharacterBehaviourBase>().hp += this.value;
                break;
            case PowerupType.TIME:
                GameObject.Find("GameControllers").GetComponent<GameController>().levelTime += this.value;
                break;
        }
        this.spawner.isEnabled = true;
        GameObject.Destroy(this.gameObject);
    }

    private void createRandom()
    {
        switch (this.type)
        {
            case PowerupType.WEAPON:
                this.value = Random.Range(0, 6);
                break;
            case PowerupType.HEALTH:
                this.value = Random.Range(5, 10);
                break;
            case PowerupType.TIME:
                this.value = Random.Range(10, 30);
                break;
        }
    }


    private void addWeapon(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        Weapon weapon = getWeapon(player);
        if (weapon != null)
        {
            player.selectWeapon(weapon);
        }
    }

    private Weapon getWeapon(PlayerController player)
    {
        switch (this.value)
        {
            case 0:
                return player.gameObject.AddComponent<Pistol>();
            case 1:
                return player.gameObject.AddComponent<Knife>();
            case 2:
                return player.gameObject.AddComponent<Fish>();
            case 3:
                return player.gameObject.AddComponent<Catapult>();
            case 4:
                return player.gameObject.AddComponent<Tank>();
            case 5:
                return player.gameObject.AddComponent<Uzi>();
            default:
                return null;
        }
    }
}
