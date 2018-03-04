using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class ProjectileWeapon : Weapon {

    public float minChargeTime = 0.0f;
    public float maxChargeTime = 3.0f;

    public int maxRecoilRotation = 10;
    public float recoilResetMultiplier = 0.5f;

    public float gravityScale;
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;
    
    // Use this for initialization
    public override void Awake() {
        projectileSpawnPoint = transform.Find("projectileSpawnPoint");
        if (projectilePrefab == null)
        {
            GameController gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
            projectilePrefab = gc.basicBulletPrefab;
        }
        base.Awake();
    }

    protected override void DoAttack(GameObject parent, Vector3 direction, float chargeTime)
    {
        float speedMultiplier = isChargedWeapon ? GetSpeedMultiplier(chargeTime) : 1.0f;

        SimulateRecoil();
        SpawnPojectile(direction, speedMultiplier);
        DoKnockback(direction);
    }

    private float GetSpeedMultiplier(float chargeTime)
    {
        Debug.Log("charge time " + chargeTime);
        chargeTime = Mathf.Min(maxChargeTime, chargeTime);
        float speedMultiplier = (chargeTime - minChargeTime) / (maxChargeTime - minChargeTime);
        Debug.Log("speed multiplier " + speedMultiplier);
        return speedMultiplier;
    }

    protected void SpawnPojectile(Vector3 attackDirection, float speedMultiplier)
    {
        Projectile p = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity).GetComponent<Projectile>();
        p.ApplySpeedMultiplier(speedMultiplier);
        p.SetVariables(this, attackDirection);
    }

    protected void DoKnockback(Vector3 attackDirection)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerBehaviour pc = player.GetComponent<PlayerBehaviour>();
        //player.GetComponent<Rigidbody2D>().AddForce(-attackDirection.normalized * knockback, ForceMode2D.Impulse);
        pc.DoKnockback(-attackDirection.normalized * knockback);
    }

    protected void SimulateRecoil()
    {
        float angle = RandomUtils.GetRandom(-maxRecoilRotation, maxRecoilRotation);
        Rotate(angle);
    }

}
