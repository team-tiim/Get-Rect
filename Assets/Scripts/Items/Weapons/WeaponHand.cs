using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHand
{
    private static float MELEE_ARC_ANGLE = -120;

    public GameObject weapon;
    private Transform hand;
    private Transform defaultHandPoint;
    private Transform defaultHandPointMelee;
    private Quaternion defaultRotation;
    private Quaternion defaultHandPointRotation;
    private bool isFlipped;
    private bool isMelee;

    public WeaponHand(Transform hand, bool isFlipped)
    {
        this.hand = hand;
        defaultRotation = hand.rotation;
        defaultHandPoint = hand.Find("handPoint");
        defaultHandPointMelee = hand.Find("meleeHandPoint");
        this.isFlipped = isFlipped;
    }

    public Transform GetHand()
    {
        return hand;
    }

    public Vector3 GetDefaultHandPointGlobal()
    {
        int flip = isFlipped ? -1 : 1;
        return hand.position + GetDefaultHandPoint().localPosition * flip;
    }

    public Weapon GetWeapon()
    {
        return weapon.GetComponent<Weapon>();
    }

    public Transform GetWeaponHandle()
    {
        return weapon.GetComponent<Weapon>().handlePoint;
    }

    public Quaternion GetDefaultHandPointRotation()
    {
        return GetDefaultHandPoint().rotation;
    }

    public bool IsFlipped()
    {
        return isFlipped;
    }

    public bool IsMelee()
    {
        return weapon.GetComponent<Weapon>().GetWeaponType() == WeaponType.MELEE;
    }

    public void ResetWeaponRotation()
    {
       GetDefaultHandPoint().rotation = defaultHandPointRotation;
       weapon.GetComponent<Weapon>().ResetRotation(defaultHandPointRotation);
    }

    public void ReplaceWeapon(GameObject weaponGO)
    {
        isMelee = weaponGO.GetComponent<MeleeWeapon>() != null;

        hand.rotation = defaultRotation;
        GameObject.Destroy(weapon);
        weapon = GameObject.Instantiate(weaponGO);
        defaultHandPointRotation = GetDefaultHandPoint().rotation;
        MoveWeaponToDefaultPoint();
    }

    private void MoveWeaponToDefaultPoint()
    {
        Transform weaponHandle = GetWeaponHandle();
        Transform defaultHandpoint = GetDefaultHandPoint();

        defaultHandpoint.rotation = Quaternion.identity;
        weapon.transform.rotation = Quaternion.identity;
        weapon.transform.parent = defaultHandpoint;

        Vector3 position = new Vector3(-weaponHandle.localPosition.x, -weaponHandle.localPosition.y);
        weapon.transform.localPosition = position;
        defaultHandpoint.rotation = defaultHandPointRotation;
    }

    private Transform GetDefaultHandPoint()
    {
        return isMelee ? defaultHandPointMelee : defaultHandPoint;
    }

}
