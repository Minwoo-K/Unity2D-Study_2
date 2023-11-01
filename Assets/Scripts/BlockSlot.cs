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

    public void Filled(Color blockColor)
    {
        spriteRenderer.color = blockColor;

        IsFilled = true;
    }
}
