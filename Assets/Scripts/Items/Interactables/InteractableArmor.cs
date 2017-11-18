using UnityEngine;

public class InteractableArmor : BaseInteractable {

    public GameObject[] prefabs;   

    protected override void OnPlayerPickup(Collider2D player)
    {
        GameObject pref = RandomUtil.GetRandomFromArray(prefabs);
        GetPlayerBehaviour(player).EquipArmor(pref.GetComponent<ArmorHolder>());
        Destroy(gameObject);
    }

}
