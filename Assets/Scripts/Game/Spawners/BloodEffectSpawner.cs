using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodEffectSpawner : MonoBehaviour {
    public GameObject effectPrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SpawnEffect(Vector3 location)
    {
        Projectile p = Instantiate(effectPrefab, location, effectPrefab.transform.rotation).GetComponent<Projectile>();
    }
}
