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

    private BlockPlacementValidation blockPlacementValidation;
    private Vector3[] childBlockPositions;
    private float appearingTime = 0.5f;
    private float returningTime = 0.1f;

    public Color Color { get; private set; }
    public Vector3[] ChildBlockPositions { get => childBlockPositions; }

    public void Initialized(BlockPlacementValidation blockPlacementValidation, Vector3 parentPosition)
    {
        this.blockPlacementValidation = blockPlacementValidation;

        Color = transform.GetChild(0).GetComponent<SpriteRenderer>().color;

        childBlockPositions = new Vector3[transform.childCount];
        for ( int i = 0; i < childBlockPositions.Length; i++ )
        {
            childBlockPositions[i] = transform.GetChild(i).localPosition;
        }
        
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
        // Snapping
        float x = Mathf.RoundToInt(transform.position.x - blockCompNumber.x % 2 * 0.5f) + blockCompNumber.x % 2 * 0.5f;
        float y = Mathf.RoundToInt(transform.position.y - blockCompNumber.y % 2 * 0.5f) + blockCompNumber.y % 2 * 0.5f;
        transform.position = new Vector3(x, y, 0);

        bool success = blockPlacementValidation.TryPlaceDragBlock(this);

        // if failed to put it on the board
        if ( success == false )
        {
            StartCoroutine(MoveTo(transform.parent.position, returningTime));
            StartCoroutine(ScaleTo(Vector3.one * 0.5f, returningTime));
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
