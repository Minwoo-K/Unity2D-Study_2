using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { None = -1, Up = 0, Right, Down, Left }

public class Slot : MonoBehaviour
{
    public Block placedBlock;
    public Vector2 localPosition;
    public bool combined = false;                               // Whether this Slot has been combined this round
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
            // Fetch the Slot's info
            Vector2Int coordinate = NeighbourSlots[(int)direction].Value;
            Slot neighbourSlot = boardManager.theBoard[coordinate.y * boardManager.BoardCount.x + coordinate.x];

            // If the adjacent Slot is combining, return 'this'
            if ( neighbourSlot != null && neighbourSlot.combined )
            {
                return this;
            }

            // If both Slots have block info,
            if ( neighbourSlot.placedBlock != null && originalSlot.placedBlock != null )
            {
                // AND the both blocks are the same number, they need to be at the neighbourSlot
                if ( neighbourSlot.placedBlock.Numeric == originalSlot.placedBlock.Numeric )
                {
                    return neighbourSlot;
                }
                // And the blocks' numbers are different
                else
                {
                    return nextSlot;
                }
                
            }

            return neighbourSlot.FindSlotToLand(originalSlot, direction, neighbourSlot);
        }

        return nextSlot;
    }
}
