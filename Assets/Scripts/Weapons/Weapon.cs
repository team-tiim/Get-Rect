using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class Weapon : MonoBehaviour
    {
        protected float damage;
        protected string sprite;
        protected float attackCooldown = 1;
        protected float lastAttack;
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
    }
}
