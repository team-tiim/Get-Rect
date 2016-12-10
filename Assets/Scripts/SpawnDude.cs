using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDude : MonoBehaviour {

    public GameObject prefab;
    public Sprite[] sprites;

    public void makeDude()
    {
        int arrayIdx = Random.Range(0, sprites.Length);

        Sprite dudeSprite = sprites[arrayIdx];
        string name = dudeSprite.name;

        GameObject newDude = Instantiate(prefab);

        newDude.name = name;
        newDude.GetComponent<SpriteRenderer>().sprite = dudeSprite;
    }
}
