using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySimpleBehaviour : CharacterBehaviourBase
{

    public GameObject target;
    public bool canJump = false;

    public float turnSpeed = 1;
    public float aggroDistance = 1;
    public float patrolDistance = 5;
    public float range = 5;
    public int damage = 1;
    public float coolDown = 1;

    
    private Vector2 _startingPos;
    private AudioSource _amps_sound;
    private float _lastAttackTime;
    private float _lastHitTime;
    private float _attackAniDuration;

    // Use this for initialization
    public override void Awake()
    {
        base.Awake();
        target = GameObject.FindGameObjectWithTag("Player");
        _startingPos = transform.position;
        _amps_sound = GetComponent<AudioSource>();
        _lastAttackTime = 0;
        _attackAniDuration = 0;
        _lastHitTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isInKnockback)
        {
            return;
        }

        var _targetDistance = getDistanceTo(target.transform.position);

        if (range > _targetDistance && !IsAttackCooldown())
        {
            //Debug.Log("Attacking: " + target.name + " Target distance: " + _targetDistance +
            //          " range :" + range + " Last attack time: " + _lastAttackTime);
            DoAttack();
        }
        else if (_targetDistance < aggroDistance && 
                range < _targetDistance)
        {
            //Debug.Log("Moving to: " + target.name + " Target distance: " + _targetDistance +
            //          " range :" + range + " Last attack time: " + _lastAttackTime);
            MoveTowards(target);
        }
        else
        {
            UpdateAnimation(MovementType.IDLE);
            //Debug.Log("Idling: " + target.name + " Target distance: " + _targetDistance +
            //          " range :" + range +  " Last attack time: " + _lastAttackTime);
        }
    }

    private void DoAttack()
    {
        Attack(target, damage);
        //amps_sound.Play();
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
        //if (!IsGrounded() || !canJump)
        //{
        //    return false;
        //}

        //float targetHeight = GetComponent<CompositeCollider2D>().bounds.size.y;
        //float yDif = (targetPosition.y - targetHeight / 2) - (transform.position.y + this.size.y / 2);
        //GameObject targetPlatform = target.GetComponent<PlayerBehaviour>().closestPlatform;
        //return (yDif > 0.5) && (CanJumpToObject(target) || CanJumpToObject(targetPlatform));
        return false;
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

    protected override void Attack(GameObject target, int damage)
    {
        _lastAttackTime = Time.time;
        AttackType type = (RandomUtil.GetRandomFromArray(Enum.GetValues(typeof(AttackType)).Cast<AttackType>().ToArray()));
        _attackAniDuration = animationController.UpdateAttackAnimations(type);
    }

    private bool IsAttackCooldown()
    {
        return Time.time - _lastAttackTime <= _attackAniDuration+coolDown;
    }

    private bool IsHitCooldown()
    {
        return Time.time - _lastHitTime < _attackAniDuration;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject == target && !IsHitCooldown()) {
            var cbb = target.GetComponent<CharacterBehaviourBase>();
            cbb.TakeDamage(damage);
            Vector2 knockBackDir = (target.transform.position - transform.position).normalized;
            cbb.DoKnockback(knockBackDir * 20);
            _lastHitTime = Time.time;
        }
    }
}
