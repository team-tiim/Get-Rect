using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public abstract class Weapon : MonoBehaviour
{
    public int damage = 1;
    public float attackCooldown = 1;
    public float knockback;
    public string idleAnimation;
    private float lastAttack = -1;

    private TimedWeaponBehaviour timedBehaviour;

    public virtual void Attack(GameObject parent, Vector3 attackDirection)
    {
        if (!CanAttack())
        {
            return;
        }
        lastAttack = Time.time;
        DoAttack(parent, attackDirection);
    }

    private bool CanAttack()
    {
        return lastAttack == -1 || (Time.time - lastAttack) > attackCooldown;
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

