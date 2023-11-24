using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPuzzleManager : MonoBehaviour
{
    [SerializeField]
    private BlockBoardCreator backgroundBlockCreator;
    [SerializeField]
    private BlockBoardCreator theBlockBoardCreator;
    [SerializeField]
    private BlockPlacementValidation blockPlacementValidation;
    [SerializeField]
    private DragBlockSpawner dragBlockSpawner;

    private BlockSlot[] theBlockBoard;
    private int dragBlockCount;
    private List<BlockSlot> blocksToEmpty;

    private readonly Vector2Int blockCount = new Vector2Int(10, 10);
    private readonly Vector2 blockHalf = new Vector2(0.5f, 0.5f);
    private readonly int maxDragBlocks = 3;

    private void Awake()
    {
        backgroundBlockCreator.Initialized(blockCount, blockHalf);
        backgroundBlockCreator.CreateBlockBoard();

        theBlockBoardCreator.Initialized(blockCount, blockHalf);
        theBlockBoard = theBlockBoardCreator.CreateBlockBoard();

        blockPlacementValidation.Initialized(this, theBlockBoard, blockCount, blockHalf);

        SpawnDragBlocks();

        blocksToEmpty = new List<BlockSlot>();
    }

    private void SpawnDragBlocks()
    {
        dragBlockCount = maxDragBlocks;

        dragBlockSpawner.SpawnDragBlocks();
    }

    private int CheckFilledLine()
    {
        int filledLine = 0;

        // Horizontal Lines
        for ( int y = 0; y < blockCount.y; y++ )
        {
            int count = 0;
            for ( int x = 0; x < blockCount.x; x++ )
            {
                if ( theBlockBoard[y * blockCount.x + x].IsFilled ) count++;
            }

            if (count == blockCount.x)
            {
                filledLine++;

                for (int x = 0; x < blockCount.x; x++)
                {
                    blocksToEmpty.Add(theBlockBoard[y * blockCount.x + x]);
                }
            }
        }

        // Vertical Lines
        for ( int x = 0; x < blockCount.x; x++ )
        {
            int count = 0;
            for ( int y = 0; y < blockCount.y; y++ )
            {
                if ( theBlockBoard[y * blockCount.x + x].IsFilled ) count++;
            }

            if ( count == blockCount.y )
            {
                filledLine++;

                for (int y = 0; y < blockCount.y; y++)
                {
                    blocksToEmpty.Add(theBlockBoard[y * blockCount.x + x]);
                }
            }
        }

        return filledLine;
    }

    private IEnumerator EmptyFilledLines()
    {
        foreach ( BlockSlot block in blocksToEmpty )
        {
            block.GetEmpty();

            yield return new WaitForSeconds(0.01f);
        }

        blocksToEmpty.Clear();
    }

    public void CommandAfterBlockPlacement(DragBlock dragBlock)
    {
        StartCoroutine(AfterBlockPlacement(dragBlock));
    }

    private IEnumerator AfterBlockPlacement(DragBlock dragBlock)
    {
        Destroy(dragBlock.gameObject);

        dragBlockCount--;

        if ( dragBlockCount == 0 )
        {
            SpawnDragBlocks();
        }

        if ( CheckFilledLine() != 0 )
        {
            yield return StartCoroutine(EmptyFilledLines());
        }

        yield return new WaitForEndOfFrame();

        if ( IsGameOver() )
        {
            Debug.Log("GameOver");
        }
    }
    private bool IsPossibleToPlaceBlock(DragBlock dragBlock)
    {
        for (int y = 0; y < blockCount.y; y++)
        {
            for (int x = 0; x < blockCount.x; x++)
            {
                int count = 0;

                Vector3 position = new Vector3(-blockCount.x / 2f + blockHalf.x + x, blockCount.y / 2f - blockHalf.y - y, 0);
                position.x = dragBlock.BlockNumber.x % 2 == 0 ? position.x + 0.5f : position.x;
                position.y = dragBlock.BlockNumber.y % 2 == 0 ? position.y - 0.5f : position.y;

                for ( int i = 0; i < dragBlock.ChildBlockPositions.Length; i++ )
                {
                    Vector3 blockPosition = position + dragBlock.ChildBlockPositions[i];

                    if ( blockPlacementValidation.IsBlockOutsideMap(blockPosition) == false ) break;
                    if ( blockPlacementValidation.IsBlockFilled(blockPosition) == false ) break;

                    count++;
                }

                if ( count == dragBlock.ChildBlockPositions.Length ) return true;
            }
        }

        return false;
    }

    private bool IsGameOver()
    {
        int dragBlockCount = 0;
        for ( int i = 0; i < dragBlockSpawner.SpawningPoints.Length; i++ )
        {
            DragBlock dragBlock = dragBlockSpawner.SpawningPoints[i].GetComponentInChildren<DragBlock>();

            if ( dragBlock != null )
            {
                dragBlockCount++;
                if ( IsPossibleToPlaceBlock(dragBlock) )
                {
                    return false;
                }
            }
        }

        // Even if at least one DragBlock is left, at this point, it's not possible to palce a DragBlock, which is GameOver
        return dragBlockCount != 0;
    }
}
