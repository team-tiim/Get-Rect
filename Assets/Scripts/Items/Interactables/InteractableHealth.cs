using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableHealth : BaseInteractable
{
    protected override void OnPlayerPickup(Collider2D player)
    {
        player.GetComponent<CharacterBehaviourBase>().hp += Random.Range(5, 10);
    }

}
