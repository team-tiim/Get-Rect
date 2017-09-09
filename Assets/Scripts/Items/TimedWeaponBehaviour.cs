using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedWeaponBehaviour : TimedBehaviour {

    protected override void OnTimerEnd()
    {
        Debug.Log("Weapon timed out");
        this.gameObject.GetComponent<CharacterBehaviourBase>().ResetWeapon();
        Destroy(this);
    }
}
