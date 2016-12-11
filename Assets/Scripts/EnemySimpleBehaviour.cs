using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EnemySimpleBehaviour : CharacterBehaviourBase
{

    public GameObject target;
    public float turnSpeed = 1;
    public float aggroDistance = 1;
    public float patrolDistance = 5;
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
        target = GameObject.FindGameObjectWithTag("Player");
        startingPos = transform.position;
        amps_sound = GetComponent<AudioSource>();
        //Debug.Log("Target found: "+ target.name);
    }

	// Update is called once per frame
	void Update () {
        float targetDistance = getDistanceTo(target.transform.position);
        if (targetDistance < range )
        {
            float timeFromLastAttack = Time.time - attackTime;

            if(timeFromLastAttack > coolDown)
            {
                Debug.Log("attacked player");
                attackTime = Time.time;
                Attack(target, damage);
				amps_sound.Play ();
            }
        }
		else if(targetDistance < aggroDistance)
        {
            //Debug.Log("Target found: " + target.name);
            moveTowards(target.transform.position);
        }
	}

    private void patrol()
    {
        if (getDistanceTo(startingPos) >= patrolDistance)
        {
            moveTowards(startingPos);
        } else
        {
            moveTowards(startingPos);
        }
    }

    private void moveTowards(Vector3 targetPosition)
    {
        Debug.DrawLine(transform.position, targetPosition, Color.yellow);
        if (transform.position.x < targetPosition.x)
        {
            //Debug.Log("Player to right");
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            transform.position += transform.right * speed * Time.deltaTime;
        } else
        {
            //Debug.Log("Player to left");
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            transform.position += transform.right * speed * Time.deltaTime;
        }
        
    }

    private float getDistanceTo(Vector3 position)
    {
        return Vector2.Distance(transform.position, position);
    }
}
