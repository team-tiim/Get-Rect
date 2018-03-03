using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour {
    
    //visual
    //stats
    protected float gravityScale = 0f;
    protected float speed;
    protected Vector2 size;

    protected Rigidbody2D rb2d;
    protected Weapon weapon;
    protected Vector3 moveDirection;

    // Use this for initialization
    public virtual void Awake() { 
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.gravityScale = gravityScale;

        GetComponent<BoxCollider2D>().size = GetComponent<SpriteRenderer>().bounds.size;
    }

    public virtual void Start()
    {
        rb2d.AddForce(moveDirection.normalized * speed, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void SetVariables(Weapon weapon, Vector3 moveDirection)
    {
        //Debug.Log("projectile spawn");
        this.weapon = weapon;
        this.moveDirection = moveDirection;
    }

    public float Speed
    {
        set { speed = value; }
    }
}
