using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragBlock : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve animationCurve;
    [SerializeField]
    private float appearingTime;

    public void PlayAnimation(Vector3 spawningPoint)
    {
        StartCoroutine(MoveTo(spawningPoint, appearingTime));
    }

    private IEnumerator MoveTo(Vector3 end, float moveTime)
    {
        Vector3 start = transform.position;
        float current = 0;
        float percent = 0;

        while ( percent < 1 )
        {
            current += Time.deltaTime;
            percent = current / moveTime;

            transform.position = Vector3.Lerp(start, end, animationCurve.Evaluate(percent));

            yield return null;
        }
    }
}
