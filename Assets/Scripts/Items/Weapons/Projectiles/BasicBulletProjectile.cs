using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBulletProjectile: Projectile {

    public override void Awake()
    {
        speed = 10;
        base.Awake();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("projectile hit: " + col.gameObject.tag);
        if (col.gameObject.tag == "Enemy")
        {
            col.gameObject.GetComponent<EnemySimpleBehaviour>().TakeDamage(weapon.Damage);
        }
        Destroy(gameObject);
    }

}
