using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject interactablePrefab;
    public int spawnTick = 15; //seconds
    public int locationChangeTick; //count
    public bool isEnabled = true;
    private int curCounter = 0;
    private int counter = 0;

    void Start()
    {
        interactablePrefab.GetComponent<BaseInteractable>().spawner = this;
        InvokeRepeating("Spawn", 2, spawnTick);
    }

    public void Spawn()
    {
        if (!isEnabled || !gameObject.activeSelf)
        {
            return;
        }
        curCounter++;
        counter++;
        GameObject go = Instantiate(interactablePrefab);
        go.transform.position = this.transform.position;
    }

    public bool IsDoLocationChange()
    {
        return locationChangeTick <= 0 || curCounter < locationChangeTick;
    }

    public void ResetCurrentCounter()
    {
        this.curCounter = 0;
    }
}
