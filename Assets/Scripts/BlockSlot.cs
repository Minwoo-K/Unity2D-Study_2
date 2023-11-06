using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSlot : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    
    public bool IsFilled { get; private set; }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        IsFilled = false;
    }

    public void GetFilled(Color blockColor)
    {
        spriteRenderer.color = blockColor;

        IsFilled = true;
    }

    public void GetEmpty()
    {
        StartCoroutine(ScaleTo(Vector3.zero));
    }

    private IEnumerator ScaleTo(Vector3 end)
    {
        Vector3 start = transform.localScale;
        float current = 0;
        float percent = 0;
        float scalingTime = 0.15f;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / scalingTime;

            transform.localScale = Vector3.Lerp(start, end, percent);

            yield return null;
        }

        spriteRenderer.color = Color.white;
        transform.localScale = Vector3.one;
    }
}
