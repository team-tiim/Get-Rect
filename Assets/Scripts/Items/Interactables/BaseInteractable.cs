using UnityEngine;

public abstract class BaseInteractable : MonoBehaviour
{

    protected Spawner spawner;
    protected bool isTriggered = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isTriggered || other.tag != "Player")
        {
            return;
        }
        isTriggered = true;
        OnPlayerPickup(other);
        this.spawner.Enable();
        GameObject.Destroy(this.gameObject);
    }

    protected abstract void OnPlayerPickup(Collider2D player);

    protected PlayerBehaviour GetPlayerBehaviour(Collider2D player)
    {
        return player.GetComponent<PlayerBehaviour>();
    }

    public void SetSpawner(Spawner spawner)
    {
        this.spawner = spawner;
    }
}
