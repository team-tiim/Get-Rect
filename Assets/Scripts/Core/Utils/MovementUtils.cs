using System.Collections;
using UnityEngine;

public class MovementUtils : Singleton<MovementUtils>
{
    protected MovementUtils() { }

    public Coroutine RotateAroundAndBack(Transform transform, Transform point, Vector3 axis, float rotateAmount, float rotateTime, float rotateBackTime)
    {
        IEnumerator rotateBack = RotateAroundPoint(transform, point, axis, -rotateAmount, rotateBackTime, null);
        return StartCoroutine(RotateAroundPoint(transform, point, axis, rotateAmount, rotateTime, rotateBack));
    }

    public Coroutine RotateTowardsAndBack(Transform transform, Vector3 target, float rotateTime, float rotateBackTime)
    {
        Vector3 angles = GetRotationAngles(transform, target);
        IEnumerator rotateBack = RotateTowards(transform, angles * -1, rotateBackTime, null);
        return StartCoroutine(RotateTowards(transform, angles, rotateTime, rotateBack));
    }

    public void RotateTowards(Transform transform, Vector3 target)
    {
        Vector3 angles = GetRotationAngles(transform, target);
        transform.Rotate(angles);
    }

    public Coroutine RotateTowards(Transform transform, Vector3 target, float rotateTime)
    {
        Vector3 angles = GetRotationAngles(transform, target);
        return StartCoroutine(RotateTowards(transform, angles, rotateTime, null));
    }

    public IEnumerator RotateAroundPoint(Transform transform, Transform point, Vector3 axis, float rotateAmount, float rotateTime, IEnumerator nextCoroutine)
    {
        float step = 0.0f; //non-smoothed
        float rate = 1.0f / rotateTime; //amount to increase non-smooth step by
        float smoothStep = 0.0f; //smooth step this time
        float lastStep = 0.0f; //smooth step last time
        while (step < 1.0)
        { // until we're done
            step += Time.deltaTime * rate; //increase the step
            smoothStep = Mathf.SmoothStep(0.0f, 1.0f, step); //get the smooth step
            transform.RotateAround(point.position, axis, rotateAmount * (smoothStep - lastStep));
            lastStep = smoothStep; //store the smooth step
            yield return null;
        }
        //finish any left-over
        if (step > 1.0)
        {
            transform.RotateAround(point.position, axis, rotateAmount * (1.0f - lastStep));
        }

        if (nextCoroutine != null)
        {
            StartCoroutine(nextCoroutine);
        }
    }

    public IEnumerator RotateTowards(Transform transform, Vector3 angles, float rotateTime, IEnumerator nextCoroutine)
    {
        float step = 0.0f; //non-smoothed
        float rate = 1.0f / rotateTime; //amount to increase non-smooth step by
        float smoothStep = 0.0f; //smooth step this time
        float lastStep = 0.0f; //smooth step last time
        while (step < 1.0)
        { // until we're done
            step += Time.deltaTime * rate; //increase the step
            smoothStep = Mathf.SmoothStep(0.0f, 1.0f, step); //get the smooth step
            transform.Rotate(angles * (smoothStep - lastStep));
            lastStep = smoothStep; //store the smooth step
            yield return null;
        }
        //finish any left-over
        if (step > 1.0)
        {
            transform.Rotate(angles * (1.0f - lastStep));
        }

        if (nextCoroutine != null)
        {
            StartCoroutine(nextCoroutine);
        }
    }

    private static Vector3 GetRotationAngles(Transform transform, Vector3 target)
    {
        float angle = Vector3.SignedAngle(transform.right, (target - transform.position), transform.forward);
        return new Vector3(0, 0, angle);
    }

    public Quaternion GetRotationTowards(Vector3 fromPosition, Vector3 toPosition, bool flipped)
    {
        int flip = flipped ? -1 : 1;
        Vector3 vectorToTarget = new Vector3(flip * (toPosition.x - fromPosition.x), flip * (toPosition.y - fromPosition.y), 0);
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        return Quaternion.AngleAxis(angle, Vector3.forward);
    }

}
