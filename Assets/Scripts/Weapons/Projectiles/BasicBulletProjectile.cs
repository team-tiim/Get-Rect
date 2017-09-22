using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBulletProjectile: Projectile {

    // Use this for initialization
    protected new void Start()
    {
        //Debug.Log("basic bullet start");
        spriteSheetName = "weapons 1";
        spriteIndex = 7;
        speed = 20;
        animationName = "projectile_pistol";
        base.Start();
    }

}
