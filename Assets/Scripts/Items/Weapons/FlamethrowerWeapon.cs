using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamethrowerWeapon : Weapon
{

    public float length;
    public float radius;
    public float dps;

    public Transform projectileSpawnPoint;
    public Transform projectileStartPoint;

    public Quaternion originalRotation;

    private bool isAttacking;

    // Use this for initialization
    void Start()
    {
        projectileSpawnPoint = transform.Find("projectileSpawnPoint");
        projectileStartPoint = transform.Find("projectileStartPoint");
        if (projectileSpawnPoint == null)
        {
            projectileSpawnPoint = this.transform;
        }
        originalRotation = transform.localRotation;
    }

    protected override void DoAttack(GameObject parent, Vector3 direction, float chargeTime)
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(projectileSpawnPoint.position, radius, direction, length);
        Debug.DrawRay(projectileSpawnPoint.position, direction.normalized * length, Color.red);
        Debug.Log("Player flamethrower attack");
        foreach(RaycastHit2D hit in hits)
        {
            Debug.Log(hit.collider.tag);
        }

    }
}
