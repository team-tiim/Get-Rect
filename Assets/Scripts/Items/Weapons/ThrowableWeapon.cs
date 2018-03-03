using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableWeapon : Weapon
{
    public GameObject projectilePrefab;
    public float maxDistance = 10f;
    public float maxChanrgeTime = 3f;
    public float damageRadius;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    protected override void DoAttack(GameObject parent, Vector3 direction)
    {
        throw new System.NotImplementedException();
    }
}
