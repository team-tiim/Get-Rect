using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class Weapon : MonoBehaviour
    {
        protected int damage;
        protected float knockback;
        protected int cooldown;
        protected string idleAnimation;
        protected float attackCooldown = 1;
        protected float lastAttack;

        public virtual void Attack(GameObject parent, Vector3 attackDirection)
        {
            if (!CanAttack())
            {
                return;
            }
            DoAttack(parent, attackDirection);
        }

        private bool CanAttack()
        {
            return (Time.time - lastAttack) > attackCooldown;
        }

        protected abstract void DoAttack(GameObject parent, Vector3 direction);

        public int Damage
        {
            get { return damage; }
        }

        public string IdleAnimation
        {
            get { return idleAnimation; }
        }
    }
}
