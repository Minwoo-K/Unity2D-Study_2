using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDeploymentSystem : MonoBehaviour
{
    private BlockSlot[] theBlockBoard;
    private Vector2Int  blockCounts;
    private Vector2     blockHalf;

    public void Initialized(BlockSlot[] theBlockBoard, Vector2Int blockCounts, Vector2 blockHalf)
    {
        this.theBlockBoard = theBlockBoard;
        this.blockCounts = blockCounts;
        this.blockHalf = blockHalf;
    }

    public bool TryDeployBlock(DragBlock dragBlock)
    {
        return false;
    }

    private bool IsBlockInTheBoard(Vector3 position)
    {
        return false;
    }

    private bool IsOtherBlockOnTheSpot(Vector3 position)
    {
        return false;
    }
}
