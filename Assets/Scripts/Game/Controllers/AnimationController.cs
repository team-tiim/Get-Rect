using Anima2D;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{

    private Dictionary<AnimationType, AnimationHolder> animationHolders = new Dictionary<AnimationType, AnimationHolder>();
    private Dictionary<AnimationType, SpriteMeshInstance> spriteMeshes = new Dictionary<AnimationType, SpriteMeshInstance>();

    public GameObject origArmor;
    public GameObject origBody;
    public GameObject origHat;

    public Animator animator;
    private MovementType currentMovement;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteMeshes.Add(AnimationType.ARMOR, this.gameObject.transform.Find("armor").GetComponent<SpriteMeshInstance>());
        spriteMeshes.Add(AnimationType.BODY, this.gameObject.transform.Find("body").GetComponent<SpriteMeshInstance>());
        spriteMeshes.Add(AnimationType.HAT, this.gameObject.transform.Find("hat").GetComponent<SpriteMeshInstance>());

        AddAnimationHolder(origArmor);
        AddAnimationHolder(origBody);
        AddAnimationHolder(origHat);
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeAnimation(AnimationHolder animationHolder)
    {
        AnimationType type = animationHolder.animationType;
        if (!spriteMeshes.ContainsKey(type))
        {
            Debug.Log("Animation type change not implemented:" + type.ToString());
            return;
        }
        Debug.Log("Animation type:" + type.ToString());
        Debug.Log("Animation type:" + animationHolder.front.ToString());
        animationHolders[animationHolder.animationType] = animationHolder;
        spriteMeshes[animationHolder.animationType].spriteMesh = animationHolder.front;
    }

    public void UpdateMoveAnimations(MovementType movementType)
    {
        if (currentMovement == movementType)
        {
            return;
        }
        bool isMoving = movementType != MovementType.IDLE;
        foreach (KeyValuePair<AnimationType, SpriteMeshInstance> entry in spriteMeshes)
        {
            entry.Value.spriteMesh = isMoving ? animationHolders[entry.Key].side : animationHolders[entry.Key].front;
        }
        UpdateRotation(movementType);
        animator.SetBool("isMove", isMoving);
        currentMovement = movementType;
    }

    private void UpdateRotation(MovementType movementType)
    {
        float rotation = movementType == MovementType.WALK_RIGHT || movementType == MovementType.IDLE  && currentMovement == MovementType.WALK_RIGHT ? 180 : 0;
        this.gameObject.transform.localRotation = Quaternion.Euler(0, rotation, 0);
    }

    private void AddAnimationHolder(GameObject go)
    {
        if (go == null)
        {
            return;
        }
        AnimationHolder holder = go.GetComponent<AnimationHolder>();
        if (holder == null)
        {
            return;
        }
        animationHolders.Add(holder.animationType, holder);
    }

}
