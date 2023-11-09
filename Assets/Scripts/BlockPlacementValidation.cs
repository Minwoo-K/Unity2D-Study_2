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
}
