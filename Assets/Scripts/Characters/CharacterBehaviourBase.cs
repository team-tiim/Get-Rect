using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBehaviourBase : MonoBehaviour
{

    public float jumpPower;
    public float speed = 1;  //Floating point variable to store the player's movement speed.
    public int hp = 10;
    protected Vector3 size;

    public Transform animationsComponent;
    protected Rigidbody2D rb2d;       //Store a reference to the Rigidbody2D component required to use 2D Physics.

    protected AudioSource jumpsound;
    // add health bar

    protected GameObject equippedWeapon;
    protected Color origColor;
    public GameObject origWeapon;
    public Armor armor;

    public bool isInKnockback;

    protected BasicAnimationController animationController;

    public virtual void Awake()
    {
        animationsComponent = transform.Find("animations");
        rb2d = GetComponent<Rigidbody2D>();
        animationController = animationsComponent.GetComponent<BasicAnimationController>();
    }

    public virtual void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
    }

    protected bool IsGrounded()
    {
        return rb2d.velocity.y == 0;
    }

    public virtual void UpdateAnimation(MovementType movementType)
    {
        animationController.UpdateMoveAnimations(movementType);
    }

    protected void Attack(Vector3 direction)
    {
        equippedWeapon.GetComponent<Weapon>().Attack(gameObject, direction);
    }

    protected void Attack(GameObject target, int damage)
    {
        animationController.animator.SetTrigger("doAttack");
        CharacterBehaviourBase cbb = target.GetComponent<CharacterBehaviourBase>();
        cbb.TakeDamage(damage);
        Vector2 knockBackDir = (target.transform.position - transform.position).normalized;
        //Debug.Log(knockBackDir);
        cbb.DoKnockback(knockBackDir * 20);
    }

    public void TakeDamage(int damage)
    {
        OnDamage(damage);
        if (hp <= 40)
        {
            //TODO old stuff, rework
            animationController.animator.SetLayerWeight(0, 0.0f);
            animationController.animator.SetLayerWeight(1, 1.0f);
        }
        if (hp <= 0)
        {
            OnDeath();
        }
    }

    protected virtual void OnDeath()
    {
        GameObject.Destroy(this.gameObject);
        GameObject controller = GameObject.Find("GameControllers");
        controller.GetComponent<GameController>().doExplosion(this.transform.position);
    }

    protected virtual void OnDamage(int damage)
    {
        if (armor != null)
        {
            damage = armor.BlockDamage(damage);
        }
        hp -= damage;
    }

    public virtual void DoKnockback(Vector3 direction)
    {
        isInKnockback = true;
        rb2d.AddForce(direction, ForceMode2D.Impulse);
        Invoke("SetKnockbackFalse", 1);
    }

    private void SetKnockbackFalse()
    {
        isInKnockback = false;
    }
}
