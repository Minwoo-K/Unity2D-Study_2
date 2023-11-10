using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPlacementValidation : MonoBehaviour
{
    private BlockSlot[] theBlockBoard;
    private Vector2Int blockCount;
    private Vector2 blockHalf;

    public void Initialized(BlockSlot[] theBlockBoard, Vector2Int blockCount, Vector2 blockHalf)
    {
        this.theBlockBoard = theBlockBoard;
        this.blockCount = blockCount;
        this.blockHalf = blockHalf;
    }

    public bool TryPlaceDragBlock(DragBlock dragBlock)
    {
        // TO-DO: Validate whether there's another block on the spot

        // TO-DO: Validate whether the block was placed within the map
        return false;
    }

    public bool IsOtherBlockOnSpot(Vector3 position)
    {
        return false;
    }

    public bool IsBlockOutsideTheMap(Vector3 position)
    {
        if ( position.x < -blockCount.x / 2f + blockHalf.x || blockCount.x / 2f - blockHalf.x > position.x ||
             position.y > -blockCount.y / 2f + blockHalf.y || blockCount.y / 2f - blockHalf.y > position.y )
        {
            return true;
        }

        return false;
    }

    private int GetIndexFromPosition(Vector3 position)
    {
        float x = blockCount.x / 2f - blockHalf.x + position.x;
        float y = blockCount.y / 2f - blockHalf.y - position.y;

        return (int)(y * blockCount.x + x);
    }
}
