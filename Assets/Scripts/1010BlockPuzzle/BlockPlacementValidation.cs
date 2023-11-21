using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPlacementValidation : MonoBehaviour
{
    private BlockSlot[] theBlockBoard;
    private Vector2Int  blockCount;
    private Vector2     blockHalf;

    public void Initialized(BlockSlot[] theBlockBoard, Vector2Int blockCount, Vector2 blockHalf)
    {
        this.theBlockBoard = theBlockBoard;
        this.blockCount = blockCount;
        this.blockHalf = blockHalf;
    }

    public bool TryPlaceBlock(DragBlock dragBlock)
    {
        // TO-DO write this function
        return false;
    }

    private int GetIndexFromPosition(Vector3 position)
    {
        float x = blockCount.x / 2f - blockHalf.x + position.x;
        float y = blockCount.y / 2f - blockHalf.y - position.y;

        return (int)(y * blockCount.x + x);
    }

    private bool IsBlockOutsideMap(Vector3 position)
    {
        float maxValue =  blockCount.y/2f - blockHalf.y;
        float minValue = -blockCount.x/2f + blockHalf.x;

        if ( position.x > maxValue || position.x < minValue || position.y > maxValue || position.y < minValue )
        {
            return true;
        }

        return false;
    }

    private bool IsBlockFilled(Vector3 position)
    {
        int index = GetIndexFromPosition(position);

        if ( theBlockBoard[index].IsFilled ) return true;

        return false;
    }
}
