using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDeploymentSystem : MonoBehaviour
{
    private BlockSlot[] theBlockBoard;
    private Vector2Int blockCount;
    private Vector2    blockHalf;
        

    public void Initialized(BlockSlot[] theBlockBoard, Vector2Int blockCount, Vector2 blockHalf)
    {
        this.theBlockBoard = theBlockBoard;
        this.blockCount = blockCount;
        this.blockHalf = blockHalf;
    }

    public bool TryDeployDragBlock(DragBlock blocks)
    {
        return false;
    }

    private int PositionToIndex(Vector2 position)
    {
        float x = blockCount.x * 0.5f - blockHalf.x + position.x;
        float y = blockCount.y * 0.5f - blockHalf.y - position.y;

        int index = (int)(y * blockCount.x + x);

        return index;
    }

    private bool IsBlockInTheBoard(Vector2 position)
    {
        if ( position.x < -blockCount.x * 0.5f + blockHalf.x || position.x > blockCount.x * 0.5f - blockHalf.x ||
             position.y < -blockCount.y * 0.5f + blockHalf.y || position.y > blockCount.y * 0.5f - blockHalf.y)
        {
            return false;
        }

        return true;
    }

    private bool IsOtherBlockInTheSlot(Vector2 position)
    {
        int index = PositionToIndex(position);

        if (theBlockBoard[index].IsFilled) return false;
        else return true;
    }
}
