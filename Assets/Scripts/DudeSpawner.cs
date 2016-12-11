using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DudeSpawner : MonoBehaviour {

    public GameObject[] prefabs;
    public int spawnTick = 15; //seconds
    private int counter = 0;

    void Start()
    {
        InvokeRepeating("MakeDude", 2, spawnTick);
    }

    public void MakeDude()
    {
        int arrayIdx = Random.Range(0, prefabs.Length);
        GameObject newDude = Instantiate(prefabs[arrayIdx]);
        newDude.name = name;
        newDude.transform.position = this.transform.position;
    }
}
