using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public Block placedBlock;
    public Vector2 localPosition;

    public Vector2Int Coordinate { get; private set; }
    public Vector2Int?[] NeighbourSlots { get; private set; }

    private BoardManager boardManager;

    public void Initialized(BoardManager boardManager, Vector2Int?[] neighbourSlots, Vector2Int coordinate)
    {
        this.boardManager = boardManager;
        NeighbourSlots = neighbourSlots;
        Coordinate = coordinate;
    }
}
