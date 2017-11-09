﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableArmor : BaseInteractable {

    public GameObject[] prefabs;   

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
        PlayerBehaviour player = other.GetComponent<PlayerBehaviour>();
        GameObject pref = RandomUtil.GetRandomFromArray(prefabs);
        player.EquipArmor(pref.GetComponent<ArmorHolder>());
    }
}
