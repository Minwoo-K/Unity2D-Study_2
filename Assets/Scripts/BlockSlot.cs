using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSlot : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private float scalingTime = 0.15f;

    public bool IsFilled { get; private set; } = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void GetFilled(Color color)
    {
        IsFilled = true;
        spriteRenderer.color = color;
    }

    public void GetEmpty()
    {
        StartCoroutine(EmptyingAnimation(Vector3.zero));
    }

    private IEnumerator EmptyingAnimation(Vector3 end)
    {
        Vector3 start = transform.localScale;
        float current = 0;
        float percent = 0;

        while ( percent < 1 )
        {
            current += Time.deltaTime;
            percent = current / scalingTime;

            transform.localScale = Vector3.Lerp(start, end, percent);

            yield return null;
        }

        spriteRenderer.color = Color.white;
        transform.localScale = Vector3.one;
        IsFilled = false;
    }
}
