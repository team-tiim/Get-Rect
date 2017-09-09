﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : CharacterBehaviourBase

{
    public GameObject[] weaponPrefabs;
    public GameObject[] weapons;

    public GameObject closestPlatform;
	public AudioSource[] sounds;
	public AudioSource bgm;

    public Boolean isInKnocback;
    public Vector3 knockbackDirection;

    protected GameObject rightHand;
    protected GameObject leftHand;

    private  Dictionary<KeyCode, Action> keyActionMap = new Dictionary<KeyCode, Action>();

    //Called before Start, use as constructor
    private void Awake()
    {
        weapons = new GameObject[weaponPrefabs.Length];
        for (int i = 0; i < weaponPrefabs.Length; ++i)
        {
            weapons[i] = Instantiate(weaponPrefabs[i]);
        }
        keyActionMap.Add(KeyCode.Space, () => DoJump());
        keyActionMap.Add(KeyCode.Mouse0, () => DoAttack());
        keyActionMap.Add(KeyCode.M, () => DoPause());
        //weapons
        keyActionMap.Add(KeyCode.Alpha1, () => SelectWeapon(weapons[0]));
        keyActionMap.Add(KeyCode.Alpha2, () => SelectWeapon(weapons[1]));
        keyActionMap.Add(KeyCode.Alpha3, () => SelectWeapon(weapons[2]));
        keyActionMap.Add(KeyCode.Alpha4, () => SelectWeapon(weapons[3]));
        keyActionMap.Add(KeyCode.Alpha5, () => SelectWeapon(weapons[4]));
        keyActionMap.Add(KeyCode.Alpha6, () => SelectWeapon(weapons[5]));
        keyActionMap.Add(KeyCode.Alpha7, () => SelectWeapon(weapons[6]));
    }

    // Use this for initialization
    protected new void Start()
    {
        base.Start();
        this.rightHand = gameObject.transform.GetChild(0).gameObject;
        this.leftHand = gameObject.transform.GetChild(1).gameObject;
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        origWeapon = weapons[2];
        SelectWeapon(origWeapon);
		jumpsound = sounds [0];
		bgm = sounds [1];         
    }

	void Update ()
	{

	}

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            ExitGame();
        }

        if (this.hp <= 0)
        {
            return;
        }

        animator.SetBool("isGrounded", IsGrounded());
        float moveHorizontal = 0;

        if (Input.GetKey(KeyCode.A))
        {
            moveHorizontal -= speed;
        }

        if (Input.GetKey(KeyCode.D))
        {
            moveHorizontal += speed;
        }

        foreach (KeyValuePair<KeyCode, Action> entry in keyActionMap)
        {
            if (Input.GetKey(entry.Key)){
                entry.Value();
            }
        }       

        if (isInKnockback)
        {
            return;
        }
        /*
        Debug.Log(IsGrounded());
        float yvel = 0;
         if (!IsGrounded())
        {
        yvel = rb2d.velocity.y + Physics.gravity.y * Time.deltaTime;
         }
        */
        
        Vector2 movement = new Vector2(moveHorizontal, rb2d.velocity.y);

        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        rb2d.velocity = movement;
        //knockbackDirection = null;
        UpdateAnimation(moveHorizontal);
    }

    protected override void OnDeath()
    {
        GetComponent<Animator>().Play("player_death");
    }

    protected override void OnDamage(int damage)
    {
        base.OnDamage(damage);
        StartCoroutine(Utils.ChangeColor(this.spriteRenderer, this.origColor));
    }

    private void ExitGame()
    {
        Debug.Log("exitGame");
        SceneManager.LoadScene("menu");
    }

    private void DoPause()
    {
        //TODO see peaks mängu pausima mitte mingi audio klipi?
        if (bgm.isPlaying)
        {
            bgm.Pause();
        }
        else
        {
            bgm.UnPause();
        }
    }

    private void DoJump()
    {
        if (!IsGrounded())
        {
            return;
        }
        animator.SetTrigger("doJump");
        rb2d.AddForce(new Vector2(0, rb2d.mass * jumpPower), ForceMode2D.Impulse);

        if (!jumpsound.isPlaying)
        {
            jumpsound.Play();
        }
    }
    private void DoAttack()
    {
        Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pz.z = 0;

        Vector3 direction = pz - transform.position;
        if (direction.x > 0 && flipped || direction.x <= 0 && !flipped)
        {
            this.leftHand.GetComponent<Animator>().SetTrigger("Attack");
        }
        else
        {
            this.rightHand.GetComponent<Animator>().SetTrigger("Attack");
        }

        Attack(direction);
    }

    public override void SelectWeapon(GameObject weapon)
    {
        base.SelectWeapon(weapon);
        Weapon w = selectedWeapon.GetComponent<Weapon>();
        this.rightHand.GetComponent<Animator>().Play(w.IdleAnimation);
        this.leftHand.GetComponent<Animator>().Play(w.IdleAnimation);
    }
}
