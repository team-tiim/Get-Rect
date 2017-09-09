using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehaviourBase : MonoBehaviour {

    public float jumpPower;
    public float speed = 1;  //Floating point variable to store the player's movement speed.
    public int hp = 10;

    protected Rigidbody2D rb2d;       //Store a reference to the Rigidbody2D component required to use 2D Physics.
    protected SpriteRenderer spriteRenderer;
    protected Vector3 size;
    protected Animator animator;
    protected GameObject selectedWeapon;
    protected AudioSource jumpsound;
    protected bool flipped;

    protected Color origColor;
    protected GameObject origWeapon;

    protected bool isInKnockback;

    // Use this for initialization
    protected void Start () {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.animator = GetComponent<Animator>();
        this.rb2d = GetComponent<Rigidbody2D>();
        this.size = GetComponent<SpriteRenderer>().sprite.bounds.size;
        this.flipped = false;
        this.origColor = spriteRenderer.color;
        GetComponent<BoxCollider2D>().size = size;
    }
	
	// Update is called once per frame
	void Update () {		
	}

    protected bool IsGrounded()
    {
        return this.rb2d.velocity.y == 0;
    }

    protected void UpdateAnimation(float moveHorizontal)
    {
        if (moveHorizontal > 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            this.flipped = false;
            this.animator.SetBool("isMove", true);
        }
        else if (moveHorizontal < 0)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            this.flipped = true;
            this.animator.SetBool("isMove", true);
        }
        else
        {
            this.animator.SetBool("isMove", false);
        }
    }

    protected void Attack(Vector3 direction)
    {
        Debug.Log(this.selectedWeapon.GetComponent<Weapon>());
        this.selectedWeapon.GetComponent<Weapon>().Attack(gameObject, direction);
    }

    protected void Attack(GameObject target, int damage)
    {
        this.animator.SetTrigger("doAttack");

        CharacterBehaviourBase cbb = target.GetComponent<CharacterBehaviourBase>();
        cbb.TakeDamage(damage);
        Vector2 knockBackDir = (target.transform.position - transform.position).normalized;
        //Debug.Log(knockBackDir);
        cbb.DoKnockback(knockBackDir * 20);
    }

    public void TakeDamage(int damage)
    {
        OnDamage(damage);
        if (this.hp <= 0)
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
        this.hp -= damage;
    }

    public virtual void SelectWeapon(GameObject weapon)
    {
        this.selectedWeapon = weapon;
    }

    public void ResetWeapon()
    {
        SelectWeapon(origWeapon);
    }

    public virtual void DoKnockback(Vector3 direction)
    {
        isInKnockback = true;
        rb2d.AddForce(direction, ForceMode2D.Impulse);
        Invoke("setKnockbackFalse", 1);
    }

    private void setKnockbackFalse()
    {
        isInKnockback = false;
    }
}
