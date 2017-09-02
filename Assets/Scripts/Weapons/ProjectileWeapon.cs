using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class ProjectileWeapon : Weapon
    {
        protected float knockback;
        protected float gravityScale;
        protected GameObject projectilePrefab;
        protected int projectileSpeed;

        // Use this for initialization
        void Start()
        {
            GameController gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
            projectilePrefab = gc.basicBulletPrefab;
        }


        protected override void doAttack(GameObject parent, Vector3 attackDirection)
        {
            spawnPojectile(parent, attackDirection);
            doKnockback(attackDirection);
        }

        private void spawnPojectile(GameObject parent, Vector3 attackDirection)
        {
            lastAttack = Time.time;

            Projectile p = Instantiate(projectilePrefab, parent.transform.position, Quaternion.identity).GetComponent<Projectile>();
            p.SetVariables(parent, this, attackDirection);

            if(projectileSpeed > 0)
            {
                p.Speed = projectileSpeed;
            }

        }

        private void doKnockback(Vector3 attackDirection)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            PlayerController pc = player.GetComponent<PlayerController>();
            //player.GetComponent<Rigidbody2D>().AddForce(-attackDirection.normalized * knockback, ForceMode2D.Impulse);
            pc.doKnockback(-attackDirection.normalized * knockback);
        }

    }
}
