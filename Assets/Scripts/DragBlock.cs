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

    private Vector3 offset;     // offset value of mouse position when being dragged
    private float returningTime = 0.1f;

    public void Initialized(Vector3 spawningPoint)
    {
        offset = new Vector3(0, BlockCounts.y * 0.5f, 10);
        StartCoroutine(MoveTo(spawningPoint, appearingTime));
    }

    private void OnMouseDown()
    {
        StopCoroutine("ScaleTo");
        StartCoroutine(ScaleTo(Vector3.one));
    }

    private void OnMouseDrag()
    {
        Vector3 input = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = input + offset;
    }

    private void OnMouseUp()
    {
        // blockcount 가 짝수면 0.5를 안더하고, blockcount 가 홀수면 0.5를 더해야한다
        float x = Mathf.RoundToInt(transform.position.x- BlockCounts.x % 2 * 0.5f) + BlockCounts.x % 2 * 0.5f;
        float y = Mathf.RoundToInt(transform.position.y- BlockCounts.y % 2 * 0.5f) + BlockCounts.y % 2 * 0.5f;

        transform.position = new Vector3(x, y, 0);
        //StartCoroutine(ScaleTo(Vector3.one * 0.5f));
        //StartCoroutine(MoveTo(transform.parent.position, returningTime));
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

    private IEnumerator ScaleTo(Vector3 end)
    {
        Vector3 start = transform.localScale;
        float current = 0;
        float percent = 0;

        while ( percent < 1 )
        {
            current += Time.deltaTime;
            percent = current / returningTime;

            transform.localScale = Vector3.Lerp(start, end, scaleCurve.Evaluate(percent));

            yield return null;
        }
    }
}
