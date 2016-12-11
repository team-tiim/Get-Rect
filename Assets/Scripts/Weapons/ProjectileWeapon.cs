using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class ProjectileWeapon : Weapon
    {
        protected string animationName;
        protected float projectileSpeed;
        protected float gravityScale;
        protected Vector2 projectileSize;
        protected string projectileSprite;
        protected int projectileSpriteIndex;


        public override void Attack(GameObject parent, Vector3 direction)
        {
            if (canAttack())
            {
                Debug.Log("Projectile attack");
                lastAttack = Time.time;
                GameObject projectile = GameObject.FindGameObjectWithTag("ScriptsContainer").GetComponent<Weapon>().getProjectile(parent.transform);

                Sprite[] resources = Resources.LoadAll<Sprite>(projectileSprite);
                projectile.GetComponent<SpriteRenderer>().sprite = resources[projectileSpriteIndex];

                projectile.name = "Bullet";
                ProjectileController projectileController = projectile.GetComponent<ProjectileController>();
                projectileController.speed = projectileSpeed;
                projectileController.direction = direction;
                projectileController.origin = parent;

                //BoxCollider2D boxCollider2d = projectileController.GetComponent<BoxCollider2D>();
                //SpriteRenderer spriteRenderer = projectileController.GetComponent<SpriteRenderer>();
                //Sprite sprite = spriteRenderer.sprite;
                //Debug.Log(spriteRenderer.bounds);
                ////Debug.Log(spriteRenderer.);
                //Debug.Log(sprite);
                //boxCollider2d.size = sprite.bounds.size;

                //Sprite[] resources = Resources.LoadAll<Sprite>("");
                //Debug.Log(projectileSprite);
                //Debug.Log(resources.Length);
                //Debug.Log(resources);

                //var anim = projectile.AddComponent<Animator>();
                //var ctrl = new RuntimeAnimatorController();
                //ctrl.animationClips[0] = resources[0].clip;
                //anim.runtimeAnimatorController = ctrl;

                //projectile.GetComponent<Animation>().AddClip(resources[0].clip, "asd");
                //projectile.GetComponent<Animation>().AddClip(resources[0].clip, "asd2");

                //Animation animation = projectile.GetComponent<Animation>();
                //animation.clip.legacy = true;
                //Debug.Log(animation.isPlaying);
                //Debug.Log(animation.clip.isLooping);

                //Debug.Log(animation.clip.length);
                //animation.AddClip(resources[0].clip, "asd");
                //animation.AddClip(resources[0].clip, "asd2");
                //animation.AddClip(resources[0].clip, "asd3");
                //animation.clip.isLooping = true;

                //projectile.GetComponent<Animation>().clip.legacy = true;

                //projectile.GetComponent<SpriteRenderer>().sprite = resources[0];
                //projectile.GetComponent<AnimationClip>(). = resources[0];
                //Animator anim = projectile.GetComponent<Animator>();
                projectile.GetComponent<Animator>().Play(animationName);

                
                
                //projectile.GetComponent<BoxCollider2D>().size = projectile.GetComponent<Animator>().GetComponent<Animation>().GetComponent<Renderer>().bounds.size;

                projectile.GetComponent<Rigidbody2D>().gravityScale = gravityScale;
            }

            
        }
    }
}
