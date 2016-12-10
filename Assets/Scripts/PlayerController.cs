using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterBehaviourBase

{

    public float jumpPower;

    private Rigidbody2D rb2d;       //Store a reference to the Rigidbody2D component required to use 2D Physics.
	private AudioSource jumpsound;

    // Use this for initialization
    protected void Start()
    {
        base.Start();
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D>();        
        animator = GetComponent<Animator>();
        Debug.Log(rb2d.transform.position.x);
		jumpsound = GetComponent<AudioSource> ();
    }

    bool IsGrounded()
    {
        return rb2d.velocity.y == 0;
    }


    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space) && IsGrounded())
        {
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
