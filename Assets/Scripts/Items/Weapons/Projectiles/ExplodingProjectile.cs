using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingProjectile : Projectile {

    public GameObject destroyAnimation;
    public float detonationTime = 0.75f;
    public float radius = 1.0f;

    public override void Awake()
    {
        speed = 10;
        gravityScale = 1f;
        base.Awake();
    }

    public override void Start()
    {
        base.Start();
        StartCoroutine(DoDamage());
    }

    private IEnumerator DoDamage()
    {
        yield return new WaitForSeconds(detonationTime);
        //TODO change layer on enemies
        int layer = LayerMask.NameToLayer("Enemy");
        Collider2D[] colliders = Physics2D.OverlapCircleAll(gameObject.transform.position, radius, layer);
        foreach (Collider2D col in colliders)
        {
            if (col.gameObject.tag == "Enemy")
            {
                Debug.Log("projectile hit: " + col.gameObject.name);
            }
        }

        Destroy(gameObject);

        if (destroyAnimation != null)
        {
            GameObjectUtils.Instance.InstantiateAndPlayAnimation(destroyAnimation, gameObject.transform.position);
        }
    }

}
