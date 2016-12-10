using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class ProjectileWeapon : Weapon
    {
        public string projectileSprite;
        public float projectileSpeed;

        public override void Attack()
        {
            Debug.Log("Projectile attack");
            GameObject projectile = GameObject.FindGameObjectWithTag("ScriptsContainer").GetComponent<Weapon>().getProjectile();

            projectile.name = "Bullet";
            Sprite[] resources = Resources.LoadAll<Sprite>("");
            Debug.Log(projectileSprite);
            Debug.Log(resources.Length);
            Debug.Log(resources);
            projectile.GetComponent<SpriteRenderer>().sprite = resources[0];
            projectile.GetComponent<ProjectileController>().speed = projectileSpeed;
        }
    }
}
