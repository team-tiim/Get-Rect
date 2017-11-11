using Anima2D;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : BasicAnimationController
{

    private Dictionary<AnimationType, AnimationHolder> animationHolders = new Dictionary<AnimationType, AnimationHolder>();
    private Dictionary<AnimationType, SpriteMeshInstance> spriteMeshes = new Dictionary<AnimationType, SpriteMeshInstance>();

    public GameObject origArmor;
    public GameObject origBody;
    public GameObject origHat;


    public override void Awake()
    {
        base.Awake();
        AddSpriteMesh(AnimationType.ARMOR, "armor");
        AddSpriteMesh(AnimationType.BODY, "body");
        AddSpriteMesh(AnimationType.HAT, "hat");

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
        animationHolders[animationHolder.animationType] = animationHolder;
        spriteMeshes[animationHolder.animationType].spriteMesh = animationHolder.front;
    }

    public override void UpdateMoveAnimations(MovementType movementType)
    {
        if (currentMovement == movementType)
        {
            return;
        }
        foreach (KeyValuePair<AnimationType, SpriteMeshInstance> entry in spriteMeshes)
        {
            entry.Value.spriteMesh = movementType != MovementType.IDLE ? animationHolders[entry.Key].side : animationHolders[entry.Key].front;
        }
        base.UpdateMoveAnimations(movementType);

    }

    private void AddSpriteMesh(AnimationType type, string name)
    {
        Transform spriteTransform = this.gameObject.transform.Find(name);

        if (spriteTransform == null || spriteTransform.GetComponent<SpriteMeshInstance>() == null)
        {
            return;
        }
        spriteMeshes.Add(type, spriteTransform.GetComponent<SpriteMeshInstance>());
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
