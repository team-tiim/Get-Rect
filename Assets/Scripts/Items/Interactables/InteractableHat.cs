using UnityEngine;

public class InteractableHat : BaseInteractable {

    public GameObject[] prefabs;

    protected override void OnPlayerPickup(Collider2D player)
    {
        GameObject pref = RandomUtil.GetRandomFromArray(prefabs);
        GetPlayerBehaviour(player).EquipHat(pref.GetComponent<ArmorHolder>());
        Destroy(gameObject);
    }

}
