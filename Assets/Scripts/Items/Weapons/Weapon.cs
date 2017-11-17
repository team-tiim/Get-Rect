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

    private float lastAttack = -1;

    private TimedWeaponBehaviour timedBehaviour;

    public void Awake()
    {
        //this.gameObject.GetComponent<Animator>().Play();
    }

    public virtual void Attack(GameObject parent, Vector3 attackDirection)
    {
        Debug.Log("weapon attack");
        if (!CanAttack())
        {
            return;
        }
        lastAttack = Time.time;
        PlayAnimation();
        DoAttack(parent, attackDirection);
    }

    private bool CanAttack()
    {
        return lastAttack == -1 || (Time.time - lastAttack) > attackCooldown;
    }

    private void PlayAnimation()
    {
        this.gameObject.GetComponent<Animator>().SetTrigger("Attack");
    }

    protected abstract void DoAttack(GameObject parent, Vector3 direction);

    public int Damage
    {
        get { return damage; }
    }

}

