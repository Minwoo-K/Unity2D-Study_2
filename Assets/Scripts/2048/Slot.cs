using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public Block placedBlock;
    public Vector2 localPosition;

    public Vector2Int Coordinate { get; private set; }

    public void Initialized(Vector2Int coordinate)
    {
        Coordinate = coordinate;
    }
}
