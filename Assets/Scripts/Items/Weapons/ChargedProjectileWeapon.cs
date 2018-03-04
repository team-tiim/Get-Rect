using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargedProjectileWeapon : BaseProjectileWeapon {

    public float minChargeTime = 0.0f;
    public float maxChargeTime = 3.0f;

    protected override void OnProjectileSpawn(WeaponAttackParams parameters, Projectile projectile)
    {
        projectile.ApplySpeedMultiplier(GetSpeedMultiplier(parameters.chargeTime));
    }

    private float GetSpeedMultiplier(float chargeTime)
    {
        chargeTime = Mathf.Min(maxChargeTime, chargeTime);
        float speedMultiplier = (chargeTime - minChargeTime) / (maxChargeTime - minChargeTime);

        Debug.Log("speed multiplier " + speedMultiplier);
        Debug.Log("charge time " + chargeTime);

        return speedMultiplier;
    }

    public override WeaponType GetWeaponType()
    {
        return WeaponType.CHARGED;
    }
}
