using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Rigidbody2D rb2d;
    public Boolean isInKnocback;
    public Vector3 knockbackDirection;
    public PlayerBehaviour player;

    private  Dictionary<KeyCode, Action> keyActionMap = new Dictionary<KeyCode, Action>();

    //Called before Start, use as constructor
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        player = GetComponent<PlayerBehaviour>();
        keyActionMap.Add(KeyCode.Space, () => DoJump());
        keyActionMap.Add(KeyCode.Mouse0, () => player.DoAttack());
    }

    // Use this for initialization
    private void Start()
    {

    }

	void Update ()
	{

	}

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        if (player.hp <= 0)
        {
            return;
        }


        float moveHorizontal = 0;

        if (Input.GetKey(KeyCode.A))
        {
            moveHorizontal -= player.speed;
        }

        if (Input.GetKey(KeyCode.D))
        {
            moveHorizontal += player.speed;
        }

        foreach (KeyValuePair<KeyCode, Action> entry in keyActionMap)
        {
            if (Input.GetKey(entry.Key)){
                entry.Value();
            }
        }       

        if (player.isInKnockback)
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
        player.UpdateAnimation(moveHorizontal);
    }

    private void DoJump()
    {
        if (!IsGrounded())
        {
            return;
        }
        rb2d.AddForce(new Vector2(0, rb2d.mass * player.jumpPower), ForceMode2D.Impulse);
        player.DoJump();
    }

    private bool IsGrounded()
    {
        return rb2d.velocity.y == 0;
    }
}
