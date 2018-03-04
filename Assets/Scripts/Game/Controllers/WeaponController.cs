using System.Collections;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject origWeapon;
    private WeaponHand left;
    private WeaponHand right;

    private WeaponHand currentWeaponHand;
    private Coroutine currentIdleCoroutine;

    private float currentChargeTime;

    void Start()
    {
        left = new WeaponHand(transform.Find("leftHandWeapon"), true);
        right = new WeaponHand(transform.Find("rightHandWeapon"), false);
        EquipWeapon(origWeapon);
    }

    void Update()
    {
        AimWeapon();
        WeaponType weaponType = currentWeaponHand.GetWeapon().GetWeaponType();
        switch (weaponType)
        {
            case WeaponType.CONTINUOUS:
                CheckContinuousUserInput();
                break;
            case WeaponType.CHARGED:
                CheckChargedUserInput();
                break;
            default:
                CheckUserInput();
                break;
        }
    }

    private void CheckUserInput()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            WeaponAttackParams parameters = new WeaponAttackParams();
            DoAttack(parameters);
        }
    }

    private void CheckChargedUserInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            currentChargeTime = Time.time;
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            float chargeTime = Time.time - currentChargeTime;
            WeaponAttackParams parameters = new WeaponAttackParams();
            parameters.chargeTime = chargeTime;
            DoAttack(parameters);
        }
    }

    private void CheckContinuousUserInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            WeaponAttackParams parameters = new WeaponAttackParams();
            DoAttack(parameters);
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            ((FlamethrowerWeapon) currentWeaponHand.GetWeapon()).StopAttack();
        }
    }

    private void DoAttack(WeaponAttackParams parameters)
    {
        Vector3 mousePosition = GetMousePosition();
        MoveMeleeWeapon(mousePosition);

        Vector3 direction = mousePosition - transform.position;
        parameters.parent = gameObject;
        parameters.direction = direction;

        currentWeaponHand.GetWeapon().DoAttack(parameters);
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
        if (!currentWeaponHand.IsMelee() || !currentWeaponHand.GetWeapon().CanAttack())
        {
            return;
        }

        float attackCooldown = currentWeaponHand.GetWeapon().attackCooldown;
        MovementUtils.Instance.RotateTowardsAndBack(currentWeaponHand.GetHand().transform, mousePosition, attackCooldown * 1 / 3, attackCooldown * 2 / 3);
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
