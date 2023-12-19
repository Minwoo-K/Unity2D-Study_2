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

    public void Initialized()
    {
        Numeric = Random.Range(0, 100) < 90 ? 2 : 4;

        StartCoroutine(OnScaleTo(Vector3.zero, Vector3.one, 0.15f));
    }

    public void MoveToNode(Node node)
    {
        Target = node;
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
            Target = null;
        }
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
