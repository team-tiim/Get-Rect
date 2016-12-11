using Assets.Scripts;
using Assets.Scripts.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : ProjectileWeapon
{

    public Tank()
    {
        _damage = 100;
        _idleAnimation = "weapon_tank_idle";
        projectileSprite = "weapons 1";
        projectileSpriteIndex = 7;
        projectileSpeed = 20;
        projectileAnimation = "projectile_pistol";
        gravityScale = 0.1f;
        attackCooldown = 1;
        _projectileExplosion = "explosion";
    }
}
