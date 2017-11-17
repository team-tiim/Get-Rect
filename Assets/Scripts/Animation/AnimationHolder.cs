using Anima2D;
using UnityEngine;

public class AnimationHolder : MonoBehaviour {

    public SpriteMesh front;
    public SpriteMesh side;
    public AnimationType animationType;

    // Use this for initialization
    void Start () {
		if(side == null)
        {
            side = front;
        }
	}
}
