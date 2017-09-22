using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyingBehaviour : EnemySimpleBehaviour
{

    private Vector2 direction;

	// Use this for initialization
	void Start () {
        base.Start();
        //move();
        direction = (new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f))).normalized;
        //transform.Rotate(direction);
        //rb2d.velocity = Random.insideUnitCircle * speed;
        rb2d.AddForce(transform.up * speed);
    }
	
	// Update is called once per frame
	void Update () {
        //transform.position += moveDirection * speed * Time.deltaTime;
        Vector3 dir = direction;
        Vector2 newPos = transform.position + dir * speed * Time.deltaTime;
        //rb2d.MovePosition(newPos);
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        //Debug.Log("Collided");
        move(collision);
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            Debug.Log("hit player");
            Attack(collision.collider.gameObject, damage);
        }
    }
    private void move(Collision2D collision)
    {
        transform.localRotation = Quaternion.Euler(0, 0, Random.value * 360);
        rb2d.AddForce(transform.up * speed);
        //var x = Random.Range(-1f, 1f);
        //var y = Random.Range(-1f, 1f);
        //direction = new Vector2(x, y);
        //GetComponent<Rigidbody2D>().AddForce(moveDirection * speed * 100);
        //direction = col.contacts[0].normal;
        //direction = new Vector2(Random.Range(-359, 359), Random.Range(-359, 359));
        //Quaternion.AngleAxis(Random.Range(-70.0f, 70.0f), Vector2.up) * direction;
    }

    private void move2(Collision2D collision)
    {
        direction = collision.contacts[0].normal;
        direction = Quaternion.AngleAxis(Random.Range(-70.0f, 70.0f), Vector2.right) * direction;
        transform.rotation = Quaternion.LookRotation(direction);
    }
}
