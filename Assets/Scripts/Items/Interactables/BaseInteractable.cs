using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseInteractable : MonoBehaviour {

    public Spawner spawner;
    private bool isTriggered = false;

    // Use this for initialization
    void Start () {
        this.spawner.isEnabled = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(isTriggered || other.tag != "Player")
        {
            return;
        }
        isTriggered = true;
        OnPlayerPickup(other);
        this.spawner.isEnabled = true;
        GameObject.Destroy(this.gameObject);
    }

    protected abstract void OnPlayerPickup(Collider2D player);
}
