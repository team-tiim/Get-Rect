using System.Collections;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject origWeapon;
    private WeaponHand left;
    private WeaponHand right;

    void Start()
    {
        left = new WeaponHand(transform.Find("leftHandWeapon"), true);
        right = new WeaponHand(transform.Find("rightHandWeapon"), false);
        EquipWeapon(origWeapon);
    }

    void Update()
    {
        MoveWeapon();
    }

    public void DoAttack()
    {
        Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pz.z = 0;

        Vector3 direction = pz - transform.position;
        if (direction.x <= 0)
        {
            left.weapon.GetComponent<Weapon>().Attack(gameObject, direction);
            left.ResetRecoil();
        }
        else
        {
            right.weapon.GetComponent<Weapon>().Attack(gameObject, direction);
            right.ResetRecoil();
        }

    }

    public void EquipWeapon(GameObject weaponGO)
    {
        StopAllCoroutines();

        left.ReplaceWeapon(weaponGO);
        right.ReplaceWeapon(weaponGO);
    }

    public void ResetWeapon()
    {
        EquipWeapon(origWeapon);
    }

    private void MoveWeapon()
    {
        StopAllCoroutines();

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mousePosition.x <= transform.position.x)
        {
            RotateTowards(left, mousePosition);
            StartCoroutine(MoveToIdlePosition(right));
        }
        else
        {
            RotateTowards(right, mousePosition);
            StartCoroutine(MoveToIdlePosition(left));
        }
    }

    private void RotateTowards(WeaponHand weaponHand, Vector3 mousePosition)
    {
        weaponHand.GetHand().transform.rotation = GetRotationTowards(weaponHand.GetHand(), mousePosition, weaponHand.IsFlipped());
    }

    private Quaternion GetRotationTowards(Transform transform, Vector3 toPosition, bool flipped)
    {
        int flip = flipped ? -1 : 1;
        Vector3 vectorToTarget = new Vector3(flip * (toPosition.x - transform.position.x), flip * (toPosition.y - transform.position.y), 0);
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        return Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private IEnumerator MoveToIdlePosition(WeaponHand weaponHand)
    {
        Vector3 idlePosition = weaponHand.GetDefaultHandPointGlobal();
        Transform hand = weaponHand.GetHand();
        while (Vector3.Distance(weaponHand.GetHand().position, idlePosition) > 0)
        {
            Quaternion q = GetRotationTowards(hand, idlePosition, weaponHand.IsFlipped());
            hand.rotation = Quaternion.Slerp(hand.rotation, q, Time.deltaTime * 10f);
            yield return null;
        }
    }
}
