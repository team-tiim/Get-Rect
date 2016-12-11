using Assets.Scripts;
using Assets.Scripts.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehaviourBase : MonoBehaviour {

    public float jumpPower;
    public float speed = 1;  //Floating point variable to store the player's movement speed.
    public int hp = 10;

    protected Rigidbody2D rb2d;       //Store a reference to the Rigidbody2D component required to use 2D Physics.
    protected Vector3 size;
    protected Animator animator;
    protected Weapon selectedWeapon;
    protected AudioSource jumpsound;

    // Use this for initialization
    protected void Start () {
        this.animator = GetComponent<Animator>();
        this.rb2d = GetComponent<Rigidbody2D>();
        this.size = GetComponent<SpriteRenderer>().sprite.bounds.size;
        GetComponent<BoxCollider2D>().size = size;
    }
	
	// Update is called once per frame
	void Update () {		
	}

    protected bool IsGrounded()
    {
        return this.rb2d.velocity.y == 0;
    }

    protected void updateAnimation(float moveHorizontal)
    {
        if (moveHorizontal > 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            this.animator.SetBool("isMove", true);
        }
        else if (moveHorizontal < 0)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            this.animator.SetBool("isMove", true);
        }
        else
        {
            this.animator.SetBool("isMove", false);
        }
    }

    protected void Attack(Vector3 direction)
    {
        selectedWeapon.Attack(gameObject, direction);
    }

    protected void Attack(GameObject target, int damage)
    {
        this.animator.SetTrigger("doAttack");
        target.GetComponent<CharacterBehaviourBase>().takeDamage(damage);
    }

    public void takeDamage(int damage)
    {
        this.hp -= damage;
        if (this.hp <= 0)
        {
            if (this.gameObject.tag == "Player")
            {
                GetComponent<Animator>().Play("player_death");
            }
            else
            {
                GameObject.Destroy(this.gameObject);
                GameObject controller = GameObject.Find("GameControllers");
                controller.GetComponent<GameController>().doExplosion(this.transform.position);
            }
        }
    }

    public virtual void selectWeapon(Weapon weapon)
    {
        this.selectedWeapon = weapon;
    }
}
