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
    private Image block_image;
    [SerializeField]
    private TextMeshProUGUI blockNumeric_text;

    public Slot Target { get; private set; }

    private int numeric;

    public int Numeric
    {
        set
        {
            numeric = value;

            blockNumeric_text.text = numeric.ToString();

            block_image.color = blockColorList[(int)Mathf.Log(value, 2) - 1];
        }

        get => numeric;
    }

    public void Initialized()
    {
        int random = UnityEngine.Random.Range(1, 100);

        Numeric = random > 90 ? 4 : 2;

        StartCoroutine(ScaleAnimation(Vector3.one * 0.5f, Vector3.one, 0.15f));
    }

    public void StartMovingToTarget()
    {
        float moveTime = 0.1f;
        StartCoroutine(MoveAnimation(Target.localPosition, moveTime, EventAfterMoving));
    }

    public void MoveTo(Slot to)
    {
        Target = to;
    }

    private void EventAfterMoving()
    {
        if ( Target != null )
        {
            Target = null;
        }
    }

    private IEnumerator ScaleAnimation(Vector3 start, Vector3 end, float time)
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

    private IEnumerator MoveAnimation (Vector3 end, float time, Action actionAfter)
    {
        float current = 0;
        float percent = 0;
        Vector3 start = transform.localPosition;

        while ( percent < 1 )
        {
            current += Time.deltaTime;
            percent = current / time;

            transform.localPosition = Vector3.Lerp(start, end, percent);

            yield return null;
        }

        if ( actionAfter != null )
        {
            actionAfter.Invoke();
        }
    }
}
