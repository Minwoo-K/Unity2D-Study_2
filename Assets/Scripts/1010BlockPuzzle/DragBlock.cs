using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragBlock : MonoBehaviour
{
    [SerializeField]
    private Vector2Int blockNumber;
    [SerializeField]
    private AnimationCurve movingCurve;
    [SerializeField]
    private AnimationCurve scaleCurve;

    private float movingTime = 0.2f;
    private float returningTime = 0.1f;
    
    public void Initialized(Vector3 position)
    {
        StartCoroutine(MoveTo(position, 0.7f));
    }

    // When clicked to be moved
    private void OnMouseDown()
    {
        StopCoroutine("ScaleTo");
        StartCoroutine(ScaleTo(Vector3.one, movingTime));
    }

    // When being moved
    private void OnMouseDrag()
    {
        Vector3 gap = new Vector3(0, 0, 10) + Vector3.up * blockNumber.y * 0.5f;
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + gap;
    }

    // When dropped
    private void OnMouseUp()
    {
        StopCoroutine("ScaleTo");

        StartCoroutine(MoveTo(transform.parent.position, returningTime));
        StartCoroutine(ScaleTo(Vector3.one * 0.5f, returningTime));
    }

    private IEnumerator ScaleTo(Vector3 end, float time)
    {
        Vector3 start = transform.localScale;
        float current = 0;
        float percent = 0;

        while ( percent < 1 )
        {
            current += Time.deltaTime;
            percent = current / time;

            transform.localScale = Vector3.Lerp(start, end, scaleCurve.Evaluate(percent));

            yield return null;
        }
    }

    private IEnumerator MoveTo(Vector3 end, float time)
    {
        Vector3 start = transform.position;
        float current = 0;
        float percent = 0;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / time;

            transform.position = Vector3.Lerp(start, end, movingCurve.Evaluate(percent));

            yield return null;
        }
    }
}
