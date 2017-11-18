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
    public WeaponController weaponController;

    //Called before Start, use as constructor
    public override void Awake()
    {
        base.Awake();
        weaponController = GetComponent<WeaponController>();
    }

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        this.armor = new Armor(10);
    }

    void Update()
    {
        animationController.animator.SetBool("isGrounded", IsGrounded());

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
        weaponController.DoAttack();
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

}
