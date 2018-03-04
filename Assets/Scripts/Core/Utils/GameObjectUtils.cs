using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectUtils : Singleton<GameObjectUtils>
{
    protected GameObjectUtils() { }

    public void InstantiateAndPlayAnimation(GameObject gameObject, Vector3 location)
    {
        GameObject expl = Instantiate(gameObject, location, gameObject.transform.rotation);
        Animator animator = expl.GetComponent<Animator>();
        Destroy(expl, animator.GetCurrentAnimatorStateInfo(0).length);
    }


}
