using UnityEngine;
using UnityEngine.UI;
using System;
using Anima2D;

public class PlayerBehaviour : CharacterBehaviourBase
{
    public AudioSource[] sounds;
    public AudioSource bgm;
    public Slider healthslider;

    public GameObject closestPlatform;
    public Boolean isInKnocback;
    public Vector3 knockbackDirection;

    private GameObject leftWeapon;
    private GameObject rightWeapon;

    private Transform leftHandPoint;
    private Transform rightHandPoint;
    private Transform handMovementPath;


    //Called before Start, use as constructor
    public override void Awake()
    {
        base.Awake();
        leftHandPoint = transform.Find("leftHandPoint");
        rightHandPoint = transform.Find("rightHandPoint");
        handMovementPath = transform.Find("handMovementPath");
    }

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        this.armor = new Armor();

        EquipWeapon(origWeapon);
    }

    void Update()
    {
        MoveWeapon();
        animationController.animator.SetBool("isGrounded", IsGrounded());

        //healthslider.value = hp;
        //UpdateAnimation(moveHorizontal);
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {

    }

    protected override void OnDeath()
    {
        GetComponent<Animator>().Play("player_death");
    }

    protected override void OnDamage(int damage)
    {
        base.OnDamage(damage);
        //StartCoroutine(Utils.ChangeColor(this.spriteRenderer, this.origColor));
    }

    public void DoJump()
    {
        animationController.animator.SetTrigger("doJump");

        if (jumpsound != null && !jumpsound.isPlaying)
        {
            jumpsound.Play();
        }
    }

    public void DoAttack()
    {
        Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pz.z = 0;

        Vector3 direction = pz - transform.position;
        if (direction.x <= 0)
        {
            leftWeapon.GetComponent<Weapon>().Attack(gameObject, direction);
        }
        else
        {
            rightWeapon.GetComponent<Weapon>().Attack(gameObject, direction);
        }
    }

    private void MoveWeapon()
    {
        CircleCollider2D col = handMovementPath.GetComponent<CircleCollider2D>();
        Vector3 center = col.transform.position;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 centerToMouse = mousePos - center;
        centerToMouse.z = 0;

        //Vector3 closestPoint = col.bounds.ClosestPoint(shoulderToMouseDir);
        float distance = centerToMouse.magnitude;
        Vector3 direction = centerToMouse / distance;
        Vector3 pointOnCircle = center + direction * col.radius;

        Debug.DrawLine(center, pointOnCircle, Color.blue, 2);
        Debug.DrawLine(pointOnCircle, pointOnCircle + direction, Color.red, 2);

        if (centerToMouse.x <= 0)
        {
            MoveWeaponToPoint(leftWeapon, pointOnCircle, leftHandPoint.rotation, true);
            leftWeapon.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 180 - GetAngleTowards(centerToMouse)));
            //TODO: teise relva aeglaselt tagasi liigutamine
            MoveWeaponToPoint(rightWeapon, rightHandPoint.position, rightHandPoint.rotation, false);
        }
        else
        {
            MoveWeaponToPoint(rightWeapon, pointOnCircle, rightHandPoint.rotation, false);
            rightWeapon.transform.rotation = Quaternion.Euler(new Vector3(0, 0, GetAngleTowards(centerToMouse)));
            //TODO: teise relva aeglaselt tagasi liigutamine
            MoveWeaponToPoint(leftWeapon, leftHandPoint.position, leftHandPoint.rotation, true);
        }
    }

    public void EquipWeapon(GameObject weapon)
    {
        Destroy(leftWeapon);
        Destroy(rightWeapon);
        leftWeapon = Instantiate(weapon);
        leftWeapon.transform.parent = transform;
        MoveWeaponToPoint(leftWeapon, leftHandPoint.position, leftHandPoint.rotation, true);
        rightWeapon = Instantiate(weapon);
        rightWeapon.transform.parent = transform;
        MoveWeaponToPoint(rightWeapon, rightHandPoint.position, rightHandPoint.rotation, false);
    }

    public void EquipArmor(ArmorHolder armorHolder)
    {
        AddArmor(armorHolder);
        ((AnimationController)animationController).ChangeAnimation(armorHolder);
    }

    public void EquipHat(ArmorHolder armorHolder)
    {
        AddArmor(armorHolder);
        ((AnimationController)animationController).ChangeAnimation(armorHolder);
    }

    private void AddArmor(ArmorHolder armorHolder)
    {
        if (armor == null)
        {
            armor = new Armor(armorHolder.armorValue);
        }
        else
        {
            armor.Increase(armorHolder.armorValue);
        }
    }

    public override void UpdateAnimation(MovementType movementType)
    {
        if (animationController == null)
        {
            return;
        }
        animationController.UpdateMoveAnimations(movementType);
    }

    public void ResetWeapon()
    {
        EquipWeapon(origWeapon);
    }

    private void MoveWeaponToPoint(GameObject weapon, Vector3 point, Quaternion rotation, bool flip)
    {
        int offset = flip ? 1 : -1;
        Transform weaponHandP = weapon.transform.Find("handPoint");
        Vector3 position = new Vector3(point.x + weaponHandP.localPosition.x * offset, point.y - weaponHandP.localPosition.y);
        weapon.transform.position = position;
        weapon.transform.rotation = rotation;
    }

    private float GetAngleTowards(Vector3 point)
    {
        return Mathf.Atan2(point.y, point.x) * Mathf.Rad2Deg;
    }
}
