using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBlock : MonoBehaviour
{
    public Color Color { get; private set; }

    public void Initialized(Color color)
    {
        for ( int i = 0; i < transform.childCount; i++ )
        {
            transform.GetChild(i).GetComponent<SpriteRenderer>().color = color;
        }
        Color = color;
    }
}
