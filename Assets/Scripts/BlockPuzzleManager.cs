using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPuzzleManager : MonoBehaviour
{
    [SerializeField]
    private BlockBoardCreator backgroundBlockBoard;
    [SerializeField]
    private BlockBoardCreator blockBoardCreator;
    [SerializeField]
    private DragBlockSpawner dragBlockSpawner;
    [SerializeField]
    private BlockPlacementValidation blockPlacementValidation;

    private BlockSlot[] theBlockBoard;
    private int dragBlockCount;

    private readonly Vector2Int blockCount = new Vector2Int(10, 10);
    private readonly Vector2 blockHalf = new Vector2(0.5f, 0.5f);
    private readonly int maxDragBlockCount = 3;


    private void Awake()
    {
        backgroundBlockBoard.CreateBoard(blockCount, blockHalf);

        theBlockBoard = new BlockSlot[blockCount.x * blockCount.y];
        theBlockBoard = blockBoardCreator.CreateBoard(blockCount, blockHalf);

        blockPlacementValidation.Initialized(theBlockBoard, blockCount, blockHalf);

        SpawnDragBlocks();
    }

    private void SpawnDragBlocks()
    {
        dragBlockCount = maxDragBlockCount;
        dragBlockSpawner.SpawnDragBlocksCommand();
    }

    public void AfterBlockPlacement(DragBlock dragBlock)
    {

    }

    private void OnAfterBlockPlacement()
    {

    }
}
