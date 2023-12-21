using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Block : MonoBehaviour
{
    [SerializeField]
    private Color[] blockColorList;
    [SerializeField]
    private Image blockImage;
    [SerializeField]
    private TextMeshProUGUI textBlockNumber;

    public Node Target { get; private set; }
    
    private int numeric;
    private bool combined = false;   // Whether block has been combined or not

    public int Numeric
    {
        set
        {
            numeric = value;
            textBlockNumber.text = numeric.ToString();
            blockImage.color = blockColorList[(int)Mathf.Log(value, 2) - 1];
        }
        get => numeric;
    }

    public bool NeedDestroyed { get; private set; }

    public void Initialized()
    {
        Numeric = UnityEngine.Random.Range(0, 100) < 90 ? 2 : 4;

        StartCoroutine(OnScaleTo(Vector3.zero, Vector3.one, 0.15f));
    }

    public void MoveToNode(Node to)
    {
        Target = to;
        combined = false;
    }

    public void CombinedToNode(Node to)
    {
        Target = to;
        combined = true;
    }

    public void StartMoving()
    {
        float moveTime = 0.1f;
        StartCoroutine(OnMoveTo(Target.localPosition, moveTime, EventAfterMove));
    }

    private void EventAfterMove()
    {
        if ( Target != null )
        {
            if ( combined )
            {
                Target.blockInfo.Numeric *= 2;
                Target.blockInfo.StartPunchScale(Vector3.one * 0.25f, 0.15f, EventAfterPunchScale);
            }
            else
            {
                Target = null;
            }
        }
    }

    private void EventAfterPunchScale()
    {
        Target = null;
        NeedDestroyed = true;
    }

    public void StartPunchScale(Vector3 punch, float time, Action action)
    {
        StartCoroutine(OnPunchScale(punch, time, action));
    }

    private IEnumerator OnPunchScale(Vector3 punch, float time, Action action)
    {
        Vector3 current = Vector3.one;

        yield return StartCoroutine(OnScaleTo(current, current + punch, time * 0.5f));
        // the 2 coroutine comes into 1 second
        yield return StartCoroutine(OnScaleTo(current + punch, current, time * 0.5f));

        if (action != null) action.Invoke();
    }

    private IEnumerator OnScaleTo(Vector3 start, Vector3 end, float time)
    {
        float current = 0;
        float percent = 0;

        while ( percent < 1 )
        {
            current += Time.deltaTime;
            percent = current / time;

            transform.localScale = Vector3.Lerp(start, end, percent);

            yield return null;
        }
    }

    private IEnumerator OnMoveTo(Vector3 end, float moveTime, Action action)
    {
        Vector3 start = transform.localPosition; //GetComponent<RectTransform>().localPosition;
        float current = 0;
        float percent = 0;

        while ( percent < 1 )
        {
            current += Time.deltaTime;
            percent = current / moveTime;

            transform.localPosition = Vector3.Lerp(start, end, percent);

            yield return null;
        }

        if (action != null) action.Invoke();
    }
}
