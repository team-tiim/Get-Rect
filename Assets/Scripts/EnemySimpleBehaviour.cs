using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySimpleBehaviour : CharacterBehaviourBase
{

    public GameObject target;
    public float turnSpeed = 1;
    public float aggroDistance = 1;
    public float patrolDistance = 5;

    private Vector2 startingPos;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player");
        startingPos = transform.position;
        Debug.Log("Target found: "+ target.name);
    }
	
	// Update is called once per frame
	void Update () {
		if(getDistanceTo(target.transform.position) < aggroDistance)
        {
            Debug.Log("Target found: " + target.name);
            moveTowards(target.transform.position);
        }
        else
        {
            patrol();
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
        updateAnimation(targetPosition.x - transform.position.x);
        if (transform.position.x < targetPosition.x)
        {
            Debug.Log("Player to right");
            transform.position += transform.right * speed * Time.deltaTime;
        } else
        {
            Debug.Log("Player to left");
            transform.position += transform.right * speed * Time.deltaTime;
        }
        
    }

    private void attack()
    {

    }

    private float getDistanceTo(Vector3 position)
    {
        return Vector2.Distance(transform.position, position);
    }
}
