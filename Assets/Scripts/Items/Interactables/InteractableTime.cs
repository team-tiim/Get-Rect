using UnityEngine;

public class InteractableTime : BaseInteractable
{
    protected override void OnPlayerPickup(Collider2D player)
    {
        //TODO move to GameController and change animation back to normal if needed
        GameObject.Find("GameControllers").GetComponent<GameController>().levelTime += Random.Range(15, 30);
    }
}
