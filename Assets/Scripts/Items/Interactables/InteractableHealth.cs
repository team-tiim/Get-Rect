using UnityEngine;

public class InteractableHealth : BaseInteractable
{
    protected override void OnPlayerPickup(Collider2D player)
    {
        GetPlayerBehaviour(player).hp += Random.Range(5, 10);
    }

}
