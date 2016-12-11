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
        projectileSprite = "weapons 1";
        projectileSpriteIndex = 7;
        projectileSpeed = 5;
        projectileAnimation = "projectile_pistol";
        gravityScale = 0.1f;
        attackCooldown = 1f;
        _projectileExplosion = "explosion";
    }
}
