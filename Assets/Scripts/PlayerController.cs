using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Rigidbody2D rb;
    // Use this for initialization
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        Debug.Log(rb.transform.position.x);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Pressed A");
            rb.AddForce(Vector2.left * 1, ForceMode2D.Impulse);
            rb.AddForce(Vector3.forward * 1, ForceMode2D.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            rb.AddForce(Vector3.right * 1, ForceMode2D.Impulse);
            rb.AddForce(Vector3.forward * 1, ForceMode2D.Impulse);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            Vector3 dir = new Vector3(-10f, 15f, 0f);
            dir.Normalize();
            rb.AddForce(Vector3.up * 1, ForceMode2D.Impulse);
        }
    }
}
