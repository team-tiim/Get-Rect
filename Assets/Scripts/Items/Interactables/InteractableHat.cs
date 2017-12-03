using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableHat : BaseInteractable {

    public GameObject[] prefabs;

    protected override void OnPlayerPickup(Collider2D player)
    {
        AddArmor(player);
    }

    private void AddArmor(Collider2D other)
    {
        PlayerBehaviour player = other.GetComponent<PlayerBehaviour>();
        GameObject pref = RandomUtils.GetRandomFromArray(prefabs);
        player.EquipHat(pref.GetComponent<ArmorHolder>());
        Destroy(gameObject);
    }
}
