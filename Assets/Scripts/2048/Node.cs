using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    private Block block;
    private Vector3 localPosition;

    public Vector2Int Coordinate { get; private set; }

    public void Initialized(Vector2Int coordinate)
    {
        Coordinate = coordinate;
    }
}
