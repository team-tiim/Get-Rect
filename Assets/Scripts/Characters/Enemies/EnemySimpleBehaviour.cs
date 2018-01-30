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
        Debug.Log(CanSeePlayer());
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
        if (targetDistance < aggroDistance &&
                range < targetDistance)
        {
            //Debug.Log("Moving to: " + target.name + " Target distance: " + targetDistance +
            //          " range :" + range + " Last attack time: " + _lastAttackTime + "My x speed is:" + rb2d.velocity.x);
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
        Debug.DrawLine(transform.position, targetPosition, Color.yellow);
        var direction = (targetPosition.x - transform.position.x) < 0 ? -1 : 1;
        MovementType type = Utils.GetMovementType(direction);
        UpdateAnimation(type);
        var movement = new Vector2(direction * acceleration * rb2d.mass, 0);
        if (Math.Abs(rb2d.velocity.x) < speed || 
            rb2d.velocity.x == 0 || 
            (rb2d.velocity.x < 0 != direction < 0)) rb2d.AddForce(movement, ForceMode2D.Force);
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
}
