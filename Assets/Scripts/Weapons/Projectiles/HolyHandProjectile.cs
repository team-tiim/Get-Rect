using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyHandProjectile : Projectile {

    // Use this for initialization
    protected new void Start()
    {
        spriteSheetName = "weapons2";
        spriteIndex = 5;
        speed = 10;
        animationName = "projectile_holyhand";
        explosionName = "nuke";
        base.Start();
    }

}
