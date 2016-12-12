using Assets.Scripts;
using Assets.Scripts.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uzi : ProjectileWeapon
{

    public Uzi()
    {
        _damage = 1;
        _idleAnimation = "weapon_uzi_idle";
        projectileSprite = "weapons 1";
        projectileSpriteIndex = 7;
        projectileSpeed = 20;
        projectileAnimation = "projectile_pistol";
        gravityScale = 0.5f;
        attackCooldown = 0.001f;
    }
}
