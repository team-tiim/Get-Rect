using Assets.Scripts;
using Assets.Scripts.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uzi : ProjectileWeapon
{

    public Uzi()
    {
        damage = 1;
        idleAnimation = "weapon_uzi_idle";
        gravityScale = 0.5f;
        attackCooldown = 0.001f;
    }
}
