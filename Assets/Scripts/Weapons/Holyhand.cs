using Assets.Scripts;
using Assets.Scripts.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holyhand : ProjectileWeapon
{

	public Holyhand()
	{
		_damage = 15;
		_idleAnimation = "weapon_holyhand_idle";
		projectileSprite = "weapons 2";
		projectileSpriteIndex = 5;
		projectileSpeed = 10;
		projectileAnimation = "projectile_holyhand";
		gravityScale = 0.9f;
		attackCooldown = 0.2f;
	}
}
