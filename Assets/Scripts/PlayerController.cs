using Assets.Scripts;
using Assets.Scripts.Weapons;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : CharacterBehaviourBase

{
    public GameObject closestPlatform;

    protected GameObject rightHand;
    protected GameObject leftHand;

    // Use this for initialization
    protected new void Start()
    {
        base.Start();
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        //Debug.Log(rb2d.transform.position.x);
        rightHand = gameObject.transform.GetChild(0).gameObject;
        leftHand = gameObject.transform.GetChild(1).gameObject;
        jumpsound = GetComponent<AudioSource> ();
        selectWeapon(gameObject.AddComponent<Pistol>());
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
			jumpsound.Play();
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

        if (Input.GetKey(KeyCode.Mouse0))
        {
            Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pz.z = 0;
            
            Vector3 direction = pz - transform.position;
            Debug.Log(transform.rotation.ToString());
            if (direction.x > 0 && flipped || direction.x <= 0 && !flipped)
            {
                this.leftHand.GetComponent<Animator>().SetTrigger("Attack");
            } else
            {
                this.rightHand.GetComponent<Animator>().SetTrigger("Attack");
            }
            
            Attack(direction);
        }

        Vector2 movement = new Vector2(moveHorizontal, rb2d.velocity.y);

        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        rb2d.velocity = movement;
        updateAnimation(moveHorizontal);
    }

    public override void selectWeapon(Weapon weapon)
    {
        base.selectWeapon(weapon);

        this.rightHand.GetComponent<Animator>().Play(weapon.idleAnimation);
        this.leftHand.GetComponent<Animator>().Play(weapon.idleAnimation);
    }

    private void exitGame()
    {
        Debug.Log("exitGame");
        SceneManager.LoadScene("menu");
    }
}
