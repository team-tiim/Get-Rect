using UnityEngine;
using System;
using System.Collections;

public class ProjectileWeapon : Weapon
{
    public int maxRecoilRotation = 10;
    public float recoilResetMultiplier = 0.5f;

    public float gravityScale;
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;
    public Transform projectileStartPoint;

    public Quaternion originalRotation;

    // Use this for initialization
    void Start()
    {
        if (projectilePrefab == null)
        {
            GameController gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
            projectilePrefab = gc.basicBulletPrefab;
        }
        projectileSpawnPoint = transform.Find("projectileSpawnPoint");
        projectileStartPoint = transform.Find("projectileStartPoint");
        if (projectileSpawnPoint == null)
        {
            projectileSpawnPoint = this.transform;
        }
        originalRotation = transform.localRotation;
    }


    protected override void DoAttack(GameObject parent, Vector3 attackDirection)
    {
        //Debug.Log("Projectile weapon attack");
        SimulateRecoil();
        SpawnPojectile(attackDirection);
        DoKnockback(attackDirection);
    }

    private void SpawnPojectile(Vector3 attackDirection)
    {
        //Debug.Log("Spawning projectile " );
        Projectile p = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity).GetComponent<Projectile>();
        if (projectileStartPoint != null)
        {
            attackDirection = projectileSpawnPoint.position - projectileStartPoint.position;
        }
        p.SetVariables(this, attackDirection);
    }

    private void DoKnockback(Vector3 attackDirection)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerBehaviour pc = player.GetComponent<PlayerBehaviour>();
        //player.GetComponent<Rigidbody2D>().AddForce(-attackDirection.normalized * knockback, ForceMode2D.Impulse);
        pc.DoKnockback(-attackDirection.normalized * knockback);
    }

    private void SimulateRecoil()
    {
        float angle = RandomUtil.GetRandom(-maxRecoilRotation, maxRecoilRotation);
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
       // Debug.Log(angle);
       // Debug.Log(q);
        //transform.rotation *= q;
        transform.RotateAround(transform.position, Vector3.forward, angle);

        //transform.rotation = Quaternion.Lerp(transform.rotation, q, 1);
        //transform.rotation = Quaternion.Slerp(transform.rotation, q, attackCooldown/2);        
        //transform.position -= (transform.rotation * handPoint.position);
        //transform.RotateAround(Vector3.zero, Vector3.forward, angle);
    }

    public void ResetRecoil(Quaternion q)
    {
        StopAllCoroutines();
        StartCoroutine(ResetRecoilCoroutine(q));
    }

    private IEnumerator ResetRecoilCoroutine(Quaternion q)
    {
        yield return new WaitForSeconds(attackCooldown/2);
        Debug.Log("resetting recoil");
        transform.rotation = Quaternion.Lerp(transform.rotation, q, 1);
    }

}
