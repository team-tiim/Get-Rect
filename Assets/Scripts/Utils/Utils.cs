using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils {

    public static IEnumerator ChangeColor(SpriteRenderer renderer, Color origColor)
    {
        renderer.color = Color.red;
        yield return new WaitForSeconds(0.3f);
        renderer.color = origColor;
    }
}
