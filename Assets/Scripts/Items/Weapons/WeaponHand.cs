using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHand {

    public GameObject weapon;
    private Transform hand;
    private Vector3 defaultHandPoint;
    private bool flipped;

    public WeaponHand(Transform hand, bool flipped)
    {
        this.hand = hand;
        this.defaultHandPoint = hand.Find("handPoint").localPosition;
        defaultHandPoint.z = 0;
        this.flipped = flipped;
    }

    public Transform GetHand()
    {
        return hand;
    }

    public Vector3 GetDefaultHandPointGlobal()
    {
        return hand.position + defaultHandPoint;
    }

    public Transform GetWeaponHandle()
    {
        return weapon.transform.Find("handPoint");
    }

    public Quaternion GetHandPointRotation()
    {
        return hand.Find("handPoint").rotation;
    }

    public bool IsFlipped()
    {
        return flipped;
    }

    public void ResetRecoil()
    {
        ProjectileWeapon pw = weapon.GetComponent<ProjectileWeapon>();
        if (pw == null)
        {
            return;
        }
        pw.ResetRecoil(GetHandPointRotation());
    }

    public void ReplaceWeapon(GameObject weaponGO)
    {
        GameObject.Destroy(weapon);
        weapon = GameObject.Instantiate(weaponGO);
        MoveWeaponToPoint();
    }

    private void MoveWeaponToPoint()
    {
        int offset = IsFlipped() ? 1 : -1;
        Transform weaponHandle = GetWeaponHandle();
        weapon.transform.rotation = GetHandPointRotation();
        weapon.transform.parent = hand;
        Vector3 position = new Vector3(defaultHandPoint.x + weaponHandle.localPosition.x * offset, defaultHandPoint.y - weaponHandle.localPosition.y);
        weapon.transform.localPosition = position;
    }
}
