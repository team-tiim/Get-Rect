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
	new void Start () {
        base.Start();
        animator = GetComponent<Animator>();
        this.target = GameObject.FindGameObjectWithTag("Player");
        this.startingPos = transform.position;
        amps_sound = GetComponent<AudioSource>();
        //Debug.Log("Target found: "+ target.name);
    }

    // Update is called once per frame
    void Update()
    {
        float targetDistance = getDistanceTo(target.transform.position);
        if (targetDistance < range)
        {
            float timeFromLastAttack = Time.time - attackTime;
            if (timeFromLastAttack > coolDown)
            {
                Debug.Log("attacked player");
                this.attackTime = Time.time;
                Attack(target, damage);
                amps_sound.Play();
            }
        }
        else if (targetDistance < aggroDistance)
        {
            //Debug.Log("Target found: " + target.name);
            moveTowards(target);
        }
    }

    private void moveTowards(GameObject target)
    {
        Vector3 targetPosition = target.transform.position;
        Debug.DrawLine(transform.position, targetPosition, Color.yellow);
        float xDif = targetPosition.x - transform.position.x;
        if (shouldJump(targetPosition))
        {
            Debug.Log("jump");
            this.animator.SetTrigger("doJump");
            this.rb2d.AddForce(new Vector2(0, rb2d.mass * jumpPower), ForceMode2D.Impulse);
        }

        updateAnimation(xDif);
        transform.position += transform.right * speed * Time.deltaTime;
    }

    private float getDistanceTo(Vector3 position)
    {
        return Vector2.Distance(transform.position, position);
    }

    private bool shouldJump(Vector3 targetPosition)
    {
        if (!IsGrounded() || !canJump)
        {
            return false;
        }

        float targetHeight = GetComponent<SpriteRenderer>().sprite.bounds.size.y;
        float yDif = (targetPosition.y - targetHeight / 2) - (transform.position.y + this.size.y / 2);
        GameObject targetPlatform = target.GetComponent<PlayerController>().closestPlatform;
        return (yDif > 0.5) && (canJumpToObject(target) || canJumpToObject(targetPlatform));
    }


    private bool canJumpToObject(GameObject obj)
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
}
