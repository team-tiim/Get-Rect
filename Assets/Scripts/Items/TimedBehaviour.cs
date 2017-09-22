using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TimedBehaviour : MonoBehaviour {

    public int duration;
    private float startTime;

	// Use this for initialization
	void Start () {
        startTime = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
        float diff = Time.time - startTime;
        if (diff > duration)
        {
            OnTimerEnd();
        }
	}

    protected abstract void OnTimerEnd();
}
