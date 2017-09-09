using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour {
    public GameObject[] spawnerLocations;
    public Spawner[] enemySpawners;
    public Spawner[] weaponSpawners;
    public Spawner[] itemSpawners;

    //TODO change spawner locations?
    //private List<GameObject> freeSpawnerLocations = new List<GameObject>();
    //private bool isLocationChangeEnabled = true;


    void Start () {

    }
	
	void Update () {

    }

    //TODO change spawner locations?
    /*
    public GameObject GetNewLocation(GameObject location)
    {
        if (!freeSpawnerLocations.Any())
        {
            return location;
        }
        freeSpawnerLocations.Add(location);
        return RandomUtil.GetAndRemoveRandom(freeSpawnerLocations);
    }
    */
}
