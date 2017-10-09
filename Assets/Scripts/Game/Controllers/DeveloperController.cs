using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeveloperController : MonoBehaviour {

    public GameObject[] weaponPrefabs;

    private Dictionary<KeyCode, Action> keyActionMap = new Dictionary<KeyCode, Action>();

    private bool isPaused;
    private CharacterBehaviourBase player;
    private GameObject spawners;

    //Called before Start, use as constructor
    private void Awake()
    {
        keyActionMap.Add(KeyCode.Escape, () => ExitGame());
        keyActionMap.Add(KeyCode.P, () => TogglePause());
        keyActionMap.Add(KeyCode.F1, () => ToggleEnemySpawners());
        keyActionMap.Add(KeyCode.F2, () => KillAllEnemies());
        //weapons
        keyActionMap.Add(KeyCode.Alpha1, () => EquipWeapon(weaponPrefabs[0]));
        keyActionMap.Add(KeyCode.Alpha2, () => EquipWeapon(weaponPrefabs[1]));
        keyActionMap.Add(KeyCode.Alpha3, () => EquipWeapon(weaponPrefabs[2]));
        keyActionMap.Add(KeyCode.Alpha4, () => EquipWeapon(weaponPrefabs[3]));
        keyActionMap.Add(KeyCode.Alpha5, () => EquipWeapon(weaponPrefabs[4]));
        keyActionMap.Add(KeyCode.Alpha6, () => EquipWeapon(weaponPrefabs[5]));
        keyActionMap.Add(KeyCode.Alpha7, () => EquipWeapon(weaponPrefabs[6]));
        keyActionMap.Add(KeyCode.Alpha8, () => EquipWeapon(weaponPrefabs[7]));
        keyActionMap.Add(KeyCode.Alpha9, () => EquipWeapon(weaponPrefabs[8]));
        keyActionMap.Add(KeyCode.Alpha0, () => EquipWeapon(weaponPrefabs[9]));
    }

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterBehaviourBase>();
        spawners = GameObject.Find("spawners");
    }
	
	// Update is called once per frame
	void Update () {
        foreach (KeyValuePair<KeyCode, Action> entry in keyActionMap)
        {
            if (Input.GetKey(entry.Key))
            {
                entry.Value();
            }
        }
    }

    private void ExitGame()
    {
        Debug.Log("exitGame");
        SceneManager.LoadScene("menu");
    }

    private void TogglePause()
    {
        if (isPaused)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
    }

    private void ToggleEnemySpawners()
    {
        List<GameObject> gos = Utils.FindChildObjects(spawners, "Spawner");
        foreach (GameObject go in gos)
        {
            go.SetActive(!go.activeSelf);
        }
    }

    private void KillAllEnemies()
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject go in gos)
        {
            Destroy(go);
        }
    }

    private void EquipWeapon(GameObject weapon)
    {
        player.EquipWeapon(Instantiate(weapon));
    }
}
