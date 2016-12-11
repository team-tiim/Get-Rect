using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DudeSpawner : MonoBehaviour {

    public GameObject prefab;
    public Sprite[] sprites;
    public RuntimeAnimatorController[] controllers;
    public float spawnTick = 10; //seconds

    void Start()
    {
        InvokeRepeating("MakeDude", 2, spawnTick);
    }

    public void MakeDude()
    {
        int arrayIdx = Random.Range(0, sprites.Length);

        Sprite sprite = sprites[arrayIdx];
        RuntimeAnimatorController animationController = controllers[arrayIdx];
        string name = sprite.name;

        GameObject newDude = Instantiate(prefab);
        newDude.name = name;
        newDude.GetComponent<SpriteRenderer>().sprite = sprite;
        newDude.GetComponent<Animator>().runtimeAnimatorController = animationController;
        newDude.transform.position = this.transform.position;

    }
}
