using Assets.Scripts;
using Assets.Scripts.Weapons;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterBehaviourBase

{

    public float jumpPower;

	private AudioSource jumpsound;

    // Use this for initialization
    protected new void Start()
    {
        base.Start();
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D>();        
        animator = GetComponent<Animator>();
        Debug.Log(rb2d.transform.position.x);
		jumpsound = GetComponent<AudioSource> ();
        selectedWeapon = gameObject.AddComponent<Pistol>();
    }

    bool IsGrounded()
    {
        return rb2d.velocity.y == 0;
    }


    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
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
            //Debug.Log("Click");
            //Debug.Log(transform.position);
            //Debug.Log(Input.mousePosition);
            //Debug.Log(transform.position + Input.mousePosition);
            //Debug.Log((transform.position + Input.mousePosition).normalized);
            //Debug.Log("Angle");
            Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Debug.Log(Camera.main);
            pz.z = 0;
            //Debug.Log("Click");
            //Debug.Log(transform.position);
            //Debug.Log(pz);
            //Debug.Log(pz - transform.position);
            //Debug.Log((pz - transform.position).normalized);
            //Debug.Log("Angle");
            //Debug.Log(Vector2.Angle(new Vector2(transform.position.x, transform.position.y), Input.mousePosition));
            Attack((pz - transform.position).normalized);
        }

        //Store the current horizontal input in the float moveHorizontal.
        //float moveHorizontal = Input.GetAxis("Horizontal") * speed;

        //Use the two store floats to create a new Vector2 variable movement.
        Vector2 movement = new Vector2(moveHorizontal, rb2d.velocity.y);

        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        //rb2d.AddForce(movement * speed);
        rb2d.velocity = movement;


        updateAnimation(moveHorizontal);
    }
}
