using Assets.Scripts;
using Assets.Scripts.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catapult : ProjectileWeapon
{

    public Catapult()
    {
        _damage = 100;
        _idleAnimation = "weapon_catapult_idle";
        gravityScale = 0.1f;
        attackCooldown = 1f;
    }
}
