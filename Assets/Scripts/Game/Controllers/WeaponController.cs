using System.Collections;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject origWeapon;
    private WeaponHand left;
    private WeaponHand right;

    // Use this for initialization
    void Start()
    {
        left = new WeaponHand(transform.Find("leftHandWeapon"), true);
        right = new WeaponHand(transform.Find("rightHandWeapon"), false);
        EquipWeapon(origWeapon);
    }

    // Update is called once per frame
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
        }
        else
        {
            right.weapon.GetComponent<Weapon>().Attack(gameObject, direction);
        }
    }

    public void EquipWeapon(GameObject weapon)
    {
        StopAllCoroutines();

        EquipWeaponInHand(left, weapon);
        EquipWeaponInHand(right, weapon);
    }

    public void ResetWeapon()
    {
        EquipWeapon(origWeapon);
    }

    private void EquipWeaponInHand(WeaponHand weaponHand, GameObject weapon)
    {
        Destroy(weaponHand.weapon);
        weaponHand.weapon = Instantiate(weapon);
        MoveWeaponToPoint(weaponHand);
    }

    private void MoveWeaponToPoint(WeaponHand weaponHand)
    {
        int offset = weaponHand.flipped ? 1 : -1;
        Transform weaponHandP = weaponHand.weapon.transform.Find("handPoint");
        Vector3 equipHandPoint = weaponHand.gunDefaultPoint;
        Vector3 position = new Vector3(equipHandPoint.x + weaponHandP.localPosition.x * offset, equipHandPoint.y + weaponHandP.localPosition.y * offset);
        weaponHand.weapon.transform.position = position;
        weaponHand.weapon.transform.rotation = weaponHand.hand.Find("handPoint").rotation;
        weaponHand.weapon.transform.parent = weaponHand.hand;
    }


    private void MoveWeapon()
    {
        StopAllCoroutines();

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mousePosition.x <= transform.position.x)
        {
            RotateTowards(left.hand, mousePosition, true);
            StartCoroutine(MoveToIdlePosition(right, false));
        }
        else
        {
            RotateTowards(right.hand, mousePosition, false);
            StartCoroutine(MoveToIdlePosition(left, true));
        }
    }

    private void RotateTowards(Transform transform, Vector3 mousePosition, bool fipped)
    {
        Quaternion q = GetRotationTowards(transform, mousePosition, fipped);
        transform.rotation = GetRotationTowards(transform, mousePosition, fipped);
    }

    private Quaternion GetRotationTowards(Transform transform, Vector3 toPosition, bool flipped)
    {
        int flip = flipped ? -1 : 1;
        Vector3 vectorToTarget = new Vector3(flip * (toPosition.x - transform.position.x), flip * (toPosition.y - transform.position.y), 0);
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        return Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private IEnumerator MoveToIdlePosition(WeaponHand weaponHand, bool fipped)
    {
        Vector3 idlePosition = weaponHand.GetGunDefaultPointGlobal();
        while (Vector3.Distance(weaponHand.hand.position, idlePosition) > 0)
        {
            Quaternion q = GetRotationTowards(weaponHand.hand, idlePosition, fipped);
            weaponHand.hand.rotation = Quaternion.Slerp(weaponHand.hand.rotation, q, Time.deltaTime * 10f);
            yield return null;
        }
    }


    private class WeaponHand
    {
        public GameObject weapon;
        public Transform hand;
        public Vector3 gunDefaultPoint;
        public bool flipped;

        public WeaponHand(Transform hand, bool flipped)
        {
            this.hand = hand;
            this.gunDefaultPoint = hand.Find("handPoint").localPosition;
            gunDefaultPoint.z = 0;
            this.flipped = flipped;
        }

        public Vector3 GetGunDefaultPointGlobal()
        {
            return hand.position + gunDefaultPoint;
        }
    }
}
