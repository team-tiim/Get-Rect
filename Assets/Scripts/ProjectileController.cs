using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{

    //public float speed;
    public GameObject origin;
    //public Vector3 direction;
    public Weapon weapon;

    private Rigidbody2D rb2d;

    // Use this for initialization
    void Start()
    {
        GetComponent<BoxCollider2D>().size = GetComponent<SpriteRenderer>().bounds.size;
        rb2d = GetComponent<Rigidbody2D>();
        
        //BoxCollider2D boxCollider2d = GetComponent<BoxCollider2D>();
        //SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        //Sprite sprite = spriteRenderer.sprite;
        ////Debug.Log(sprite);
        //boxCollider2d.size = sprite.bounds.size;
        //Debug.Log("Projectile size");
        //Debug.Log(GetComponent<Animator>().GetComponent<Animation>().GetComponent<Renderer>().bounds.size);
        //Debug.Log(GetComponent<Animator>().runtimeAnimatorController.animationClips[0].localBounds.size);
        //GetComponent<BoxCollider2D>().size = GetComponent<Animator>().runtimeAnimatorController.animationClips[0].localBounds.size;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log("Velocity");
        //Debug.Log(rb2d.velocity);
        //Debug.Log(speed);
        //Debug.Log(transform.position);
        //Debug.Log(direction);
        //transform.position = transform.position + (direction * speed * Time.deltaTime);
        //transform.Translate(direction.normalized * speed * Time.deltaTime);
        //Debug.Log(direction.normalized * speed * Time.deltaTime);
        //Debug.Log(transform.position.magnitude);
        //Debug.Log(transform.position);
        //Vector2 movement = new Vector2(speed, speed);

        //rb2d.velocity = direction * speed * Time.deltaTime;
        //rb2d.AddForce(direction * speed * Time.deltaTime);
        //rb2d.AddForce(direction * speed);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log("Collision");

        //Debug.Log(col.gameObject.name);
        ////all projectile colliding game objects should be tagged "Enemy" or whatever in inspector but that tag must be reflected in the below if conditional
        if (col.gameObject.tag == "Enemy")
        {
            //Destroy(col.gameObject);
            col.gameObject.GetComponent<EnemySimpleBehaviour>().takeDamage(weapon.damage);
            //add an explosion or something
            //destroy the projectile that just caused the trigger collision
            Destroy(gameObject);
        }
        if (!col.gameObject.tag.Equals(origin.tag))
        {
            //Debug.Log("Destroy");
            //Debug.Log(gameObject.tag);
            //Debug.Log(gameObject.name);
            //Debug.Log(col.gameObject.name);
            //Debug.Log(col.gameObject.tag);
            Destroy(gameObject);
        }
        
    }
}
