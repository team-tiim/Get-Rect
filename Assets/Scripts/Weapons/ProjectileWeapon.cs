using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class ProjectileWeapon : Weapon
    {
        protected string projectileAnimation;
        protected float projectileSpeed;
        protected float gravityScale;
        protected Vector2 projectileSize;
        protected string projectileSprite;
        protected int projectileSpriteIndex;
        protected string _projectileExplosion = "tiny_explosion";


        public override void Attack(GameObject parent, Vector3 direction)
        {
            if (canAttack())
            {
                //Debug.Log("Projectile attack");
                //Debug.Log(projectileSpeed);
                lastAttack = Time.time;
                GameObject projectile = GameObject.FindGameObjectWithTag("GameController").GetComponent<Weapon>().getProjectile(parent.transform);

                Sprite[] resources = Resources.LoadAll<Sprite>(projectileSprite);
                projectile.GetComponent<SpriteRenderer>().sprite = resources[projectileSpriteIndex];

                projectile.name = "Bullet";
                ProjectileController projectileController = projectile.GetComponent<ProjectileController>();
                projectileController.origin = parent;
                projectileController.weapon = this;

                projectile.GetComponent<Animator>().Play(projectileAnimation);

                Rigidbody2D projectileBody = projectile.GetComponent<Rigidbody2D>();
                projectileBody.gravityScale = gravityScale;
                projectileBody.AddForce(direction.normalized * projectileSpeed, ForceMode2D.Impulse);
            }
        }

        public string projectileExplosion
        {
            get { return _projectileExplosion; }
        }
    }
}
