using System.Collections.Generic;

public static class RandomUtils {

    public static int GetRandom(int min, int max)
    {
        return UnityEngine.Random.Range(min, max);
    }

    public static int GetRandom(int max)
    {
        return GetRandom(0, max);
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
