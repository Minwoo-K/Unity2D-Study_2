using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragBlock : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve animationCurve;
    [SerializeField]
    private AnimationCurve scaleCurve;
    [SerializeField]
    private float appearingTime;
    [field:SerializeField]
    public Vector2Int BlockCounts { get; private set; }

    private Vector3 offset = new Vector3(0, 0, 10);      // offset value of mouse position when being dragged

    public void Initialized(Vector3 spawningPoint)
    {
        StartCoroutine(MoveTo(spawningPoint, appearingTime));
    }

    private void OnMouseDown()
    {
        transform.localScale = Vector3.one;
    }

    private void OnMouseDrag()
    {
        
    }

    private void OnMouseUp()
    {
        transform.localScale = Vector3.one * 0.5f;
        transform.position = transform.parent.position;
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
