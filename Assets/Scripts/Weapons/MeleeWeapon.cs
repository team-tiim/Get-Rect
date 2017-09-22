using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class MeleeWeapon : Weapon
    {
        protected float swingLength;
        protected float swingRadius;

        // Use this for initialization
        void Start()
        {

        }

        protected override void DoAttack(GameObject parent, Vector3 direction)
        {
            //Debug.Log("Regular weapon");
            lastAttack = Time.time;

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
}
