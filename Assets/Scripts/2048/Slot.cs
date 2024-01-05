using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { None = -1, Up = 0, Right, Down, Left }

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

    public Slot FindSlotToLand(Slot originalSlot, Direction direction, Slot nextSlot=null)
    {
        // If a Slot exists on the direction
        if ( NeighbourSlots[(int)direction].HasValue )
        {
            Vector2Int coordinate = NeighbourSlots[(int)direction].Value;
            Slot neighbourSlot = boardManager.theBoard[coordinate.y * boardManager.BoardCount.x + coordinate.x];

            if ( neighbourSlot.placedBlock != null && originalSlot != null )
            {
                return nextSlot;
            }

            return neighbourSlot.FindSlotToLand(originalSlot, direction, nextSlot);
        }

        return nextSlot;
    }
}
