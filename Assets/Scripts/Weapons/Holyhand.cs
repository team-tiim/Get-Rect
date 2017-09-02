using Assets.Scripts;
using Assets.Scripts.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holyhand : ProjectileWeapon
{

	public Holyhand()
	{
		damage = 15;
		idleAnimation = "weapon_holyhand_idle";
        gravityScale = 0.1f;
		attackCooldown = 0.2f;
	}
}
