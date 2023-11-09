using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSlot : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    
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
}
