using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    public float swingLength;
    public float swingRadius;

    protected override void DoAttack(GameObject parent, Vector3 direction)
    {
        RaycastHit2D[] targets = Physics2D.CircleCastAll(parent.transform.position, swingRadius, direction, swingLength);
        //Debug.Log(targets.Length);
        foreach (RaycastHit2D target in targets)
        {
            if (target.transform.CompareTag("Enemy"))
            {
                //Debug.Log("Hit: " + target.transform.gameObject.name);
                target.transform.gameObject.GetComponent<EnemySimpleBehaviour>().TakeDamage(damage);
            }

        }
    }
}
