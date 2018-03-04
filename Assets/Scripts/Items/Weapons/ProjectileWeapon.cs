using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : BaseProjectileWeapon
{

    public override WeaponType GetWeaponType()
    {
        return WeaponType.SINGLESHOT;
    }
}
