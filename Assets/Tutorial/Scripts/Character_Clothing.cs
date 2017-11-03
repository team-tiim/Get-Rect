using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anima2D;

public class Character_Clothing : MonoBehaviour {

    public SpriteMeshAnimation spr_anim;

	public void ChangeHat()
    {
        int frames = spr_anim.frames.Length;
        int currFrame = spr_anim.frame;

        if (currFrame + 1 < frames)
        {
            currFrame += 1;
        }
        else
        {
            currFrame = 0;
        }

        spr_anim.frame = currFrame;
        
    }
}
