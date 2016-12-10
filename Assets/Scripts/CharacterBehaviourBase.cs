using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerBase : MonoBehaviour {

    public float speed = 1;
    public int hp = 10;

    protected Animator animator;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {		
	}


    protected void updateAnimation(float moveHorizontal)
    {
        if (moveHorizontal > 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            animator.SetBool("isMove", true);
        }
        else if (moveHorizontal < 0)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            animator.SetBool("isMove", true);
        }
        else
        {
            animator.SetBool("isMove", false);
        }
    }
}
