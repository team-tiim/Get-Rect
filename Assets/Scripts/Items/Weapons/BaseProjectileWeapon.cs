using UnityEngine;

public abstract class BaseProjectileWeapon : Weapon {

    public int maxRecoilRotation = 10;
    public float recoilResetMultiplier = 0.5f;
    public float gravityScale;
    public GameObject projectilePrefab;

    protected Transform projectileSpawnPoint;

    public override void Awake()
    {
        projectileSpawnPoint = transform.Find("projectileSpawnPoint");
        if(projectileSpawnPoint == null)
        {
            projectileSpawnPoint = transform;
        }

        if (projectilePrefab == null)
        {
            GameController gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
            projectilePrefab = gc.basicBulletPrefab;
        }
        base.Awake();
    }

    public override void DoAttack(WeaponAttackParams parameters)
    {
        if (!CanAttack())
        {
            return;
        }
        base.DoAttack(parameters);

        SimulateRecoil();
        SpawnPojectile(parameters);
        DoKnockback(parameters);
    }

    protected void SpawnPojectile(WeaponAttackParams parameters)
    {
        Projectile p = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity).GetComponent<Projectile>();
        p.SetVariables(this, parameters.direction);
        OnProjectileSpawn(parameters, p);
    }

    protected virtual void OnProjectileSpawn(WeaponAttackParams parameters, Projectile projectile)
    {
        //NOOP
    }

    protected void DoKnockback(WeaponAttackParams parameters)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerBehaviour pc = player.GetComponent<PlayerBehaviour>();
        pc.DoKnockback(-parameters.direction.normalized * knockback);
    }

    protected void SimulateRecoil()
    {
        if(maxRecoilRotation == 0.0f)
        {
            return;
        }

        float angle = RandomUtils.GetRandom(-maxRecoilRotation, maxRecoilRotation);
        Rotate(angle);
    }
}
