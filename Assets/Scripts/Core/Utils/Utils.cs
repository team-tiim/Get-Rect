using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils {


    public static IEnumerator ChangeColor(SpriteRenderer renderer, Color origColor)
    {
        if (renderer != null)
        {
            renderer.color = Color.red;
            yield return new WaitForSeconds(0.3f);
            renderer.color = origColor;
        }
    }

    public static bool IsNullOrEmpty(Array array)
    {
        return (array == null || array.Length == 0);
    }

    public static List<GameObject> FindChildObjects(this GameObject parent, string tag)
    {
        Component[] cs = parent.GetComponentsInChildren(typeof(Transform), true);
        List<GameObject> result = new List<GameObject>();
        foreach (Component c in cs)
        {
            if (c.CompareTag(tag))
            {
                result.Add(c.gameObject);
            }
        }
        return result;
    }

    public static MovementType GetMovementType(float moveHorizontal)
    {
        if (moveHorizontal == 0)
        {
            return MovementType.IDLE;
        }
        if (moveHorizontal > 0)
        {
            return MovementType.WALK_RIGHT;
        }
        return MovementType.WALK_LEFT;
    }
}
