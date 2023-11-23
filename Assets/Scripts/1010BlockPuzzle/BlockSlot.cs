using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSlot : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public bool IsFilled { get; private set; } = false;

    public void Initialized()
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
        IsFilled = false;

        spriteRenderer.color = Color.white;
    }    
}