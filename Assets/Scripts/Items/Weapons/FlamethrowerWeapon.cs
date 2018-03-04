using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamethrowerWeapon : BaseProjectileWeapon
{

    public float length;
    public float radius;
    public float dps;

    private bool isAttacking;
    private Time lastDamageTime;

    public override void Awake()
    {
        projectileSpawnPoint = transform.Find("projectileSpawnPoint");

        base.Awake();
    }

    public override void DoAttack(WeaponAttackParams parameters)
    {
        if (!CanAttack())
        {
            return;
        }
        base.DoAttack(parameters);

        //TODO play animation
        RaycastHit2D[] hits = Physics2D.CircleCastAll(projectileSpawnPoint.position, radius, parameters.direction, length);
        Debug.DrawRay(projectileSpawnPoint.position, parameters.direction.normalized * length, Color.red);
        Debug.Log("Player flamethrower attack");
        foreach (RaycastHit2D hit in hits)
        {
            Debug.Log(hit.collider.tag);
        }

    }

    public void StopAttack()
    {
        isAttacking = false;
        //TODO stop animation
    }

    public override WeaponType GetWeaponType()
    {
        return WeaponType.CONTINUOUS;
    }
}
