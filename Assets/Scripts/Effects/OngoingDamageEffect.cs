using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OngoingDamageEffect : MonoBehaviour {
    private static readonly float ONE_SECOND = 1.0f;

    public float duration = 3.0f;
    public int dps = 1;

    private float nextActionTime = 0.0f;

    void Start () {

    }

    void Update()
    {
        if (Time.time > nextActionTime) {
            nextActionTime = Time.time + ONE_SECOND;
            gameObject.GetComponent<CharacterBehaviourBase>().TakeDamage(dps);
        }
    }


}
