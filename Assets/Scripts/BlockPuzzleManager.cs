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
    private List<BlockSlot> blocksToEmpty;

    private readonly Vector2Int blockCount = new Vector2Int(10, 10);
    private readonly Vector2 blockHalf = new Vector2(0.5f, 0.5f);
    private readonly int maxDragBlockCount = 3;


    private void Awake()
    {
        backgroundBlockBoard.CreateBoard(blockCount, blockHalf);

        theBlockBoard = new BlockSlot[blockCount.x * blockCount.y];
        theBlockBoard = blockBoardCreator.CreateBoard(blockCount, blockHalf);

        blockPlacementValidation.Initialized(this, theBlockBoard, blockCount, blockHalf);

        SpawnDragBlocks();
    }

    private void SpawnDragBlocks()
    {
        dragBlockCount = maxDragBlockCount;
        dragBlockSpawner.SpawnDragBlocksCommand();
    }

    public void AfterBlockPlacement(DragBlock dragBlock)
    {
        OnAfterBlockPlacement(dragBlock);
    }

    private void OnAfterBlockPlacement(DragBlock dragBlock)
    {
        Destroy(dragBlock.gameObject);

        dragBlockCount--;

        // Check for a full line
        int LinesToEmpty = FilledLineCheck();

        if ( LinesToEmpty != 0 )
        {
            EmptyFilledLines();
        }

        if (dragBlockCount == 0)
        {
            SpawnDragBlocks();
        }
    }

    private int FilledLineCheck()
    {
        int LinesToEmpty = 0;
        // Horizontal Line
        for ( int y = 0; y < blockCount.y; y++ )
        {
            int filledBlockCount = 0;
            for ( int x = 0; x < blockCount.x; x++ )
            {
                if ( theBlockBoard[y * blockCount.x + x].IsFilled )
                {
                    filledBlockCount++;
                }
                else break;
            }

            if ( filledBlockCount == blockCount.x ) LinesToEmpty++;
        }

        // Vertical Line
        for ( int x = 0; x < blockCount.x; x++ )
        {
            int filledBlockCount = 0;
            for ( int y = 0; y < blockCount.y; y++ )
            {
                if ( theBlockBoard[y * blockCount.x + x].IsFilled )
                {
                    filledBlockCount++;
                }
                else break;
            }

            if (filledBlockCount == blockCount.y) LinesToEmpty++;
        }

        return LinesToEmpty;
    }

    private void EmptyFilledLines()
    {

    }
}
