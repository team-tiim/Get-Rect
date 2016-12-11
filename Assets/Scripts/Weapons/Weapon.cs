using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class Weapon : MonoBehaviour
    {
        protected int _damage;
        protected string _idleAnimation;
        protected float attackCooldown = 1;
        protected float lastAttack;
        protected float _swingLength;
        protected float _swingRadius;


        public GameObject projectilePrefab;

        public virtual void Attack(GameObject parent, Vector3 direction)
        {
            if (canAttack())
            {
                Debug.Log("Regular weapon");
                lastAttack = Time.time;


            }
        }

        protected bool canAttack()
        {
            return (Time.time - lastAttack) > attackCooldown;
        }

        public GameObject getProjectile(Transform parentTransform)
        {
            return Instantiate(projectilePrefab, parentTransform.position, Quaternion.identity);
        }

        public int damage
        {
            get { return _damage; }
        }

        public string idleAnimation
        {
            get { return _idleAnimation; }
        }
    }
}
