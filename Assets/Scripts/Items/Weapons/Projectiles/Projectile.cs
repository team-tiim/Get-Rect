using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    //visual
    protected string animationName;
    protected string spriteSheetName;
    protected int spriteIndex;
    protected string explosionName = "tiny_explosion";
    //stats
    protected float gravityScale;
    protected float speed;
    protected Vector2 size;

    protected Rigidbody2D rb2d;
    protected ProjectileWeapon weapon;
    protected Vector3 moveDirection;

    // Use this for initialization
    protected void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        //TODO võiks eraldi prefabis enne seada spraidi
        Sprite[] resources = Resources.LoadAll<Sprite>(spriteSheetName);
        GetComponent<SpriteRenderer>().sprite = resources[spriteIndex];
        GetComponent<BoxCollider2D>().size = GetComponent<SpriteRenderer>().bounds.size;

        //why do we have this animation name here?
        GetComponent<Animator>().Play(animationName);
        rb2d.gravityScale = gravityScale;
        rb2d.AddForce(moveDirection.normalized * speed, ForceMode2D.Impulse);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetVariables(ProjectileWeapon weapon, Vector3 moveDirection)
    {
        Debug.Log("projectile spawn");
        this.weapon = weapon;
        this.moveDirection = moveDirection;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("bullet hit");
        ////all projectile colliding game objects should be tagged "Enemy" or whatever in inspector but that tag must be reflected in the below if conditional
        if (col.gameObject.tag == "Enemy")
        {
            //Destroy(col.gameObject);
            col.gameObject.GetComponent<EnemySimpleBehaviour>().TakeDamage(weapon.Damage);
            //add an explosion or something
            //destroy the projectile that just caused the trigger collision
            Destroy(gameObject);
        }
        GameObject controller = GameObject.Find("GameControllers");
        controller.GetComponent<GameController>().doExplosion(this.transform.position, explosionName);
        Destroy(gameObject);
    }

    public float Speed
    {
        set { speed = value; }
    }
}
