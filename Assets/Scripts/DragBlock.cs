using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragBlock : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve spawningAnimation;
    [SerializeField]
    private Vector2Int blockCompNumber;

    private float appearingTime = 1f;

    public void Initialized()
    {
        StartCoroutine(MoveTo(Vector3.zero));
    }

    private IEnumerator MoveTo(Vector3 end)
    {
        Vector3 start = transform.position;
        float current = 0;
        float percent = 0;

        while ( percent < 1 )
        {
            current += Time.deltaTime;
            percent = current / appearingTime;

            transform.localPosition = Vector3.Lerp(start, end, spawningAnimation.Evaluate(percent));

            yield return null;
        }
    }
}
