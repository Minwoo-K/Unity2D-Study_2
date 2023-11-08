using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragBlock : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve spawningAnimation;
    [SerializeField]
    private AnimationCurve scalingAnimation;
    [SerializeField]
    private Vector2Int blockCompNumber;

    private float appearingTime = 1f;
    private float returningTime = 0.05f;

    public void Initialized(Vector3 parentPosition)
    {
        StartCoroutine(MoveTo(parentPosition, appearingTime));
    }

    private void OnMouseDown()
    {
        StopCoroutine("ScaleTo");
        StartCoroutine(ScaleTo(Vector3.one, returningTime));
    }

    private void OnMouseDrag()
    {
        Vector3 delta = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = delta + new Vector3(0, 0, 10) + Vector3.up;
    }

    private void OnMouseUp()
    {
        StartCoroutine(MoveTo(transform.parent.position, returningTime));
        StartCoroutine(ScaleTo(Vector3.one * 0.5f, returningTime));
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

            transform.position = Vector3.Lerp(start, end, spawningAnimation.Evaluate(percent));

            yield return null;
        }
    }

    private IEnumerator ScaleTo(Vector3 end, float time)
    {
        Vector3 start = transform.localScale;
        float current = 0;
        float percent = 0;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / time;

            transform.localScale = Vector3.Lerp(start, end, scalingAnimation.Evaluate(percent));

            yield return null;
        }
    }
}
