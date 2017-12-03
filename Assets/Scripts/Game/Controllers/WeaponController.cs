using System.Collections;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject origWeapon;
    private WeaponHand left;
    private WeaponHand right;

    private WeaponHand currentWeaponHand;
    private Coroutine currentIdleCoroutine;

    void Start()
    {
        left = new WeaponHand(transform.Find("leftHandWeapon"), true);
        right = new WeaponHand(transform.Find("rightHandWeapon"), false);
        EquipWeapon(origWeapon);
    }

    void Update()
    {
        AimWeapon();
    }

    public void DoAttack()
    {
        Vector3 mousePosition = GetMousePosition();
        MoveMeleeWeapon(mousePosition);

        Vector3 direction = mousePosition - transform.position;
        WeaponHand hand = GetUsedWeaponHand(mousePosition);
        hand.GetWeapon().Attack(gameObject, direction);
    }

    public void EquipWeapon(GameObject weaponGO)
    {
        if (currentIdleCoroutine != null)
        {
            StopCoroutine(currentIdleCoroutine);
        }

        left.ReplaceWeapon(weaponGO);
        right.ReplaceWeapon(weaponGO);
    }

    public void ResetWeapon()
    {
        EquipWeapon(origWeapon);
    }

    private void AimWeapon()
    {
        Vector3 mousePosition = GetMousePosition();
        WeaponHand weaponHand = GetUsedWeaponHand(mousePosition);
        if (weaponHand.IsMelee())
        {
            return;
        }

        if (weaponHand != currentWeaponHand)
        {
            if (currentIdleCoroutine != null)
            {
                StopCoroutine(currentIdleCoroutine);
            }
            currentIdleCoroutine = MoveToIdlePosition(GetIdleWeaponHand(mousePosition));
        }

        MovementUtils.Instance.RotateTowards(weaponHand.GetHand(), mousePosition);
        currentWeaponHand = weaponHand;
    }

    private void MoveMeleeWeapon(Vector3 mousePosition)
    {
        WeaponHand hand = GetUsedWeaponHand(mousePosition);
        if (!hand.IsMelee() || !hand.GetWeapon().CanAttack())
        {
            return;
        }

        float attackCooldown = hand.GetWeapon().attackCooldown;
        MovementUtils.Instance.RotateTowardsAndBack(hand.GetHand().transform, mousePosition, attackCooldown * 1 / 3, attackCooldown * 2 / 3);
    }

    private Coroutine MoveToIdlePosition(WeaponHand weaponHand)
    {
        Vector3 idlePosition = weaponHand.GetDefaultHandPointGlobal();
        return MovementUtils.Instance.RotateTowards(weaponHand.GetHand(), weaponHand.GetDefaultHandPointGlobal(), 1f);
    }

    private WeaponHand GetUsedWeaponHand(Vector3 mousePosition)
    {
        bool isUsingLeft = mousePosition.x <= transform.position.x;
        return isUsingLeft ? left : right;
    }

    private WeaponHand GetIdleWeaponHand(Vector3 mousePosition)
    {
        bool isUsingLeft = mousePosition.x <= transform.position.x;
        return !isUsingLeft ? left : right;
    }

    private Vector3 GetMousePosition()
    {
        Vector3 res = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        res.z = 0;
        return res;
    }
}
