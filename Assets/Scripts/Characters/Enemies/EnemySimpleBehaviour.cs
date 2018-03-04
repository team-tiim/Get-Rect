using Assets.Scripts.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySimpleBehaviour : CharacterBehaviourBase
{

    public GameObject target;
    public Rigidbody2D targetRb2d;
    public bool canJump = false;

    public float acceleration = 15;
    public float aggroDistance = 20;
    public float patrolDistance = 5;
    public float patrolTime = 1;
    public float range = 5;
    public int damage = 1;
    public float coolDown = 1;
    public EnemyState state = EnemyState.Patrolling;

 
    private LayerMask _platformMask;
    private LayerMask _buddyMask;
    private Vector2 _startingPos;
    private AudioSource _amps_sound;
    private float _lastAttackTime;
    private float _lastHitTime;
    private float _attackAniDuration;
    private float targetDistance;
    private Vector2 patrolPointMin;
    private Vector2 patrolPointMax;
    private float _lastPatrolTime;
    private bool waiting;

    // Use this for initialization
    public override void Awake()
    {
        base.Awake();
        target = GameObject.FindGameObjectWithTag("Player");
        _platformMask = SetMask();
        targetRb2d = target.GetComponent<Rigidbody2D>();
        _startingPos = transform.position;
        _amps_sound = GetComponent<AudioSource>();
        _lastAttackTime = 0;
        _attackAniDuration = 0;
        _lastHitTime = 0;
        _lastPatrolTime = 0;
        waiting = false;
        var movement = new Vector2(-1 * acceleration * rb2d.mass, 0);
        rb2d.AddForce(movement, ForceMode2D.Force);
        patrolPointMin = new Vector2(this.transform.position.x - patrolDistance, this.transform.position.y);
        patrolPointMax = new Vector2(this.transform.position.x + patrolDistance, this.transform.position.y);
        _buddyMask = LayerMask.GetMask("Enemy");
    }

    LayerMask SetMask()
    {
        var mask = new LayerMask();
        var thisLayer = this.gameObject.layer;
        mask = 1 << thisLayer;
        return ~mask;
    }

    // Update is called once per frame
    void Update()
    {

        if (isInKnockback)
        {
            return;
        }
        if (state != EnemyState.KillerMode)
        {
            if(CanSeePlayer())state = EnemyState.KillerMode;
        }
    }

    void FixedUpdate ()
    {
        targetDistance = getDistanceTo(target.transform.position);
        if (isInKnockback)
        {
            return;
        }
        if (state == EnemyState.KillerMode)
        {
            //Debug.Log("Moving to: " + target.name + " Target distance: " + targetDistance +
            //          " range :" + range + " Last attack time: " + _lastAttackTime + "My x speed is:" + rb2d.velocity.x);
            KillTarget(target);
        }
        else
        {
            Patrol();
            //Debug.Log("Idling: " + target.name + " Target distance: " + _targetDistance +
            //          " range :" + range +  " Last attack time: " + _lastAttackTime);
        }

    }

    private void DoAttack()
    {
        Attack(target, damage);
        //amps_sound.Play();
    }

    private void KillTarget(GameObject target)
    {
        var direction = (target.transform.position.x - transform.position.x) < 0 ? -1 : 1;
        MovementType type = Utils.GetMovementType(direction);
        animationController.UpdateRotation(type);
        if (range > targetDistance)
        {
            //Debug.Log("Attacking: " + target.name + " Target distance: " + _targetDistance +
            //          " range :" + range + " Last attack time: " + _lastAttackTime);
            if (!IsAttackCooldown()) DoAttack();
            return;
        }
        if (IsBuddyNear())
        {
            Jump();
            return;
        }
        if (!IsGrounded())
        {
            MoveTowards(target.transform.position, speed);
            return;
        }
        if (Math.Abs(target.transform.position.x - rb2d.transform.position.x) < range && target.transform.position.y - rb2d.transform.position.y > range) Jump();
        else if (IsEdgeNear())
        {
           
            if (targetDistance > speed / 4 && target.transform.position.y - rb2d.transform.position.y > -4 ) Jump();
            else MoveTowards(target.transform.position, speed);
        }
        else MoveTowards(target.transform.position, speed);

        //TODO pathing

        //TODO jumping

        //TODO platform end

    }

    private void Jump()
    {
        if (IsGrounded())
        {
            //TODO jumping animation
            rb2d.AddForce(new Vector2(0, rb2d.mass * jumpPower), ForceMode2D.Impulse);
        }
    }


    private void MoveTowards(Vector3 targetPosition,float targetSpeed)
    {
        var direction = (targetPosition.x - transform.position.x) < 0 ? -1 : 1;
        MovementType type = Utils.GetMovementType(direction);
        animationController.UpdateRotation(type);
        Debug.DrawLine(transform.position, targetPosition, Color.yellow);
        UpdateAnimation(type);
        var movement = new Vector2(direction * acceleration * rb2d.mass, 0);
        if (Math.Abs(rb2d.velocity.x) < targetSpeed) rb2d.AddForce(movement, ForceMode2D.Force);

    }

    private void StopMoving()
    {
        Debug.Log("trying to stop");
        var currentSpeed = Math.Abs(rb2d.velocity.x);
        if (currentSpeed > (0.1))
        {
            var direction = -1*animationController.GetCurrentDirection();
            var movement = new Vector2(direction * acceleration * rb2d.mass, 0);
            rb2d.AddForce(movement, ForceMode2D.Force);
        }
        else UpdateAnimation(MovementType.IDLE);

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
        AttackType type = (RandomUtils.GetRandomFromArray(Enum.GetValues(typeof(AttackType)).Cast<AttackType>().ToArray()));
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

    public bool CanSeePlayer()
    {
        if (aggroDistance > targetDistance)
        {
            var ray = Physics2D.Raycast(rb2d.position, targetRb2d.position - rb2d.position,aggroDistance,_platformMask);
            if (ray.collider.CompareTag("Player")) return true;

        }
        return false;
    }

    public bool IsEdgeNear()
    {
        var bounds = GetCurrentPlatformBounds();
        if(bounds != null)
        {
            var direction = -animationController.GetCurrentDirection();
            var xPos = rb2d.position.x;
            var edgeDistance = bounds.extents.x - (direction == 1 ? bounds.center.x - xPos : xPos - bounds.center.x);
            var delta = speed / 6;
            
            if (edgeDistance<delta) return true;
        }

        return false;
    }

    public void Patrol()
    {
        var targetPos = animationController.GetCurrentDirection() == 1 ? patrolPointMax : patrolPointMin;
        if (!waiting)
        {
            //Debug.Log(IsEdgeNear() + " " + (Math.Abs(transform.position.x - targetPos.x) < 0.1) + " " + IsBuddyNear());
            if (IsEdgeNear() || Math.Abs(transform.position.x-targetPos.x) < 0.1 || IsBuddyNear() )
            {
                waiting = true;
                _lastPatrolTime = Time.time;
                UpdateAnimation(MovementType.IDLE);
            }
            else
            {
                MoveTowards(targetPos, speed/4);
            }
        }
        else
        {
            if(Time.time-_lastPatrolTime > patrolTime)
            {
                waiting = false;
                MovementType type = Utils.GetMovementType(-animationController.GetCurrentDirection());
                animationController.UpdateRotation(type);
            }
        }
    }

    public bool IsBuddyNear()
    {
        var capsule = rb2d.GetComponent<CapsuleCollider2D>();
        var capsulWidth = capsule.size.x / 2;
        var pos = new Vector2(capsule.transform.position.x, capsule.transform.position.y) +capsule.offset;
        pos.x += animationController.GetCurrentDirection()*(capsulWidth + 0.2f);
        var ray = Physics2D.Raycast(pos, new Vector2(animationController.GetCurrentDirection(),0), range/2, _buddyMask);
        Debug.DrawRay(pos, new Vector2(animationController.GetCurrentDirection(), 0), Color.blue);
        if (ray)
        {
            return ray.collider.CompareTag(gameObject.tag);
        }
        return false;
    }

    public bool IsEdgeNear_old()
    {
        var capsule = rb2d.GetComponent<CapsuleCollider2D>();
        var rayheight = capsule.size.y/2;
        var position = new Vector2(capsule.transform.position.x, capsule.transform.position.y) + capsule.offset;
        var length = speed/4;
        var direction = animationController.GetCurrentDirection();
        var angle = new Vector2(length * direction, -rayheight);
        var distance = (float)Math.Sqrt(length * length + rayheight * rayheight) + 1;
        var ray = Physics2D.Raycast(position, angle, distance, _platformMask);
       
        Debug.DrawRay(position, angle, Color.red);
        if (ray)return false;
        //Debug.Log(position + " " + distance);
        return true;
    }

    public Bounds GetCurrentPlatformBounds()
    {
        var bounds = new Bounds();
        var capsule = rb2d.GetComponent<CapsuleCollider2D>();
        var rayheight = capsule.size.y / 2;
        var position = new Vector2(capsule.transform.position.x, capsule.transform.position.y) + capsule.offset;

        var ray = Physics2D.Raycast(position, new Vector2(0,-1), rayheight+range/2, _platformMask);

        if (ray)
        {
            if(ray.collider.CompareTag("Platform"))return ray.collider.bounds;
        }

        return bounds;
    }

    
}
