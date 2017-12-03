using System.Collections;
using UnityEngine;


public abstract class Weapon : MonoBehaviour
{
    public Transform handlePoint;
    public bool isVertical;
    public int damage = 1;
    public float attackCooldown = 1;
    public float knockback;

    private float lastAttack = -1;

    private TimedWeaponBehaviour timedBehaviour;
    private Quaternion previousRotation;

    public virtual void Awake()
    {
        handlePoint = transform.Find("handle");
        //this.gameObject.GetComponent<Animator>().Play();
    }

    public virtual void Attack(GameObject parent, Vector3 attackDirection)
    {
        //Debug.Log("weapon attack");
        if (!CanAttack())
        {
            return;
        }
        lastAttack = Time.time;
        PlayAnimation();
        DoAttack(parent, attackDirection);
    }

    public bool CanAttack()
    {
        return lastAttack == -1 || (Time.time - lastAttack) > attackCooldown;
    }

    private void PlayAnimation()
    {
        this.gameObject.GetComponent<Animator>().SetTrigger("Attack");
    }

    protected abstract void DoAttack(GameObject parent, Vector3 direction);

    public void ResetRotation(Quaternion q)
    {
        StopAllCoroutines();
        StartCoroutine(DoResetRotation(q));
    }

    protected void Rotate(float angle)
    {
        Debug.Log(angle);
        transform.RotateAround(handlePoint.position, Vector3.forward, angle);
    }

    private IEnumerator DoResetRotation(Quaternion q)
    {
        yield return new WaitForSeconds(attackCooldown * 2/3);
        float angle = Quaternion.Angle(q, transform.rotation);
        Debug.Log(angle);
        transform.RotateAround(handlePoint.position, Vector3.forward, angle);
    }

    private IEnumerator DoRotation(float angle)
    {
        yield return new WaitForSeconds(attackCooldown / 2);

    }

    public int Damage
    {
        get { return damage; }
    }

}

