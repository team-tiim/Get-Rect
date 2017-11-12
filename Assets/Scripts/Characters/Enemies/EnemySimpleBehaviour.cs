using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySimpleBehaviour : CharacterBehaviourBase
{

    public GameObject target;
    public bool canJump = false;

    public int turnSpeed = 1;
    public int aggroDistance = 1;
    public int patrolDistance = 5;
    public int range = 1;
    public int damage = 1;
    public int coolDown = 5;

    private Vector2 startingPos;
    private float attackTime = 0;
    private AudioSource amps_sound;

    // Use this for initialization
    public override void Awake () {
        base.Awake();
        this.target = GameObject.FindGameObjectWithTag("Player");
        this.startingPos = transform.position;
        amps_sound = GetComponent<AudioSource>();
        //Debug.Log("Target found: "+ target.name);
    }

    // Update is called once per frame
    void Update()
    {
        if (isInKnockback)
        {
            return;
        }
        float targetDistance = getDistanceTo(target.transform.position);
        if (targetDistance < range)
        {
            DoAttack();
        }
        else if (targetDistance < aggroDistance)
        {
            Debug.Log("Target found: " + target.name);
            MoveTowards(target);
        }
    }

    private void DoAttack()
    {
        float timeFromLastAttack = Time.time - attackTime;
        if(timeFromLastAttack <= coolDown) {
            return;
        }
        //Debug.Log("attacked player");
        this.attackTime = Time.time;
        Attack(target, damage);
       // amps_sound.Play();
    }


    private void MoveTowards(GameObject target)
    {
        Vector3 targetPosition = target.transform.position;
        Debug.DrawLine(transform.position, targetPosition, Color.yellow);
        float xDif = targetPosition.x - transform.position.x;
        if (ShouldJump(targetPosition))
        {
            //Debug.Log("jump");
            this.animationController.animator.SetTrigger("doJump");
            this.rb2d.AddForce(new Vector2(0, rb2d.mass * jumpPower), ForceMode2D.Impulse);
        }

        MovementType type = Utils.GetMovementType(xDif);
        UpdateAnimation(type);
        Vector2 movement = new Vector2(xDif, rb2d.velocity.y);
        rb2d.velocity = movement;
    }

    private float getDistanceTo(Vector3 position)
    {
        return Vector2.Distance(transform.position, position);
    }

    private bool ShouldJump(Vector3 targetPosition)
    {
        if (!IsGrounded() || !canJump)
        {
            return false;
        }

        float targetHeight = GetComponent<BoxCollider2D>().bounds.size.y;
        float yDif = (targetPosition.y - targetHeight / 2) - (transform.position.y + this.size.y / 2);
        GameObject targetPlatform = target.GetComponent<PlayerBehaviour>().closestPlatform;
        return (yDif > 0.5) && (CanJumpToObject(target) || CanJumpToObject(targetPlatform));
    }

    private bool CanJumpToObject(GameObject obj)
    {
        if (obj == null)
        {
            return false;
        }
        Transform t = obj.transform;
        bool yReachable = Math.Abs(t.position.y - transform.position.y) <= jumpPower;
        bool xReachable = Math.Abs(t.position.x - transform.position.x) <= jumpPower * 0.33;
        return yReachable && xReachable;
    }

    protected override void OnDamage(int damage)
    {
        base.OnDamage(damage);
        //StartCoroutine(Utils.ChangeColor(this.spriteRenderer, this.origColor));
    }
}
