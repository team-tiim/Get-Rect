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
    public float range = 5;
    public int damage = 1;
    public float coolDown = 1;


    private LayerMask _layerMask;
    private Vector2 _startingPos;
    private AudioSource _amps_sound;
    private float _lastAttackTime;
    private float _lastHitTime;
    private float _attackAniDuration;
    private float targetDistance;

    // Use this for initialization
    public override void Awake()
    {
        base.Awake();
        target = GameObject.FindGameObjectWithTag("Player");
        _layerMask = SetMask();
        targetRb2d = target.GetComponent<Rigidbody2D>();
        _startingPos = transform.position;
        _amps_sound = GetComponent<AudioSource>();
        _lastAttackTime = 0;
        _attackAniDuration = 0;
        _lastHitTime = 0;
        var movement = new Vector2(-1 * acceleration * rb2d.mass, 0);
        rb2d.AddForce(movement, ForceMode2D.Force);
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
        if (range > targetDistance && !IsAttackCooldown())
        {
            //Debug.Log("Attacking: " + target.name + " Target distance: " + _targetDistance +
            //          " range :" + range + " Last attack time: " + _lastAttackTime);
            DoAttack();
        }
    }

    void FixedUpdate ()
    {
        targetDistance = getDistanceTo(target.transform.position);

        if (isInKnockback)
        {
            return;
        }
        if (CanSeePlayer() &&
                range < targetDistance)
        {
            //Debug.Log("Moving to: " + target.name + " Target distance: " + targetDistance +
            //          " range :" + range + " Last attack time: " + _lastAttackTime + "My x speed is:" + rb2d.velocity.x);
            TryMove(target);
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

    private void TryMove(GameObject target)
    {
        if (IsGrounded()) MoveTowards(target);

        //TODO pathing

        //TODO jumping

        //TODO platform end

    }


    private void MoveTowards(GameObject target)
    {
        var targetPosition = target.transform.position;
        var direction = (targetPosition.x - transform.position.x) < 0 ? -1 : 1;
        MovementType type = Utils.GetMovementType(direction);
        animationController.UpdateRotation(type);
        if (!IsEdgeNear())
        {
            Debug.DrawLine(transform.position, targetPosition, Color.yellow);
            UpdateAnimation(type);
            var movement = new Vector2(direction * acceleration * rb2d.mass, 0);
            if (Math.Abs(rb2d.velocity.x) < speed) rb2d.AddForce(movement, ForceMode2D.Force);
        }
        else
        {
            Debug.Log("Jumping");
            rb2d.AddForce(new Vector2(0, rb2d.mass * jumpPower), ForceMode2D.Impulse);
            //StopMoving();
        }

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
        if(currentSpeed == 0) UpdateAnimation(MovementType.IDLE);

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
            var ray = Physics2D.Raycast(rb2d.position, targetRb2d.position - rb2d.position,aggroDistance,_layerMask);
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
            var delta = speed / 4;
            
            if (edgeDistance<delta) return true;
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
        var ray = Physics2D.Raycast(position, angle, distance, _layerMask);
       
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

        var ray = Physics2D.Raycast(position, new Vector2(0,-1), rayheight+0.1f, _layerMask);

        if (ray)
        {
            if(ray.collider.CompareTag("Platform"))return ray.collider.bounds;
        }

        return bounds;
    }

    
}
