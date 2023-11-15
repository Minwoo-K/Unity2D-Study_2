using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPuzzleManager : MonoBehaviour
{
    [Header("Core Scripts")]
    [SerializeField]
    private BlockBoardCreator backgroundBlockBoard;
    [SerializeField]
    private BlockBoardCreator blockBoardCreator;
    [SerializeField]
    private DragBlockSpawner dragBlockSpawner;
    [SerializeField]
    private BlockPlacementValidation blockPlacementValidation;
    [SerializeField]
    private UI_Manager ui_Manager;

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

        blocksToEmpty = new List<BlockSlot>();

        blockPlacementValidation.Initialized(this, theBlockBoard, blockCount, blockHalf);

        StartCoroutine(SpawnDragBlocks());
    }

    private IEnumerator SpawnDragBlocks()
    {
        dragBlockCount = maxDragBlockCount;

        dragBlockSpawner.SpawnDragBlocksCommand();

        yield return new WaitUntil(() => IsSpawnDragBlocksComplete());
    }

    private bool IsSpawnDragBlocksComplete()
    {
        int count = 0;

        for ( int i = 0; i < dragBlockSpawner.SpawningPoints.Length; i++ )
        {
            if ( dragBlockSpawner.SpawningPoints[i].childCount != 0 && 
                 dragBlockSpawner.SpawningPoints[i].GetChild(0).localPosition == Vector3.zero )
            {
                count++;
            }
        }

        return ( count == dragBlockSpawner.SpawningPoints.Length );
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

            if ( filledBlockCount == blockCount.x )
            {
                for ( int x = 0; x < blockCount.x; x++ )
                {
                    blocksToEmpty.Add(theBlockBoard[y * blockCount.x + x]);
                }

                LinesToEmpty++;
            }

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

            if (filledBlockCount == blockCount.y)
            {
                for (int y = 0; y < blockCount.y; y++)
                {
                    blocksToEmpty.Add(theBlockBoard[y * blockCount.x + x]);
                }

                LinesToEmpty++;
            }
        }

        return LinesToEmpty;
    }

    private IEnumerator EmptyFilledLines()
    {
        foreach ( BlockSlot blockSlot in blocksToEmpty)
        {
            blockSlot.GetEmpty();

            yield return new WaitForSeconds(0.01f);
        }

        blocksToEmpty.Clear();
    }

    public void AfterBlockPlacement(DragBlock dragBlock)
    {
        StartCoroutine(OnAfterBlockPlacement(dragBlock));
    }

    private IEnumerator OnAfterBlockPlacement(DragBlock dragBlock)
    {
        Destroy(dragBlock.gameObject);

        dragBlockCount--;

        // Check for a full line
        int LinesToEmpty = FilledLineCheck();

        yield return StartCoroutine(EmptyFilledLines());

        ui_Manager.CurrentScore += LinesToEmpty == 0 ? dragBlock.ChildBlockPositions.Length :
            dragBlock.ChildBlockPositions.Length + (int)Mathf.Pow(LinesToEmpty, 2f) * 10;

        if (dragBlockCount == 0)
        {
            yield return StartCoroutine(SpawnDragBlocks());
        }

        yield return new WaitForEndOfFrame();

        if ( IsGameOver() )
        {
            Debug.Log("Game Over");

            if (ui_Manager.CurrentScore > ui_Manager.HighestScore )
            {
                PlayerPrefs.SetInt("HighestScore", ui_Manager.CurrentScore);
            }
        }
    }

    private bool IsGameOver()
    {
        int dragBlockCount = 0;
        // Check whether any DragBlock is left
        // If so, check if it can be placed anywhere
        // If not, the game still goes on
        // If the DragBlock(s) left can NOT be placed, Game Over
        for (int i = 0; i < dragBlockSpawner.SpawningPoints.Length; i++)
        {
            if ( dragBlockSpawner.SpawningPoints[i].childCount != 0 )
            {
                dragBlockCount++;

                DragBlock drag = dragBlockSpawner.SpawningPoints[i].GetComponentInChildren<DragBlock>();
                bool gameon = blockPlacementValidation.IsPossibleToPlaceBlock(drag);
                if ( gameon ) return false;
            }
        }
        // Still DragBlock left after the Placeable Check, that's GameOver
        return (dragBlockCount != 0); // false == Isn't Game Over
    }
}
