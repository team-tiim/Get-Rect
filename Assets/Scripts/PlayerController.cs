﻿using Assets.Scripts;
using Assets.Scripts.Weapons;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : CharacterBehaviourBase

{
    public GameObject closestPlatform;
	public AudioSource[] sounds;
	public AudioSource bgm;

    public Boolean isInKnocback;
    public Vector3 knockbackDirection;

    protected GameObject rightHand;
    protected GameObject leftHand;

    // Use this for initialization
    protected new void Start()
    {
        base.Start();
        this.rightHand = gameObject.transform.GetChild(0).gameObject;
        this.leftHand = gameObject.transform.GetChild(1).gameObject;
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        Debug.Log(rb2d.transform.position.x);

        selectWeapon(gameObject.AddComponent<Pistol>());
		jumpsound = sounds [0];
		bgm = sounds [1]; 
        
    }
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.M)) {

			if (bgm.isPlaying) {
				bgm.Pause ();
			} else { 
				bgm.UnPause ();
			}
		}
	}


    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            exitGame();
        }

        if (this.hp <= 0)
        {
            return;
        }

        animator.SetBool("isGrounded", IsGrounded());

        if (Input.GetKey(KeyCode.Space) && IsGrounded())
        {
            animator.SetTrigger("doJump");
            rb2d.AddForce(new Vector2(0, rb2d.mass * jumpPower), ForceMode2D.Impulse);

			if (!jumpsound.isPlaying)
			{
				jumpsound.Play ();
			} 
				

		
        }

        float moveHorizontal = 0;

        if (Input.GetKey(KeyCode.A))
        {
            moveHorizontal -= speed;
        }

        if (Input.GetKey(KeyCode.D))
        {
            moveHorizontal += speed;
        }

        if (Input.GetKey(KeyCode.Alpha1))
        {
            if (selectedWeapon)
            {
                GameObject.Destroy(selectedWeapon);
            }
            selectWeapon(gameObject.AddComponent<Knife>());
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            if (selectedWeapon)
            {
                GameObject.Destroy(selectedWeapon);
            }
            selectWeapon(gameObject.AddComponent<Pistol>());
        }

        if (Input.GetKey(KeyCode.Alpha3))
        {
            if (selectedWeapon)
            {
                GameObject.Destroy(selectedWeapon);
            }
            selectWeapon(gameObject.AddComponent<Fish>());
        }

        if (Input.GetKey(KeyCode.Alpha4))
        {
            if (selectedWeapon)
            {
                GameObject.Destroy(selectedWeapon);
            }
            selectWeapon(gameObject.AddComponent<Uzi>());
        }

        if (Input.GetKey(KeyCode.Alpha5))
        {
            if (selectedWeapon)
            {
                GameObject.Destroy(selectedWeapon);
            }
            selectWeapon(gameObject.AddComponent<Catapult>());
        }

        if (Input.GetKey(KeyCode.Alpha6))
        {
            if (selectedWeapon)
            {
                GameObject.Destroy(selectedWeapon);
            }
            selectWeapon(gameObject.AddComponent<Tank>());
		}

		if (Input.GetKey(KeyCode.Alpha7))
		{
			if (selectedWeapon)
			{
				GameObject.Destroy(selectedWeapon);
			}
			selectWeapon(gameObject.AddComponent<Holyhand>());
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pz.z = 0;
            
            Vector3 direction = pz - transform.position;
            if (direction.x > 0 && flipped || direction.x <= 0 && !flipped)
            {
                this.leftHand.GetComponent<Animator>().SetTrigger("Attack");
            } else
            {
                this.rightHand.GetComponent<Animator>().SetTrigger("Attack");
            }
            
            Attack(direction);
        }
        if (isInKnockback)
        {
            return;
        }

        Vector2 movement = new Vector2(moveHorizontal, rb2d.velocity.y);

        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        rb2d.velocity = movement;
        //knockbackDirection = null;
        updateAnimation(moveHorizontal);
    }

    protected override void onDeath()
    {
        GetComponent<Animator>().Play("player_death");
    }

    protected override void onDamage(int damage)
    {
        base.onDamage(damage);
        StartCoroutine(Utils.ChangeColor(this.spriteRenderer, this.origColor));
    }

    private void exitGame()
    {
        Debug.Log("exitGame");
        SceneManager.LoadScene("menu");
    }

    public override void selectWeapon(Weapon weapon)
    {
        base.selectWeapon(weapon);

        this.rightHand.GetComponent<Animator>().Play(selectedWeapon.idleAnimation);
        this.leftHand.GetComponent<Animator>().Play(selectedWeapon.idleAnimation);
    }
}
