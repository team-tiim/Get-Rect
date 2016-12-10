using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehaviourBase : MonoBehaviour {

    public float speed = 1;  //Floating point variable to store the player's movement speed.
    public int hp = 10;

    protected Animator animator;
    protected Weapon selectedWeapon;

    // Use this for initialization
    protected void Start () {
        GetComponent<BoxCollider2D>().size = GetComponent<SpriteRenderer>().sprite.bounds.size;
    }
	
	// Update is called once per frame
	void Update () {		
	}


    protected void updateAnimation(float moveHorizontal)
    {
        if (moveHorizontal > 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            animator.SetBool("isMove", true);
        }
        else if (moveHorizontal < 0)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            animator.SetBool("isMove", true);
        }
        else
        {
            animator.SetBool("isMove", false);
        }
    }

    protected void Attack()
    {
        selectedWeapon.Attack();
    }
}
