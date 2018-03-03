using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviourBase : MonoBehaviour
{


    public Sprite[] portraitFrames;
    public int maxAnger;
    public int angerLevel;

    void Awake()
    {
        maxAnger = portraitFrames.Length - 1;
        angerLevel = 0;
    }


    void Update()
    {

    }

    public Sprite GetCurrentImage()
    {
        if (angerLevel < maxAnger - 1)
        {
            return portraitFrames[angerLevel];
        }
        return portraitFrames[maxAnger];
    }
}
