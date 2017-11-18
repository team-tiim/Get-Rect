using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject interactablePrefab;
    public int spawnTick = 15; //seconds
    public int locationChangeTick; //count
    private bool isEnabled = true;
    private int curCounter = 0;
    private int counter = 0;

    void Start()
    {
        InvokeRepeating("Spawn", 2, spawnTick);
    }

    public void Spawn()
    {
        if (!isEnabled || !gameObject.activeSelf)
        {
            return;
        }
        Disable();
        curCounter++;
        counter++;
        GameObject go = Instantiate(interactablePrefab);
        go.GetComponent<BaseInteractable>().SetSpawner(this);
        go.transform.position = this.transform.position;
    }

    public bool IsDoLocationChange()
    {
        return locationChangeTick <= 0 || curCounter < locationChangeTick;
    }

    public void ResetCurrentCounter()
    {
        this.curCounter = 0;
    }

    public void Enable()
    {
        this.gameObject.SetActive(true);
    }

    public void Disable()
    {
        this.gameObject.SetActive(false);
    }
}
