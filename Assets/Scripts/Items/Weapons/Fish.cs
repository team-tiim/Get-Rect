using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MeleeWeapon
{

    public Fish()
    {
        damage = 100;
        idleAnimation = "weapon_fish_idle";
        swingLength = 1.2f;
        swingRadius = 0.5f;
        attackCooldown = 1f;
    }
}
