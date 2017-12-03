using Anima2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAnimationController : MonoBehaviour {
    public Animator animator;
    protected MovementType currentMovement;

    // Use this for initialization
    public virtual void Awake () {
        animator = GetComponent<Animator>();
    }

    public virtual void UpdateMoveAnimations(MovementType type)
    {
        if (currentMovement == type)
        {
            return;
        }
        UpdateRotation(type);
        animator.SetBool("isMove", type != MovementType.IDLE);
        currentMovement = type;
    }

    public float UpdateAttackAnimations(AttackType type)
    {
        animator.SetInteger("attackType", (int)type);
        animator.SetTrigger("doAttack");
        return animator.GetCurrentAnimatorStateInfo(0).length;
    }

    public int GetStringHash(string s)
    {
        int hash = Animator.StringToHash(s);
        return hash;
    }

    private void UpdateRotation(MovementType movementType)
    {
        float rotation = movementType == MovementType.WALK_RIGHT || movementType == MovementType.IDLE && currentMovement == MovementType.WALK_RIGHT ? 180 : 0;
        this.gameObject.transform.localRotation = Quaternion.Euler(0, rotation, 0);
    }

}
