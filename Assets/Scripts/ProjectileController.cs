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
    }

    void OnTriggerEnter2D(Collider2D col)
    {
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
            Destroy(gameObject);
        }
        
    }
}
