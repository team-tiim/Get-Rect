using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor {
    private const int MAX_VALUE = 100;
    private float blockPercentage = 2/3;
    private int value = 50;

    public Armor()
    {
    }

    public Armor(float blockPercentage, int value)
    {
        this.blockPercentage = blockPercentage;
        this.value = value;
    }

    public void Increase(int incValue)
    {
        value += incValue;
        if(value > MAX_VALUE)
        {
            value = MAX_VALUE;
        }
    }

    public void Decrease(int decValue)
    {
        value -= decValue;
        if (value < 0)
        {
            value = 0;
        }
    }

    public float BlockPercentage
    {
        get { return blockPercentage; }
    }

    public int Value
    {
        get { return value; }
    }
}
