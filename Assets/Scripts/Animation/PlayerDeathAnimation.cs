using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathAnimation : MonoBehaviour {

    public float floatTime = 2.0f;
    public float floatDistance = 2.0f;

    private Animator animator;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        Vector3 endMarker = transform.position;
        endMarker.y += floatDistance;
        float delay = animator.GetCurrentAnimatorStateInfo(0).length;

        StartCoroutine(FloatUp(delay, floatTime, transform.position, endMarker));
    }	

    IEnumerator FloatUp(float delay, float duration, Vector3 startMarker, Vector3 endMarker)
    {
        yield return new WaitForSeconds(delay);

        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            float normalizedTime = t / duration;
            transform.position = Vector3.Lerp(startMarker, endMarker, normalizedTime);
            yield return null;
        }

        transform.position = endMarker;
    }

}
