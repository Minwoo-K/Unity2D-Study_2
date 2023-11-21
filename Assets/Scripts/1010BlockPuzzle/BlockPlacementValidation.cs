using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPlacementValidation : MonoBehaviour
{
    [SerializeField]
    private BlockPuzzleManager blockPuzzleManager;

    private Vector2Int  blockCount;
    private Vector2     blockHalf;

    public void Initialized(BlockPuzzleManager blockPuzzleManager, Vector2Int blockCount, Vector2 blockHalf)
    {
        this.blockPuzzleManager = blockPuzzleManager;
        this.blockCount = blockCount;
        this.blockHalf = blockHalf;
    }
}
