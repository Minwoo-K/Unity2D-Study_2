using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragBlock : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve animationCurve;
    [SerializeField]
    private AnimationCurve curveScale;
    [SerializeField]
    private float appearingTime;

    [field: SerializeField]
    public Vector2Int BlockCount { get; private set; }

    private float returningTime = 0.1f;

    public void Initialized(Vector3 spawningPoint)
    {
        StartCoroutine(MoveTo(spawningPoint, appearingTime));
    }

    

    /// <summary>
    /// Built-in function triggered when the object gets pressed down
    /// Running one frame when mouse down
    /// </summary>
    private void OnMouseDown()
    {
        StopCoroutine("OnScaleTo");
        StartCoroutine(OnScaleTo(Vector3.one));
    }

    /// <summary>
    /// Built-in function triggered when the object is being dragged by the input or mouse
    /// Running every frame while dragging
    /// </summary>
    private void OnMouseDrag()
    {
        Vector3 delta = new Vector3(0, BlockCount.y * 0.5f, 10);
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + delta;
    }

    /// <summary>
    /// Built-in function triggered when the object is done being pressed down
    /// Running one time when mouse up
    /// </summary>
    private void OnMouseUp()
    {
        StartCoroutine(OnScaleTo(Vector3.one * 0.5f));
        StartCoroutine(MoveTo(transform.parent.position, returningTime));

        transform.position = transform.parent.position;
    }

    private IEnumerator OnScaleTo(Vector3 end)
    {
        float current = 0;
        float percent = 0;

        while ( percent < 1 )
        {
            current += Time.deltaTime;
            percent = current / returningTime;

            transform.localScale = Vector3.Lerp(transform.localScale, end, curveScale.Evaluate(percent));

            yield return null;
        }
    }

    private IEnumerator MoveTo(Vector3 end, float moveTime)
    {
        Vector3 start = transform.position;
        float current = 0;
        float percent = 0;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / moveTime;

            transform.position = Vector3.Lerp(start, end, animationCurve.Evaluate(percent));

            yield return null;
        }
    }
}
