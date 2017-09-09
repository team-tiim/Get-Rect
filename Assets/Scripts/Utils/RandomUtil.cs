using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomUtil {

    public static int GetRandom(int max)
    {
        return UnityEngine.Random.Range(0, max);
    }


    public static T GetRandomFromArray<T>(T[] array)
    {
        int idx = GetRandom(array.Length);
        return array[idx];
    }

    public static T GetAndRemoveRandom<T>(List<T> list)
    {
        int idx = GetRandom(list.Count);
        T value = list[idx];
        list.RemoveAt(idx);
        return value;
    }

}
