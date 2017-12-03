using UnityEngine;

public class MeleeWeapon : Weapon
{
    public GameObject trailPrefab;
    public float swingSpeed = 1;
    public float swingLength;
    public float swingRadius;
    public float swingAngle = 120f;

    private Transform tipPoint;

    public override void Awake()
    {
        base.Awake();
        tipPoint = transform.Find("tip");
    }

    protected override void DoAttack(GameObject parent, Vector3 direction)
    {
        Debug.Log("Player melee attack");
        DoSwing();
        RaycastHit2D[] targets = Physics2D.CircleCastAll(parent.transform.position, swingRadius, direction, swingLength);
        foreach (RaycastHit2D target in targets)
        {
            if (target.transform.CompareTag("Enemy"))
            {
                //Debug.Log("Hit: " + target.transform.gameObject.name);
                target.transform.gameObject.GetComponent<EnemySimpleBehaviour>().TakeDamage(damage);
            }

        }
    }

    private void DoSwing()
    {
        float downSwingTime = swingSpeed * 1 / 3;
        AddTrail(downSwingTime);

        float angle = swingAngle + transform.parent.rotation.z;
        Vector3 axis = transform.parent.rotation.y == 0 ? Vector3.back : Vector3.forward;
        Vector3 pointToRotateAround = new Vector3(handlePoint.position.x, handlePoint.position.y, handlePoint.position.z);
        MovementUtils.Instance.RotateAroundAndBack(transform, handlePoint, axis, angle, downSwingTime, swingSpeed * 2 / 3);
    }

    //TODO add fade out, currenty too abrupt
    private void AddTrail(float swingTime)
    {
        if (trailPrefab == null)
        {
            return;
        }
        GameObject trail = Instantiate(trailPrefab, tipPoint);
        trail.GetComponent<TrailRenderer>().time = swingTime;
        Destroy(trail, swingTime);
    }

}
