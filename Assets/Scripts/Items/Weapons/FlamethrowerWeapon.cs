using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamethrowerWeapon : Weapon
{

    public float length;
    public float radius;
    public float dps;

    private bool isAttacking;
    private Transform flame;
    private Animator flameAnimator;

    public override void Awake()
    {
        flame = transform.Find("flame");    
        flameAnimator = flame.gameObject.GetComponent<Animator>();
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
        RaycastHit2D[] hits = Physics2D.CircleCastAll(flame.position, radius, parameters.direction, length);
        Debug.DrawRay(flame.position, parameters.direction.normalized * length, Color.red);
        foreach (RaycastHit2D hit in hits)
        {
            GameObject hitGo = hit.collider.gameObject;
            if (hitGo.tag == "Enemy" && hitGo.GetComponent<OngoingDamageEffect>() == null)
            {
                OngoingDamageEffect sc = hitGo.AddComponent<OngoingDamageEffect>() as OngoingDamageEffect;
            }
        }

        if (!isAttacking)
        {
            flameAnimator.SetTrigger("startFire");
        }

        isAttacking = true;
    }

    public void StopAttack()
    {
        isAttacking = false;
        flameAnimator.SetTrigger("stopFire");
        //TODO stop animation
    }

    public override WeaponType GetWeaponType()
    {
        return WeaponType.CONTINUOUS;
    }
}
