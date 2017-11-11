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

    public virtual void UpdateMoveAnimations(MovementType movementType)
    {
        if (currentMovement == movementType)
        {
            return;
        }
        UpdateRotation(movementType);
        animator.SetBool("isMove", movementType != MovementType.IDLE);
        currentMovement = movementType;
    }

    private void UpdateRotation(MovementType movementType)
    {
        float rotation = movementType == MovementType.WALK_RIGHT || movementType == MovementType.IDLE && currentMovement == MovementType.WALK_RIGHT ? 180 : 0;
        this.gameObject.transform.localRotation = Quaternion.Euler(0, rotation, 0);
    }

}
