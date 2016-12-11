using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : Weapon {

    public Fish()
    {
        _damage = 100;
        _idleAnimation = "weapon_fish_idle";
        _swingLength = 1.2f;
        _swingRadius = 0.5f;
        attackCooldown = 1f;
    }
}
