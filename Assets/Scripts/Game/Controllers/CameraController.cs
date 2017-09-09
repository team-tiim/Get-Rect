using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject target;

    // Use this for initialization
    void Start () {
        target = GameObject.FindGameObjectWithTag("Player");
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);

    }
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
    }
}
