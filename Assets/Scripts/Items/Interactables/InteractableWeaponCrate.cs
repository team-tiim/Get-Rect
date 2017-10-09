using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableWeaponCrate : BaseInteractable {

    public GameObject[] weaponPrefabs;

    void Awake()
    {
        foreach (GameObject wp in weaponPrefabs)
        {
            if (wp.GetComponent<Weapon>() == null)
            {
                throw new System.Exception("Interactable weapon crate"  + this.name + " does not have valid weapon prefab: " + wp.name);
            }
        }
    }
    // Use this for initialization
    void Start () {

    }


    protected override void OnPlayerPickup(Collider2D player)
    {
        AddWeapon(player);
    }

    private void AddWeapon(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        other.gameObject.AddComponent<TimedWeaponBehaviour>().duration = spawner.spawnTick;
        GameObject go = RandomUtil.GetRandomFromArray(weaponPrefabs);
        player.EquipWeapon(go);
    }
}
