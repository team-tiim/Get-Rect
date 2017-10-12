﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehaviourBase : MonoBehaviour {

    public float jumpPower;
    public float speed = 1;  //Floating point variable to store the player's movement speed.
    public int hp = 10;

    protected Rigidbody2D rb2d;       //Store a reference to the Rigidbody2D component required to use 2D Physics.
    protected SpriteRenderer spriteRenderer;
    protected Vector3 size;
    protected List<Animator> animators = new List<Animator>();
    protected Animator armorAnimator;
    protected AudioSource jumpsound;
    // add health bar

    protected GameObject equippedWeapon;
    protected Color origColor;
    public GameObject origWeapon;
    public Armor armor;

    protected bool isInKnockback;

    // Use this for initialization
    protected void Start () {
        Transform animObj = transform.Find("animationComponent");
        if (animObj != null)
        {
            spriteRenderer = animObj.GetComponent<SpriteRenderer>();
            animators.Add(animObj.GetComponent<Animator>());
        } else {
            spriteRenderer = GetComponent<SpriteRenderer>();
            animators.Add(GetComponent<Animator>());
        }
        animObj = transform.Find("armorAnimationComponent");
        if (animObj != null)
        {
            animators.Add(animObj.GetComponent<Animator>());
        }
        rb2d = GetComponent<Rigidbody2D>();
        size = spriteRenderer.sprite.bounds.size;
        origColor = spriteRenderer.color;
        GetComponent<BoxCollider2D>().size = size;
    }
	
	// Update is called once per frame
	void Update () {		
	}

    protected bool IsGrounded()
    {
        return rb2d.velocity.y == 0;
    }

    protected void UpdateAnimation(float moveHorizontal)
    {
        foreach (Animator animator in animators)
        {
            if (moveHorizontal > 0)
            {
                animator.transform.localRotation = Quaternion.Euler(0, 0, 0);
                animator.SetBool("isMove", true);
            }
            else if (moveHorizontal < 0)
            {
                animator.transform.localRotation = Quaternion.Euler(0, 180, 0);
                animator.SetBool("isMove", true);
            }
            else
            {
                animator.SetBool("isMove", false);
            }
        }
    }

    protected void Attack(Vector3 direction)
    {
        //Debug.Log(this.equippedWeapon.GetComponent<Weapon>());
        equippedWeapon.GetComponent<Weapon>().Attack(gameObject, direction);
    }

    protected void Attack(GameObject target, int damage)
    {
        foreach (Animator animator in animators)
        {
            animator.SetTrigger("doAttack");
        }
        CharacterBehaviourBase cbb = target.GetComponent<CharacterBehaviourBase>();
        cbb.TakeDamage(damage);
        Vector2 knockBackDir = (target.transform.position - transform.position).normalized;
        //Debug.Log(knockBackDir);
        cbb.DoKnockback(knockBackDir * 20);
    }

    public void TakeDamage(int damage)
    {
        OnDamage(damage);
        if (hp <= 40)
        {
            animators[0].SetLayerWeight(0, 0.0f);
            animators[0].SetLayerWeight(1, 1.0f);
        }
        if (hp <= 0)
        {
            OnDeath();
        }
    }

    protected virtual void OnDeath()
    {
        GameObject.Destroy(this.gameObject);
        GameObject controller = GameObject.Find("GameControllers");
        controller.GetComponent<GameController>().doExplosion(this.transform.position);
    }

    protected virtual void OnDamage(int damage)
    {
        if(armor != null)
        {
            damage = armor.BlockDamage(damage);
        }
        hp -= damage;
    }

    public virtual void EquipWeapon(GameObject weapon)
    {
        equippedWeapon = weapon;
    }

    public virtual void EquipArmor(Armor armor)
    {
        if(this.armor == null)
        {
            this.armor = armor;
        }
        else
        {
            this.armor.Increase(armor.Value);
        }

    }

    public void ResetWeapon()
    {
        EquipWeapon(origWeapon);
    }

    public virtual void DoKnockback(Vector3 direction)
    {
        isInKnockback = true;
        rb2d.AddForce(direction, ForceMode2D.Impulse);
        Invoke("setKnockbackFalse", 1);
    }

    private void setKnockbackFalse()
    {
        isInKnockback = false;
    }
}
