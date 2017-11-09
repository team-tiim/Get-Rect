using System.Collections.Generic;
using UnityEngine;

public class CharacterBehaviourBase : MonoBehaviour {

    public float jumpPower;
    public float speed = 1;  //Floating point variable to store the player's movement speed.
    public int hp = 10;
    protected Vector3 size;

    public Transform animationsComponent;
    protected Rigidbody2D rb2d;       //Store a reference to the Rigidbody2D component required to use 2D Physics.
    protected SpriteRenderer spriteRenderer;
    protected Animator animator;
    
    protected AudioSource jumpsound;
    // add health bar

    protected GameObject equippedWeapon;
    protected Color origColor;
    public GameObject origWeapon;
    public Armor armor;

    public bool isInKnockback;

    public virtual void Awake()
    {
        animationsComponent = transform.Find("animations");
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        //TODO old stuff, rework
        if (spriteRenderer != null)
        {
            size = spriteRenderer.sprite.bounds.size;
            origColor = spriteRenderer.color;
            GetComponent<BoxCollider2D>().size = size;
        }
    }

    public virtual void Start()
    {

    }


    // Update is called once per frame
    void Update () {		
	}

    protected bool IsGrounded()
    {
        return rb2d.velocity.y == 0;
    }

    public void UpdateAnimation(float moveHorizontal)
    {
        animator.SetBool("isMove", moveHorizontal != 0);
        if(animationsComponent == null)
        {
            return;
        }
        if (moveHorizontal > 0)
        {
            animationsComponent.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            animationsComponent.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    protected void Attack(Vector3 direction)
    {
        equippedWeapon.GetComponent<Weapon>().Attack(gameObject, direction);
    }

    protected void Attack(GameObject target, int damage)
    {
        animator.SetTrigger("doAttack");
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
            animator.SetLayerWeight(0, 0.0f);
            animator.SetLayerWeight(1, 1.0f);
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
        if(armor != null)
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
