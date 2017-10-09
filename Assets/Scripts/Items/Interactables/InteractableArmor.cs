using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableArmor : BaseInteractable {

    public GameObject[] weaponPrefabs;   

    void Awake()
    {

    }
    // Use this for initialization
    void Start () {
    }


    protected override void OnPlayerPickup(Collider2D player)
    {
        AddArmor(player);
    }

    private void AddArmor(Collider2D other)
    {
        Armor a = new Armor(2/3, 100);    
        PlayerController player = other.GetComponent<PlayerController>();
        player.EquipArmor(a);
    }
}
