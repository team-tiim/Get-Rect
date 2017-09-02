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

        public virtual void Attack(GameObject parent, Vector3 attackDirection)
        {
            if (!canAttack())
            {
                return;
            }
            doAttack(parent, attackDirection);
        }

        protected bool canAttack()
        {
            return (Time.time - lastAttack) > attackCooldown;
        }

        protected virtual void doAttack(GameObject parent, Vector3 direction)
        {
            Debug.Log("Regular weapon");
            lastAttack = Time.time;

            RaycastHit2D[] targets = Physics2D.CircleCastAll(parent.transform.position, _swingRadius, direction, _swingLength);
            Debug.Log(targets.Length);
            foreach (RaycastHit2D target in targets)
            {
                if (target.transform.CompareTag("Enemy"))
                {
                    Debug.Log("Hit: " + target.transform.gameObject.name);
                    target.transform.gameObject.GetComponent<EnemySimpleBehaviour>().takeDamage(damage);
                }

            }
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
